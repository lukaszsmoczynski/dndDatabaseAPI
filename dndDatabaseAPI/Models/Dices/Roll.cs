using System.Collections.Generic;

namespace dndDatabaseAPI.Models.Dices
{
    public class Roll
    {
        public string Description { get; set; }
        public IDictionary<uint, int> Dices { get; set; }
    }
}
