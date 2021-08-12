namespace dndDatabaseAPI.DTOs.Spells
{
    public class MaterialComponentDto
    {
        public string Name { get; set; }
        public float? Value { get; set; }
        public bool Consumed { get; set; }
    }
}