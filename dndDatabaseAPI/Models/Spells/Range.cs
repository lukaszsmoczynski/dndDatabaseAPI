using System;
using System.Collections.Generic;
using System.Text;

namespace dndDatabaseAPI.Models.Spells
{
    public enum SpellRangeUnit
    {
        Self,
        Touch,
        Foot,
        Mile,
        Sight,
        Special,
        Unlimited
    }

    public class Range
    {
        public int Amount { get; set; }
        public SpellRangeUnit Unit { get; set; }
    }
}
