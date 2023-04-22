using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTask
{
    internal class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }

        public User(int id, string name, string password, string role)
        {
            Id = id;
            Name = name;
            Password = password;
            Role = role;
        }
        public static void AddUser(int id, string name, string password, string role)
        {
            using (var db = new DataBase())
            {
                db.Open();
                using (var command = new SqlCommand($"INSERT INTO Users (Id, Name, Password, Role) VALUES ({id}, '{name}', '{password}', '{role}')", db.connection))
                {
                    command.ExecuteNonQuery();
                }
            }
        }

        public static void DeleteUser(int id)
        {
            using (var db = new DataBase())
            {
                db.Open();
                using (var command = new SqlCommand($"DELETE FROM Users WHERE Id = {id}", db.connection))
                {
                    command.ExecuteNonQuery();
                }
            }
        }
        public static void UpdateUserDetails(int id, string name, string password, string role)
        {
                using (var db = new DataBase())
                {
                    db.Open();
                    using (var command = new SqlCommand($"UPDATE Users SET Name = '{name}', Password = '{password}', Role = '{role}' WHERE Id = {id}", db.connection))
                    {
                        command.ExecuteNonQuery();
                    }
                }
            }

        public static User GetUserDetails(int id)
        {
                using (var db = new DataBase())
                {
                    db.Open();
                    using (var command = new SqlCommand($"SELECT Name, Password, Role FROM Users WHERE Id = {id}", db.connection))
                    {
                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                string name = reader.GetString(0);
                                string password = reader.GetString(1);
                                string role = reader.GetString(2);
                                return new User(id, name, password, role);
                            }
                            else
                            {
                                return null;
                            }
                        }
                    }
                }
        }

        public static List<Task> GetUserTasks(int id)
        {
                using (var db = new DataBase())
                {
                    db.Open();
                    using (var command = new SqlCommand($"SELECT * FROM Tasks WHERE UserId = {id}", db.connection))
                    {
                        using (var reader = command.ExecuteReader())
                        {
                            List<Task> tasks = new List<Task>();
                            while (reader.Read())
                            {
                                int taskId = reader.GetInt32(0);
                                string taskName = reader.GetString(1);
                                string taskDescription = reader.GetString(2);
                                bool isCompleted = reader.GetBoolean(3);
                                tasks.Add(new Task(taskId, taskName, taskDescription, isCompleted));
                            }
                            return tasks;
                        }
                    }
                }
            }
    }
}
