using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTask
{
    internal class Comment
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Author { get; set; }
    
        public void AddComment(Comment comment)
        {
            using (var db = new DataBase())
            {
                db.Open();
                string query = "INSERT INTO Comments (Text, CreatedAt, Author) VALUES (@Text, @CreatedAt, @Author)";
                SqlCommand command = new SqlCommand(query, db.connection);
                command.Parameters.AddWithValue("@Text", comment.Text);
                command.Parameters.AddWithValue("@CreatedAt", comment.CreatedAt);
                command.Parameters.AddWithValue("@Author", comment.Author);
                command.ExecuteNonQuery();
            }
        }
        public void DeleteComment(int commentId)
        {
            using (var db = new DataBase())
            {
                db.Open();
                string query = "DELETE FROM Comments WHERE Id = @Id";
                SqlCommand command = new SqlCommand(query, db.connection);
                command.Parameters.AddWithValue("@Id", commentId);
                command.ExecuteNonQuery();
            }
        }
        public void EditComment(Comment comment)
        {
            using (var db = new DataBase())
            {
                db.Open();
                string query = "UPDATE Comments SET Text = @Text, CreatedAt = @CreatedAt, Author = @Author WHERE Id = @Id";
                SqlCommand command = new SqlCommand(query, db.connection);
                command.Parameters.AddWithValue("@Text", comment.Text);
                command.Parameters.AddWithValue("@CreatedAt", comment.CreatedAt);
                command.Parameters.AddWithValue("@Author", comment.Author);
                command.Parameters.AddWithValue("@Id", comment.Id);
                command.ExecuteNonQuery();
            }
        }
        public Comment GetCommentDetails(int commentId)
        {
            using (var db = new DataBase())
            {
                db.Open();
                string query = "SELECT * FROM Comments WHERE Id = @Id";
                SqlCommand command = new SqlCommand(query, db.connection);
                command.Parameters.AddWithValue("@Id", commentId);
                SqlDataReader reader = command.ExecuteReader();
                Comment comment = null;
                if (reader.Read())
                {
                    comment = new Comment
                    {
                        Id = reader.GetInt32(0),
                        Text = reader.GetString(1),
                        CreatedAt = reader.GetDateTime(2),
                        Author = reader.GetString(3)
                    };
                }
                reader.Close();
                return comment;
            }
            
        }
    }
}
