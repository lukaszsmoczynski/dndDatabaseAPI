using dndDatabaseAPI.DTOs.Spells;
using dndDatabaseAPI.Helpers;
using dndDatabaseAPI.Models.Spells;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using dndDatabaseAPI.Repositories;
using System;
using System.Threading.Tasks;
using dndDatabaseAPI.Authorization;
using System.Xml;
using dndDatabaseAPI.Models.Characters.Classes;
using dndDatabaseAPI.Models.Dices;
using System.Diagnostics;

namespace dndDatabaseAPI.Controllers.Spells
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class SpellsController : ControllerBase
    {
        private readonly ISpellsRepository repository;

        public SpellsController(ISpellsRepository repository)
        {
            this.repository = repository;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IEnumerable<SpellDto>> GetAllAsync()
        {
            return (await repository.GetAllAsync()).Select(spell => spell.AsDto());
        }

        [AllowAnonymous]
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

            return CreatedAtAction(nameof(GetAsync), new { id = spell.Id }, spell.AsDto());
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

        [AllowAnonymous]
        [HttpGet("reset")]
        public async Task<int> ResetAsync()
        {
            var xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(Properties.Resources.spells);
            var root = xmlDoc.ChildNodes[0];
            foreach (XmlNode node in root.SelectNodes("spell"))
            {
                await repository.CreateAsync(ProcessSpell(node));
            }

            return (await repository.GetAllAsync()).Count();
        }

        private static Spell ProcessSpell(XmlNode node)
        {
            var spell = new Spell
            {
                Id = Guid.NewGuid(),
                Name = node.SelectSingleNode("name").InnerText,
                Level = int.Parse(node.SelectSingleNode("level").InnerText),
                School = ProcessSchool(node.SelectSingleNode("school").InnerText),
                CastTime = ProcessCastTime(node.SelectSingleNode("time").InnerText),
                Range = ProcessRange(node.SelectSingleNode("range").InnerText),
                Components = ProcessComponents(node.SelectSingleNode("components")),
                Duration = ProcessDuration(node.SelectSingleNode("duration").InnerText),
                Classes = ProcessClasses(node.SelectSingleNode("classes").InnerText),
                Description = ProcessDescription(node.SelectNodes("text")),
                Ritual = node.SelectSingleNode("text")?.InnerXml.Equals("YES") ?? false,
                //Rolls = new List<Roll>() {
                //    new() { Description = "Hit", Dices = new Dictionary<uint, int>() { { 20, 1 } } },
                //    new() { Description = "Damage", Dices = new Dictionary<uint, int>() { { 6, 1 } } }
                //},
                //Upcasting = new List<Upcasting>()
                //{
                //    new () {Description = "Level 5",
                //        Level = 5,
                //        Rolls = new List<Roll>() {
                //            new() { Description = "Hit", Dices = new Dictionary<uint, int>() { { 20, 1 } } },
                //            new() { Description = "Damage", Dices = new Dictionary<uint, int>() { { 6, 2 } } }
                //        }
                //    },
                //    new () {Description = "Level 11",
                //        Level = 11,
                //        Rolls = new List<Roll>() {
                //            new() { Description = "Hit", Dices = new Dictionary<uint, int>() { { 20, 1 } } },
                //            new() { Description = "Damage", Dices = new Dictionary<uint, int>() { { 6, 3 } } }
                //        }
                //    },
                //    new () {Description = "Level 17",
                //        Level = 17,
                //        Rolls = new List<Roll>() {
                //            new() { Description = "Hit", Dices = new Dictionary<uint, int>() { { 20, 1 } } },
                //            new() { Description = "Damage", Dices = new Dictionary<uint, int>() { { 6, 4 } } }
                //        }
                //    },
                //},
            };

            return spell;
        }

        private static string ProcessDescription(XmlNodeList xmlNodeList)
        {
            var result = "";
            foreach (XmlNode node in xmlNodeList)
            {
                result += node.InnerText + "\n";
            }
            return result;
        }

        private static IEnumerable<string> ProcessClasses(string classes)
        {
            var result = new List<string>();

            var tokens = classes.Split(',');
            foreach (var className in tokens.Select(p => p.Trim()))
            {
                result.Add(className);

                //var index = className.IndexOf('(');

                //if (index < 0)
                //{
                //    result.Add(new CasterClass() { Name = className });
                //    continue;
                //}

                //var casterClass = new CasterClass() { Name = className.Substring(0, index).Trim() };

                //casterClass.SubClass = ProcessSubclass(className.Substring(index + 1, className.Length - index - 2));

                //result.Add(casterClass);
            }

            return result;
        }

        private static CasterSubClass ProcessSubclass(string subClassName)
        {

            var index = subClassName.IndexOf('(');

            if (index < 0)
            {
                return new CasterSubClass() { Name = subClassName };
            }

            var result = new CasterSubClass() { Name = subClassName.Substring(0, index).Trim() };

            //result.SubClass = ProcessSubclass(subClassName.Substring(index + 1, subClassName.Length - index - 2));

            return result;
        }

        private static Duration ProcessDuration(string innerText)
        {
            var result = new Duration();

            switch (innerText)
            {
                case "Instantaneous":
                    result.Unit = SpellDurationUnit.Instant;
                    return result;
                case "Special":
                    result.Unit = SpellDurationUnit.Special;
                    return result;
                case "Until dispelled or triggered":
                    result.Unit = SpellDurationUnit.UntilDispelledOrTriggered;
                    return result;
                case "Until dispelled":
                    result.Unit = SpellDurationUnit.UntilDispelled;
                    return result;
            }
            if (innerText.StartsWith("Concentration"))
            {
                result.Contentration = true;
                var tokens = innerText.Split(',');
                innerText = tokens[1].Trim();
            }
            innerText = innerText.ToLower();

            if (innerText.StartsWith("up to"))
            {
                result.UpTo = true;
                innerText = innerText[6..];
            }

            var x = innerText.Split(" ");
            switch (x[1])
            {
                case "round":
                case "rounds": result.Unit = SpellDurationUnit.Round; break;
                case "minute":
                case "minutes": result.Unit = SpellDurationUnit.Minute; break;
                case "hour":
                case "hours": result.Unit = SpellDurationUnit.Hour; break;
                case "day":
                case "days": result.Unit = SpellDurationUnit.Day; break;
            }
            result.Amount = int.Parse(x[0]);

            return result;
        }

        private static Components ProcessComponents(XmlNode components)
        {
            var result = new Components();
            var _components = components.SelectSingleNode("value").InnerText;
            result.Verbal = _components.Contains("V");
            result.Somatic = _components.Contains("S");
            result.Material = _components.Contains("M");

            result.MaterialComponents = ProcessMaterials(components.SelectSingleNode("materials"));

            return result;
        }

        private static IEnumerable<MaterialComponent> ProcessMaterials(XmlNode materials)
        {
            if (materials is null)
            {
                return null;
            }

            var result = new List<MaterialComponent>();
            foreach (XmlNode node in materials)
            {
                var materialComponent = new MaterialComponent()
                {
                    Name = node.SelectSingleNode("name").InnerText,
                    Consumed = node.SelectSingleNode("consumed")?.InnerText.Equals("1") ?? false,
                    Description = node.SelectSingleNode("description")?.InnerText,
                };

                var valueNode = node.SelectSingleNode("value");
                if (valueNode is not null)
                {
                    var tokens = valueNode.InnerText.Split(' ');
                    materialComponent.Value = float.Parse(tokens[0]);
                }

                result.Add(materialComponent);
            }
            return result;
        }

        private static Models.Spells.Range ProcessRange(string range)
        {
            var result = new Models.Spells.Range();

            if (range.StartsWith("Self"))
            {
                result.Unit = SpellRangeUnit.Self;
                return result;
            }
            else if (range.StartsWith("Touch"))
            {
                result.Unit = SpellRangeUnit.Touch;
                return result;
            }
            else if (range.StartsWith("Sight"))
            {
                result.Unit = SpellRangeUnit.Sight;
                return result;
            }
            else if (range.StartsWith("Special"))
            {
                result.Unit = SpellRangeUnit.Special;
                return result;
            }
            else if (range.StartsWith("Unlimited"))
            {
                result.Unit = SpellRangeUnit.Special;
                return result;
            }

            var tokens = range.Split(' ');
            result.Amount = int.Parse(tokens[0]);
            switch (tokens[1])
            {
                case "feet": result.Unit = SpellRangeUnit.Foot; break;
                case "mile":
                case "miles":
                    result.Unit = SpellRangeUnit.Mile;
                    break;
            }

            return result;
        }

        private static CastTime ProcessCastTime(string castTime)
        {
            var tokens = castTime.Split(' ');
            var result = new CastTime
            {
                Amount = int.Parse(tokens[0])
            };

            switch (tokens[1])
            {
                case "bonus": result.Unit = SpellTimeUnit.BonusAction; break;
                case "reaction": result.Unit = SpellTimeUnit.Reaction; break;
                case "action": result.Unit = SpellTimeUnit.Action; break;
                case "round": result.Unit = SpellTimeUnit.Round; break;
                case "minute": result.Unit = SpellTimeUnit.Minute; break;
                case "hour": result.Unit = SpellTimeUnit.Hour; break;
            }

            return result;
        }

        private static School ProcessSchool(string schoolCode)
        {
            return schoolCode switch
            {
                "A" => School.Abjuration,
                "C" => School.Conjuration,
                "D" => School.Divination,
                "EN" => School.Enchantment,
                "EV" => School.Evocation,
                "I" => School.Illusion,
                "N" => School.Necromancy,
                "T" => School.Transmutation,
                _ => throw new Exception(),
            };
        }
    }
}
