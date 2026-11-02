using DAL.Models;
using Microsoft.Data.SqlClient;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Data
{
    public class TabRepo : ITabRepo
    {

        private readonly string? _connectionString;

        public TabRepo()
        {
            _connectionString = ConnectionString.GetConnectionString();

        }

        public List<TabDTO> GetTabsFromUser(int userID)
        {
            var ownedTabs = new List<TabDTO>();

            string query = @"
            SELECT 
                t.id AS TabID,
                t.TabContent,
                t.Publicity,
                t.TabName,
                t.ViewCount,

                c.id AS CategoryID,
                c.Category_name AS CategoryName,

                u.id AS UserID,
                u.Username,
                u.Role
            FROM Tabs t
            INNER JOIN Category c ON t.Category_id = c.id
            INNER JOIN Users u ON t.Users_id = u.id
            WHERE u.id = @UserId;";

            using (SqlConnection connection = new SqlConnection(_connectionString))

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                connection.Open();
                command.Parameters.AddWithValue("@UserId", userID);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int tabId = reader.GetInt32(reader.GetOrdinal("TabID"));
                        string tabContent = reader.GetString(reader.GetOrdinal("TabContent"));
                        bool publicity = reader.GetBoolean(reader.GetOrdinal("Publicity"));
                        string tabName = reader.GetString(reader.GetOrdinal("TabName"));
                        int viewCount = reader.GetInt32(reader.GetOrdinal("ViewCount"));

                        int categoryId = reader.GetInt32(reader.GetOrdinal("CategoryID"));
                        string categoryName = reader.GetString(reader.GetOrdinal("CategoryName"));
                        CategoryDTO category = new CategoryDTO(categoryId, categoryName);

                        int userId = reader.GetInt32(reader.GetOrdinal("UserID"));
                        string userName = reader.GetString(reader.GetOrdinal("Username"));
                        int role = reader.GetInt32(reader.GetOrdinal("Role"));
                        UserDTO user = new UserDTO(userId, userName, role);

                        TabDTO tab = new TabDTO(tabId, category, tabContent, publicity, user, tabName, viewCount);
                        ownedTabs.Add(tab);
                    }
                }
            }

            return ownedTabs;
        }

        public List<TabDTO> GetAllTabs()
        {
            var tabs = new List<TabDTO>();

            string query = @"
            SELECT 
                t.id AS TabID,
                t.TabContent,
                t.Publicity,
                t.TabName,
                t.ViewCount,

                c.id AS CategoryID,
                c.Category_name AS CategoryName,

                u.id AS UserID,
                u.Username,
                u.Role
            FROM Tabs t
            INNER JOIN Category c ON t.Category_id = c.id
            INNER JOIN Users u ON t.Users_id = u.id;";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            
            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                connection.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int tabId = reader.GetInt32(reader.GetOrdinal("TabID"));
                        string tabContent = reader.GetString(reader.GetOrdinal("TabContent"));
                        bool publicity = reader.GetBoolean(reader.GetOrdinal("Publicity"));
                        string tabName = reader.GetString(reader.GetOrdinal("TabName"));
                        int viewCount = reader.GetInt32(reader.GetOrdinal("ViewCount"));

                        int categoryId = reader.GetInt32(reader.GetOrdinal("CategoryID"));
                        string categoryName = reader.GetString(reader.GetOrdinal("CategoryName"));
                        CategoryDTO category = new CategoryDTO(categoryId, categoryName);

                        int userId = reader.GetInt32(reader.GetOrdinal("UserID"));
                        string userName = reader.GetString(reader.GetOrdinal("Username"));
                        int role = reader.GetInt32(reader.GetOrdinal("Role"));
                        UserDTO user = new UserDTO(userId, userName, role);

                        TabDTO tab = new TabDTO(tabId, category, tabContent, publicity, user, tabName, viewCount);
                        tabs.Add(tab);
                    }
                }
            }

            return tabs;
        }

        public List<TabDTO> GetTabsBySearch(string searchTerm)
        {
            var searchedTabs = new List<TabDTO>();

            string query = @"
        SELECT 
            t.id AS TabID,
            t.TabContent,
            t.Publicity,
            t.TabName,
            t.ViewCount,

            c.id AS CategoryID,
            c.Category_name AS CategoryName,

            u.id AS UserID,
            u.Username,
            u.Role
        FROM Tabs t
        INNER JOIN Category c ON t.Category_id = c.id
        INNER JOIN Users u ON t.Users_id = u.id
        WHERE t.TabName LIKE @search;";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@search", $"%{searchTerm}%");

                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int tabId = reader.GetInt32(reader.GetOrdinal("TabID"));
                        string tabContent = reader.GetString(reader.GetOrdinal("TabContent"));
                        bool publicity = reader.GetBoolean(reader.GetOrdinal("Publicity"));
                        string tabName = reader.GetString(reader.GetOrdinal("TabName"));
                        int viewCount = reader.GetInt32(reader.GetOrdinal("ViewCount"));

                        int categoryId = reader.GetInt32(reader.GetOrdinal("CategoryID"));
                        string categoryName = reader.GetString(reader.GetOrdinal("CategoryName"));
                        CategoryDTO category = new CategoryDTO(categoryId, categoryName);

                        int userId = reader.GetInt32(reader.GetOrdinal("UserID"));
                        string userName = reader.GetString(reader.GetOrdinal("Username"));
                        int role = reader.GetInt32(reader.GetOrdinal("Role"));
                        UserDTO user = new UserDTO(userId, userName, role);

                        TabDTO tab = new TabDTO(tabId, category, tabContent, publicity, user, tabName, viewCount);
                        searchedTabs.Add(tab);
                    }
                }
            }

            return searchedTabs;
        }

        public TabDTO GetTabById(int id)
        {
            TabDTO tab = null;

            string query = @"
        SELECT 
            t.id AS TabID,
            t.TabContent,
            t.Publicity,
            t.TabName,
            t.ViewCount,

            c.id AS CategoryID,
            c.Category_name AS CategoryName,

            u.id AS UserID,
            u.Username,
            u.Role
        FROM Tabs t
        INNER JOIN Category c ON t.Category_id = c.id
        INNER JOIN Users u ON t.Users_id = u.id
        WHERE t.id = @Id;";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@Id", id);

                connection.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        int tabId = reader.GetInt32(reader.GetOrdinal("TabID"));
                        string tabContent = reader.GetString(reader.GetOrdinal("TabContent"));
                        bool publicity = reader.GetBoolean(reader.GetOrdinal("Publicity"));
                        string tabName = reader.GetString(reader.GetOrdinal("TabName"));
                        int viewCount = reader.GetInt32(reader.GetOrdinal("ViewCount"));

                        int categoryId = reader.GetInt32(reader.GetOrdinal("CategoryID"));
                        string categoryName = reader.GetString(reader.GetOrdinal("CategoryName"));
                        CategoryDTO category = new CategoryDTO(categoryId, categoryName);

                        int userId = reader.GetInt32(reader.GetOrdinal("UserID"));
                        string userName = reader.GetString(reader.GetOrdinal("Username"));
                        int role = reader.GetInt32(reader.GetOrdinal("Role"));
                        UserDTO user = new UserDTO(userId, userName, role);

                        tab = new TabDTO(tabId, category, tabContent, publicity, user, tabName, viewCount);
                    }
                }
            }

            return tab;
        }

        public DBValidator IncreaseViewCount(int tabId)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                string querie = "UPDATE Tabs SET ViewCount = ViewCount + 1 WHERE Id = @TabId";

                using (SqlCommand command = new SqlCommand(querie, connection))
                {
                    command.Parameters.AddWithValue("@TabId", tabId);

                    int rowsAffected = command.ExecuteNonQuery();

 
                    if (rowsAffected == 0)
                    {

                        return new DBValidator(true, "Error setting viewcount");
                    }
                    else
                    {
                        return new DBValidator(false, "Succeeded");
                    }
                }
            }
        }
    }
}
