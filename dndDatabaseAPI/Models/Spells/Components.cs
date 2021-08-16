using System.Collections.Generic;

namespace dndDatabaseAPI.Models.Spells
{
    public class Components
    {
        public bool Verbal { get; set; }
        public bool Somatic { get; set; }
        public bool Material { get; set; }

        public IEnumerable<MaterialComponent> MaterialComponents { get; set; }
    }
}
