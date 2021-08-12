using dndDatabaseAPI.DTOs.Character;
using dndDatabaseAPI.DTOs.Spells;
using dndDatabaseAPI.Helpers;
using dndDatabaseAPI.Models.Characters.Classes;
using dndDatabaseAPI.Models.Spells;
using dndDatabaseAPI.Character.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using dndDatabaseAPI.Repositories;
using System;
using System.Threading.Tasks;

namespace dndDatabaseAPI.Controllers.Character
{
    [ApiController]
    [Route("[controller]")]
    public class ClassesController : ControllerBase
    {
        private readonly IRepository<ICharacterClass> repository;

        public ClassesController(IRepository<ICharacterClass> repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public async Task<IEnumerable<ClassDto>> GetAll()
        {
            return (await repository.GetAllAsync()).Select(characterClass => characterClass.AsDto());
        }

        [HttpGet("{name}")]
        public async Task<ActionResult<ClassDto>> Get(Guid name)
        {
            var characterClass = await repository.GetAsync(name);
            if (characterClass is null)
            {
                return NotFound();
            }

            return characterClass.AsDto();
        }
    }
}
