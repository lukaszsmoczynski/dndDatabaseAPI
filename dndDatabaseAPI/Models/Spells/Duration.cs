using System;
using System.Collections.Generic;
using System.Text;

namespace dndDatabaseAPI.Models.Spells
{
    public enum SpellDurationUnit
    {
        Instant,
        Round,
        Minute,
        Hour,
        Day,
        Special,
        UntilDispelledOrTriggered
    }

    public class Duration
    {
        public int Amount { get; set; }
        public SpellDurationUnit Unit { get; set; }
    }
}
