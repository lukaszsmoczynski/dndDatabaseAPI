using dndDatabaseAPI.DTOs.Spells;
using dndDatabaseAPI.Helpers;
using dndDatabaseAPI.Models.Spells;
using dndDatabaseAPI.Character.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using dndDatabaseAPI.Repositories;
using System;
using System.Threading.Tasks;

namespace dndDatabaseAPI.Controllers.Spells
{
    [ApiController]
    [Route("[controller]")]
    public class SpellsController : ControllerBase
    {
        private readonly IRepository<Spell> repository;

        public SpellsController(IRepository<Spell> repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public async Task<IEnumerable<SpellDto>> GetAllAsync()
        {
            return (await repository.GetAllAsync()).Select(spell => spell.AsDto());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SpellDto>> GetAsync(Guid id)
        {
            var spell = await repository.GetAsync(id);
            if (spell is null)
            {
                return NotFound();
            }

            return spell.AsDto();
        }

        [HttpPost]
        public async Task<ActionResult<SpellDto>> CreateAsync(CreateSpellDto spellDto)
        {
            var spell = new Spell()
            {
                Id = Guid.NewGuid(),
                Name = spellDto.Name,
                Level = spellDto.Level
            };

            await repository.CreateAsync(spell);

            return CreatedAtAction(nameof(GetAsync), new { id = spell.Id}, spell.AsDto());
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateAsync(Guid id, UpdateSpellDto spellDto)
        {
            var spell = await repository.GetAsync(id);
            if (spell is null)
            {
                return NotFound();
            }

            Spell updatedSpell = spell with
            {
                Name = spellDto.Name,
                Level = spellDto.Level
            };
            await repository.UpdateAsync(updatedSpell);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAsync(Guid id)
        {
            var spell = await repository.GetAsync(id);
            if (spell is null)
            {
                return NotFound();
            }

            await repository.DeleteAsync(spell);

            return NoContent();
        }
    }
}
