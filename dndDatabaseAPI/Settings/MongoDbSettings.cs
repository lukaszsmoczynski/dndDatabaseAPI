using System;
using System.Diagnostics;

namespace dndDatabaseAPI.Settings
{
    public class MongoDbSettings
    {
        public string Host { get; set; }
        public int Port { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string ConnectionString
        {
            get  {
                Console.WriteLine($"mongodb://{Uri.EscapeDataString(Username)}:{Uri.EscapeDataString(Password)}@{Host}:{Port}");
                Debug.WriteLine($"mongodb://{Uri.EscapeDataString(Username)}:{Uri.EscapeDataString(Password)}@{Host}:{Port}");
                return $"mongodb://{Uri.EscapeDataString(Username)}:{Uri.EscapeDataString(Password)}@{Host}:{Port}";
            }
        }
    }
}
