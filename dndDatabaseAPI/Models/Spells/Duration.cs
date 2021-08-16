namespace dndDatabaseAPI.Models.Spells
{
    public class Duration
    {
        public bool UpTo { get; set; }
        public int Amount { get; set; }
        public SpellDurationUnit Unit { get; set; }
        public bool Contentration { get; set; }
    }
}
