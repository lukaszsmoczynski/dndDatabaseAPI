using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dndDatabaseAPI.Settings
{
    public class PostgreDbSettings
    {
        public string Database { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Host { get; set; }
        public int Port{ get; set; }
        public string ConnectionString
        {
            get => $"Host={Host}; Database={Database}; Username={Username}; Password={Password}";
        }
    }
}
