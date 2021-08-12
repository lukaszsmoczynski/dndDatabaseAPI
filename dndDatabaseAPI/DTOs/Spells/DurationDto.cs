using System;
using System.Collections.Generic;
using System.Text;

namespace dndDatabaseAPI.DTOs.Spells
{
    public enum SpellDurationUnitDto
    {
        Instant,
        Round,
        Minute,
        Hour,
        Day,
        Special,
        UntilDispelledOrTriggered
    }

    public class DurationDto
    {
        public int Amount { get; set; }
        public SpellDurationUnitDto Unit { get; set; }
    }
}
