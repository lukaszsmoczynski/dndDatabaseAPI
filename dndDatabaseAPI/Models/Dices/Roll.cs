using System;
using System.Collections.Generic;
using System.Text;

namespace dndDatabaseAPI.Models.Dices
{
    public class Roll
    {
        public string Description { get; set; }
        public Dictionary<uint, int> Dices { get; set; }
    }
}
