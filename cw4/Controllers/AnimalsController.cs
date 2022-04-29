using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cw4.Models;
using Microsoft.AspNetCore.Mvc;

namespace Cw4
{
    [Route("[controller]")]
    [ApiController]
    public class AnimalsController : ControllerBase
    {
        private readonly IDatabaseService _databaseService;
        public AnimalsController(IDatabaseService databaseService)
        {
            _databaseService = databaseService;
        }

        [HttpGet]
        public IActionResult GetAnimals(string orderBy)
        {
            if (orderBy is not null)
            {
                if (orderBy == "name")
                {
                    return Ok(_databaseService.GetAnimals(AnimalOrderBy.Name));
                }
                if (orderBy == "description")
                {
                    return Ok(_databaseService.GetAnimals(AnimalOrderBy.Description));
                }
                if (orderBy == "category")
                {
                    return Ok(_databaseService.GetAnimals(AnimalOrderBy.Category));
                }
                if (orderBy == "area")
                {
                    return Ok(_databaseService.GetAnimals(AnimalOrderBy.Area));
                }

                return BadRequest("Invalid orderBy value.");
            }
            return Ok(_databaseService.GetAnimals(AnimalOrderBy.Name));
        }


        [HttpPost]
        public IActionResult AddAnimal(Animal animal)
        {
            if (_databaseService.AddAnimal(animal))
            {
                return Created("", animal);
            }
            return BadRequest("Adding the animal has failed");
        }

        [HttpPut("{idAnimal}")]
        public IActionResult UpdateAnimal(int idAnimal, Animal animal)
        {
            if (_databaseService.UpdateAnimal(idAnimal, animal))
            {
                return Created("", animal);
            }
            return BadRequest("Updating the animal has failed");
        }

        [HttpDelete("{idAnimal}")]
        public IActionResult DeleteAnimal(int idAnimal)
        {
            if (_databaseService.DeleteAnimal(idAnimal))
            {
                return Ok();
            }
            return BadRequest("Deleting the animal has failed");
        }
    }
}

