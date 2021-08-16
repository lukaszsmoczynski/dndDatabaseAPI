using BCryptNet = BCrypt.Net.BCrypt;
using dndDatabaseAPI.Authorization;
using dndDatabaseAPI.DTOs.Users;
using dndDatabaseAPI.Helpers;
using dndDatabaseAPI.Models.Users;
using dndDatabaseAPI.Settings;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using dndDatabaseAPI.Repositories;
using System.Threading.Tasks;

namespace dndDatabaseAPI.Services.Users
{
    public class UsersService : IUsersService
    {
        private readonly IUsersRepository usersRepository;
        private readonly IJwtUtils jwtUtils;
        private readonly JWTSettings jwtSettings;

        public UsersService(
            IUsersRepository usersRepository,
            IJwtUtils jwtUtils,
            IOptions<JWTSettings> jwtSettings)
        {
            this.usersRepository = usersRepository;
            this.jwtUtils = jwtUtils;
            this.jwtSettings = jwtSettings.Value;
        }

        public async Task<AuthenticateResponseDto> Authenticate(AuthenticateRequestDto authenticateRequest, string ipAddress)
        {
            var user = await usersRepository.GetByUsernameAsync(authenticateRequest.Username); 
            
            // validate
            if (user == null || !BCryptNet.Verify(authenticateRequest.Password, user.PasswordHash))
                throw new AppException("Username or password is incorrect");

            // authentication successful so generate jwt and refresh tokens
            var jwtToken = jwtUtils.GenerateJwtToken(user);
            var refreshToken = jwtUtils.GenerateRefreshToken(ipAddress);
            user.RefreshTokens.Add(refreshToken);

            // remove old refresh tokens from user
            RemoveOldRefreshTokens(user);

            // save changes to db
            await usersRepository.UpdateAsync(user);

            return new AuthenticateResponseDto(user, jwtToken, refreshToken.Token);
        }

        public async Task<AuthenticateResponseDto> RefreshToken(string token, string ipAddress)
        {
            var user = await GetUserByRefreshToken(token);
            var refreshToken = user.RefreshTokens.Single(x => x.Token == token);

            if (refreshToken.IsRevoked)
            {
                // revoke all descendant tokens in case this token has been compromised
                RevokeDescendantRefreshTokens(refreshToken, user, ipAddress, $"Attempted reuse of revoked ancestor token: {token}");
                await usersRepository.UpdateAsync(user);
            }

            if (!refreshToken.IsActive)
                throw new AppException("Invalid token");

            // replace old refresh token with a new one (rotate token)
            var newRefreshToken = RotateRefreshToken(refreshToken, ipAddress);
            user.RefreshTokens.Add(newRefreshToken);

            // remove old refresh tokens from user
            RemoveOldRefreshTokens(user);

            // save changes to db
            await usersRepository.UpdateAsync(user);

            // generate new jwt
            var jwtToken = jwtUtils.GenerateJwtToken(user);

            return new AuthenticateResponseDto(user, jwtToken, newRefreshToken.Token);
        }

        public async Task RevokeToken(string token, string ipAddress)
        {
            var user = await GetUserByRefreshToken(token);
            var refreshToken = user.RefreshTokens.Single(x => x.Token == token);

            if (!refreshToken.IsActive)
                throw new AppException("Invalid token");

            // revoke token and save
            RevokeRefreshToken(refreshToken, ipAddress, "Revoked without replacement");
            await usersRepository.UpdateAsync(user);
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            return await usersRepository.GetAllAsync();
        }

        public async Task<User> GetById(Guid id)
        {
            var user = await usersRepository.GetAsync(id);
            if (user == null) throw new KeyNotFoundException("User not found");
            return user;
        }

        // helper methods

        private async Task<User> GetUserByRefreshToken(string token)
        {
            var user = await usersRepository.GetByRefreshTokenAsync(token);

            if (user == null)
                throw new AppException("Invalid token");

            return user;
        }

        private RefreshToken RotateRefreshToken(RefreshToken refreshToken, string ipAddress)
        {
            var newRefreshToken = jwtUtils.GenerateRefreshToken(ipAddress);
            RevokeRefreshToken(refreshToken, ipAddress, "Replaced by new token", newRefreshToken.Token);
            return newRefreshToken;
        }

        private void RemoveOldRefreshTokens(User user)
        {
            // remove old inactive refresh tokens from user based on TTL in app settings
            user.RefreshTokens.RemoveAll(x =>
                !x.IsActive &&
                x.Created.AddDays(jwtSettings.RefreshTokenTTL) <= DateTime.UtcNow);
        }

        private void RevokeDescendantRefreshTokens(RefreshToken refreshToken, User user, string ipAddress, string reason)
        {
            // recursively traverse the refresh token chain and ensure all descendants are revoked
            if (!string.IsNullOrEmpty(refreshToken.ReplacedByToken))
            {
                var childToken = user.RefreshTokens.SingleOrDefault(x => x.Token == refreshToken.ReplacedByToken);
                if (childToken.IsActive)
                    RevokeRefreshToken(childToken, ipAddress, reason);
                else
                    RevokeDescendantRefreshTokens(childToken, user, ipAddress, reason);
            }
        }

        private static void RevokeRefreshToken(RefreshToken token, string ipAddress, string reason = null, string replacedByToken = null)
        {
            token.Revoked = DateTime.UtcNow;
            token.RevokedByIp = ipAddress;
            token.ReasonRevoked = reason;
            token.ReplacedByToken = replacedByToken;
        }
    }
}
