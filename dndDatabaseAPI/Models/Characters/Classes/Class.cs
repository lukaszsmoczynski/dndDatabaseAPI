using System;
using System.Collections.Generic;

namespace dndDatabaseAPI.Models.Characters.Classes
{
    public class Class
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public IEnumerable<ClassFeature> ClassFeatures { get; set; }
        public IEnumerable<AdditionalDescription> AdditionalDescriptions { get; set; }
        public IEnumerable<SubClass> SubClasses { get; set; }
    }
}
