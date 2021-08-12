using System;
using System.Collections.Generic;
using System.Text;

namespace dndDatabaseAPI.Models.Spells
{
    public enum SpellTimeUnit
    {
        BonusAction,
        Reaction,
        Action,
        Round,
        Minute,
        Hour
    }

    public class CastTime
    {
        public int Amount { get; set; }
        public SpellTimeUnit Unit { get; set; }
    }
}
