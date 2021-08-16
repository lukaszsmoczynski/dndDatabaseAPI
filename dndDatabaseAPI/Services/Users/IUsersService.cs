using dndDatabaseAPI.DTOs.Users;
using dndDatabaseAPI.Models.Users;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace dndDatabaseAPI.Services.Users
{
    public interface IUsersService
    {
        Task<AuthenticateResponseDto> Authenticate(AuthenticateRequestDto authenticateRequest, string ipAddress);
        Task<AuthenticateResponseDto> RefreshToken(string token, string ipAddress);
        Task RevokeToken(string token, string ipAddress);
        Task<IEnumerable<User>> GetAll();
        Task<User> GetById(Guid id);
    }
}
