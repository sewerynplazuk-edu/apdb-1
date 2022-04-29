using System;
using System.Collections.Generic;
using Cw4.Models;

namespace Cw4
{
    public class DatabaseService : IDatabaseService
    {
        public bool AddAnimal(Animal animal)
        {
            return true;
        }

        public bool DeleteAnimal(int idAnimal)
        {
            return true;
        }

        public IEnumerable<Animal> GetAnimals(AnimalOrderBy orderBy)
        {
            var list = new List<Animal>();
            list.Add(new Animal
            {
                IdAnimal = 44,
                Name = "Jan",
                Description = "Łobuz",
                Category = "Pies",
                Area = "Wkz"
            });
            return list;
        }

        public bool UpdateAnimal(int idAnimal, Animal animal)
        {
            return true;
        }
    }
}

