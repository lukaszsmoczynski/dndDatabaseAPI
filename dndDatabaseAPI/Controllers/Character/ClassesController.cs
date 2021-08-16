using dndDatabaseAPI.Helpers;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using dndDatabaseAPI.Repositories;
using System;
using System.Threading.Tasks;
using dndDatabaseAPI.Authorization;
using dndDatabaseAPI.DTOs.Characters.Classes;
using System.Xml;
using dndDatabaseAPI.Repositories.Character;
using dndDatabaseAPI.Models.Characters.Classes;

namespace dndDatabaseAPI.Controllers.Character
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class ClassesController : ControllerBase
    {
        private readonly IClassesRepository repository;

        public ClassesController(IClassesRepository repository)
        {
            this.repository = repository;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IEnumerable<ClassDto>> GetAll()
        {
            return (await repository.GetAllAsync()).Select(characterClass => characterClass.AsDto());
        }

        [AllowAnonymous]
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

        [AllowAnonymous]
        [HttpGet("reset")]
        public async Task<IEnumerable<Class>> ResetAsync()
        {
            var xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(Properties.Resources.classes);
            var root = xmlDoc.ChildNodes[0];
            foreach (XmlNode node in root.SelectNodes("class"))
            {
                await repository.CreateAsync(ProcessClass(node));
            }

            return await repository.GetAllAsync();
        }

        private static Class ProcessClass(XmlNode node)
        {
            return new Class
            {
                Id = Guid.NewGuid(),
                Name = node.SelectSingleNode("name").InnerText,
                Description = node.SelectSingleNode("descriptions/classDescription")?.InnerText ?? "",
                AdditionalDescriptions = ProcessAdditionalDescriptions(node.SelectNodes("descriptions/additionalDescriptions/additionalDescription")),
                ClassFeatures = ProcessClassFeatures(node.SelectNodes("features/feature")),
                SubClasses = ProcessSubClasses(node.SelectNodes("subclasses/subclass")),
            };
        }

        private static IEnumerable<SubClass> ProcessSubClasses(XmlNodeList subClasses)
        {
            if (subClasses is null || subClasses.Count <= 0)
            {
                return null;
            }

            var result = new List<SubClass>();

            foreach (XmlNode subclass in subClasses)
            {
                result.Add(new()
                {
                    Id = Guid.NewGuid(),
                    Name = subclass.SelectSingleNode("name").InnerText,
                    Description = subclass.SelectSingleNode("descriptions/classDescription")?.InnerText ?? "",
                    AdditionalDescriptions = ProcessAdditionalDescriptions(subclass.SelectNodes("descriptions/additionalDescriptions/additionalDescription")),
                    ClassFeatures = ProcessClassFeatures(subclass.SelectNodes("features/feature")),
                    SubClasses = ProcessSubClasses(subclass.SelectNodes("subclasses/subclass")),
                });
            }

            return result;
        }

        private static IEnumerable<ClassFeature> ProcessClassFeatures(XmlNodeList classFeatures)
        {
            if (classFeatures is null || classFeatures.Count <= 0)
            {
                return null;
            }

            var result = new List<ClassFeature>();

            foreach (XmlNode classFeature in classFeatures)
            {
                result.Add(new()
                {
                    Level = int.Parse(classFeature.SelectSingleNode("level").InnerText),
                    Name = classFeature.SelectSingleNode("name").InnerText,
                    Description = classFeature.SelectSingleNode("description").InnerText,
                });
            }

            return result;
        }

        private static IEnumerable<AdditionalDescription> ProcessAdditionalDescriptions(XmlNodeList descriptions)
        {
            if (descriptions is null || descriptions.Count <= 0)
            {
                return null;
            }

            var result = new List<AdditionalDescription>();

            foreach(XmlNode description in descriptions)
            {
                result.Add(new ()
                {
                    Title = description.SelectSingleNode("title").InnerText,
                    Description = description.SelectSingleNode("description").InnerText,
                });
            }

            return result;
        }
    }
}
