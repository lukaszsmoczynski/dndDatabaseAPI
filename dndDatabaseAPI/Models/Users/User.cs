using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace dndDatabaseAPI.Models.Users
{
    public class User
    {
        public Guid Id { get; set; }
        public string FirstName { get; internal set; }
        public string LastName { get; internal set; }
        public string Username { get; set; }

        [JsonIgnore]
        public string PasswordHash { get; set; }

        [JsonIgnore]
        public List<RefreshToken> RefreshTokens { get; set; }
    }
}
