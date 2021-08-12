using System;
using System.Collections.Generic;
using System.Text;

namespace dndDatabaseAPI.DTOs.Spells
{
    public class ComponentsDto
    {
        public bool Verbal { get; set; } = false;
        public bool Somatic { get; set; } = false;
        public bool Material { get; set; } = false;

        public List<MaterialComponentDto> MaterialComponents { get; set; } = new List<MaterialComponentDto>();
    }
}
