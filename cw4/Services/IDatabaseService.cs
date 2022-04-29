using System;
using System.Collections.Generic;
using Cw4.Models;

namespace Cw4
{
	public interface IDatabaseService
	{
		IEnumerable<Animal> GetAnimals(AnimalOrderBy orderBy);
		bool AddAnimal(Animal animal);
		bool UpdateAnimal(int idAnimal, Animal animal);
		bool DeleteAnimal(int idAnimal);
	}
}

