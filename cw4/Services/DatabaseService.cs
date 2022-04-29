using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using Cw4.Models;

namespace Cw4
{
    public class DatabaseService : IDatabaseService
    {
        private static readonly string ConnectionString = "Data Source=db-mssql;Initial Catalog=s20129;Integrated Security=True";

        public bool AddAnimal(Animal animal)
        {
            try
            {
                using var connection = new SqlConnection(ConnectionString);
                using var command = connection.CreateCommand();
                connection.Open();
                command.CommandText = $"INSERT INTO Animals(IdAnimal, Name, Description, Category, Area) " +
                    $"VALUES (@param1, @param2, @param3, @param4, @param5)";
                command.Parameters.AddWithValue("@param1", animal.IdAnimal);
                command.Parameters.AddWithValue("@param2", animal.Name);
                command.Parameters.AddWithValue("@param3", animal.Description);
                command.Parameters.AddWithValue("@param4", animal.Category);
                command.Parameters.AddWithValue("@param5", animal.Area);
                int affectedRowsCount = command.ExecuteNonQuery();
                return affectedRowsCount == 1;
            }
            catch
            {
                return false;
            }
        }

        public bool DeleteAnimal(int idAnimal)
        {
            try
            {
                using var connection = new SqlConnection(ConnectionString);
                using var command = connection.CreateCommand();
                connection.Open();
                command.CommandText = $"DELETE FROM Animals WHERE IdAnimal = @param1";
                command.Parameters.AddWithValue("@param1", idAnimal);
                int affectedRowsCount = command.ExecuteNonQuery();
                return affectedRowsCount == 1;
            }
            catch
            {
                return false;
            }
        }

        public IEnumerable<Animal> GetAnimals(AnimalOrderBy orderBy)
        {
            try
            {
                var animals = new List<Animal>();
                using var connection = new SqlConnection(ConnectionString);
                using var command = connection.CreateCommand();
                connection.Open();
                command.CommandText = $"SELECT * FROM Animals ORDER BY @param1";
                command.Parameters.AddWithValue("@param1", MakeOrderByRawValue(orderBy));

                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    animals.Add(new Animal
                    {
                        IdAnimal = int.Parse(reader["IdAnimal"].ToString()),
                        Name = reader["Name"].ToString(),
                        Description = reader["Description"].ToString(),
                        Category = reader["Category"].ToString(),
                        Area = reader["Area"].ToString(),
                    });
                }

                return animals;
            }
            catch
            {
                return new List<Animal>();
            }
        }

        public bool UpdateAnimal(int idAnimal, Animal animal)
        {
            try
            {
                using var connection = new SqlConnection(ConnectionString);
                using var command = connection.CreateCommand();
                connection.Open();
                command.CommandText = $"UPDATE Animals " +
                    $"SET Name = @param1, Description = @param2, Category = @param3, Area = @param4 " +
                    $"WHERE IdAnimal = @param5";
                command.Parameters.AddWithValue("@param1", animal.Name);
                command.Parameters.AddWithValue("@param2", animal.Description);
                command.Parameters.AddWithValue("@param3", animal.Category);
                command.Parameters.AddWithValue("@param4", animal.Area);
                command.Parameters.AddWithValue("@param5", idAnimal);
                int affectedRowsCount = command.ExecuteNonQuery();
                return affectedRowsCount == 1;
            }
            catch
            {
                return false;
            }
        }

        private static string MakeOrderByRawValue(AnimalOrderBy orderBy)
        {
            return orderBy switch
            {
                AnimalOrderBy.Name => "name",
                AnimalOrderBy.Description => "description",
                AnimalOrderBy.Category => "category",
                AnimalOrderBy.Area => "area",
                _ => "name",
            };
        }
    }
}

