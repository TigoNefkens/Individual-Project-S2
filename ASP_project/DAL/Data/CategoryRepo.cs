using DAL.Interfaces;
using DAL.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Identity.Client;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Data
{
    public class CategoryRepo : ICategoryRepo
    {
        private readonly string? _connectionString;

        public CategoryRepo()
        {
            _connectionString = ConnectionString.GetConnectionString();

        }

        public List<CategoryDTO> GetCategoryDTOs()
        {
            var categories = new List<CategoryDTO>();
            var query = "SELECT id, Category_name FROM Category";

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                using (var command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {


                            int id = reader.GetInt32(0);
                            string name = reader.GetString(1);

                            CategoryDTO category = new CategoryDTO(id, name);
                            categories.Add(category);

                        }

                        return categories;
                    }
                }
            }
            catch (SqlException ex)
            {

                throw new ApplicationException("Error retrieving categories", ex);
            }

            
        }

        public List<int> GetUserCategories(int userID)
        {
            var categoryIds = new List<int>();

            string query = "SELECT CategoryId FROM UserCategory WHERE UserId = @userid";

            using (var connection = new SqlConnection(_connectionString))

            using (var command = new SqlCommand(query, connection))
            {
                connection.Open();
                command.Parameters.AddWithValue("@userid", userID);
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int id = reader.GetInt32(0);

                        categoryIds.Add(id);
                    }
                }
            }

            return categoryIds;
        }



        public DBValidator SetUserCategories(int userID, int categoryID)
        {
            string query = "INSERT INTO UserCategory (userId, CategoryId) VALUES (@userId, @categoryId)";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                try
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@userId", userID);
                        command.Parameters.AddWithValue("@CategoryId", categoryID);

                        command.ExecuteNonQuery();
                    }
                    DBValidator result = new DBValidator(false, "Succeeded");
                    return result;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                    DBValidator result = new DBValidator(true, "Error trying to write selected categories, try again");
                    return result;
                }
            }
        }
    }
}

