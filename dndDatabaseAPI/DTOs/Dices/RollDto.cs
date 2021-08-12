using System;
using System.Collections.Generic;
using System.Text;

namespace dndDatabaseAPI.DTOs.Dices
{
    public class RollDto
    {
        public string Description { get; set; }
        public Dictionary<uint, int> Dices { get; set; }
    }
}
