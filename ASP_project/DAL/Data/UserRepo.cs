    using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using DAL.Models;
using Microsoft.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using DAL.Interfaces;
using System.Security.Principal;

namespace DAL.Data
{
    public class UserRepo : IUserRepo
    {
        private readonly string? _connectionString;

        public UserRepo()
        {
            _connectionString = ConnectionString.GetConnectionString();

        }

        public DBValidator CheckIfUserExists(string userName) // return true als de gebruikersnaam al gebruikt wordt
        {

            string query = "SELECT COUNT(*) FROM Users WHERE Username = @username";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                try
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@username", userName);

                        int userCount = (int)command.ExecuteScalar();

                        if (userCount > 0)
                        {

                            DBValidator result = new DBValidator(true, "Username is already taken");
                            return result;
                        }
                        else
                        {
                            DBValidator result = new DBValidator(false, "Username is free");
                            return result;
                        }
                    }
                }
                catch (Exception ex)
                {
                    DBValidator result = new DBValidator(true, "Error trying to write selected categories, try again");
                    return result;
                }
            }
        }

        public DBValidator RegisterUser(string username, string hashedpassword)
        {
            string query = "INSERT INTO Users (Username, HashedPassword, Role) VALUES (@username, @Hashedpassword, 1)";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                try
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@username", username);
                        command.Parameters.AddWithValue("@Hashedpassword", hashedpassword);

                        command.ExecuteNonQuery();
                    }
                    DBValidator result = new DBValidator(false, "Succeeded");
                    return result;
                }
                catch (Exception ex)
                {
                    DBValidator result = new DBValidator(true, "Error trying to register user, try again");
                    return result;
                }
            }
        }

        public string GetHashedPassword(string username)
        {
            string query = "SELECT HashedPassword FROM Users WHERE Username = @username";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                try
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@username", username);

                        object result = command.ExecuteScalar();

                        if (result != null && result != DBNull.Value)
                        {
                            return result.ToString();
                        }
                        else
                        {
                            return "";
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                    return "";
                }
            }
        }

        public UserDTO GetUser(string username)
        {
            var likedCategories = new List<CategoryDTO>();
            UserDTO userDTO = null;

            string query = @"
        SELECT 
            u.id AS UserID,
            u.Username,
            u.Role,
            c.id AS CategoryID,
            c.Category_name AS CategoryName
        FROM Users u
        LEFT JOIN UserCategory uc ON u.id = uc.UserId
        LEFT JOIN Category c ON uc.CategoryId = c.id
        WHERE u.Username = @Username;";

            using (var connection = new SqlConnection(_connectionString))
            using (var command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Username", username);

                connection.Open();

                using (var reader = command.ExecuteReader())
                {
                    int userId = 0;
                    string userName = "";
                    int role = 0;
                    bool userSet = false;

                    while (reader.Read())
                    {
                        if (!userSet)
                        {
                            userId = reader.GetInt32(reader.GetOrdinal("UserID"));
                            userName = reader.GetString(reader.GetOrdinal("Username"));
                            role = reader.GetInt32(reader.GetOrdinal("Role"));
                            userSet = true;
                        }


                        if (!reader.IsDBNull(reader.GetOrdinal("CategoryID")))
                        {
                            likedCategories.Add(new CategoryDTO(
                                reader.GetInt32(reader.GetOrdinal("CategoryID")),
                                reader.GetString(reader.GetOrdinal("CategoryName"))
                            ));
                        }
                    }

                    if (userSet)
                    {
                        if (likedCategories.Count > 0)
                        {
                            userDTO = new UserDTO(userId, userName, role, likedCategories);
                        }
                        else
                        {
                            userDTO = new UserDTO(userId, userName, role);
                        }
                    }
                }
            }

            return userDTO;
        }
    }
}
