namespace dndDatabaseAPI.DTOs.Characters.Classes
{
    public record CasterClassDto : ClassDto
    {
        private CasterSubClassDto subClass;

        public new CasterSubClassDto SubClass
        {
            get => subClass; 
            init
            {
                base.SubClass = value;
                subClass = value;
            }
        }
    }
}
