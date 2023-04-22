using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTask
{
    internal class Task
    {
        public Task(int taskId, string taskName, string taskDescription, bool isCompleted)
        {
            TaskId = taskId;
            TaskName = taskName;
            TaskDescription = taskDescription;
            IsCompleted = isCompleted;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Deadline { get; set; }
        public int Priority { get; set; }
        public string Status { get; set; }
        public int TaskId { get; }
        public string TaskName { get; set; }
        public string TaskDescription { get; set; }
        public bool IsCompleted { get; set; }
        public void AddTask()
        {
            using (var db = new DataBase())
            {
                db.Open();
                SqlCommand command = new SqlCommand("INSERT INTO Tasks (TaskName, TaskDescription, IsCompleted) VALUES (@TaskName, @TaskDescription, @IsCompleted)", db.connection);
                command.Parameters.AddWithValue("@TaskName", TaskName);
                command.Parameters.AddWithValue("@TaskDescription", TaskDescription);
                command.Parameters.AddWithValue("@IsCompleted", IsCompleted);
                command.ExecuteNonQuery();
            }
        }
        public void DeleteTask()
        {
            using (var db = new DataBase())
            {
                db.Open();
                SqlCommand command = new SqlCommand("DELETE FROM Tasks WHERE TaskId = @TaskId", db.connection);
                command.Parameters.AddWithValue("@TaskId", TaskId);
                command.ExecuteNonQuery();
            }
        }
        public void UpdateTaskStatus(string status)
        {
            using (var db = new DataBase())
            {
                db.Open();
                SqlCommand command = new SqlCommand("UPDATE Tasks SET Status = @Status WHERE TaskId = @TaskId", db.connection);
                command.Parameters.AddWithValue("@Status", status);
                command.Parameters.AddWithValue("@TaskId", TaskId);
                command.ExecuteNonQuery();
            }
        }
        public void AssignTaskToUser(int userId)
        {
            using (var db = new DataBase())
            {
                db.Open();
                SqlCommand command = new SqlCommand("UPDATE Tasks SET AssignedUserId = @UserId WHERE TaskId = @TaskId", db.connection);
                command.Parameters.AddWithValue("@UserId", userId);
                command.Parameters.AddWithValue("@TaskId", TaskId);
                command.ExecuteNonQuery();
            }
        }
        public void GetTaskDetails(int taskId)
        {
            using (var db = new DataBase())
            {
                db.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM Tasks WHERE TaskId = @TaskId", db.connection);
                command.Parameters.AddWithValue("@TaskId", taskId);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Id = (int)reader["TaskId"];
                    Name = (string)reader["TaskName"];
                    Description = (string)reader["TaskDescription"];
                    Deadline = (DateTime)reader["Deadline"];
                    Priority = (int)reader["Priority"];
                    Status = (string)reader["Status"];
                    IsCompleted = (bool)reader["IsCompleted"];
                }
                reader.Close();
            }
        }
    }

}
