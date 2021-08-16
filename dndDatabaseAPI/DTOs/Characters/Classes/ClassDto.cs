using System;

namespace dndDatabaseAPI.DTOs.Characters.Classes
{
    public record ClassDto
    {
        public Guid Id { get; init; }
        public string Name { get; init; }
        public string Description { get; init; }
        public SubClassDto SubClass { get; init; }
    }
}
