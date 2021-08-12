using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace dndDatabaseAPI.DTOs.Spells
{
    public enum SpellRangeUnitDto
    {
        Self,
        Touch,
        Foot,
        Mile,
        Sight,
        Special,
        Unlimited
    }

    public class RangeDto
    {
        public int Amount { get; set; }
        [Required]
        public SpellRangeUnitDto Unit { get; set; }
    }
}
