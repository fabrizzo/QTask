using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace QTask
{
    internal class Team
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<string> Members { get; set; }
        public List<Project> Projects { get; set; }
        public void AddTeam(Team team)
        {
            using (var db = new DataBase())
            {
                db.Open();

                var query = "INSERT INTO Teams (Name) VALUES (@Name); SELECT SCOPE_IDENTITY();";
                var command = new SqlCommand(query, db.connection);
                command.Parameters.AddWithValue("@Name", team.Name);

                var teamId = Convert.ToInt32(command.ExecuteScalar());
                team.Id = teamId;
            }
        }
        public void DeleteTeam(int teamId)
        {
            using (var db = new DataBase())
            {
                db.Open();

                var query = "DELETE FROM Teams WHERE Id = @Id";
                var command = new SqlCommand(query, db.connection);
                command.Parameters.AddWithValue("@Id", teamId);
                command.ExecuteNonQuery();
            }
        }
        public Team GetTeamDetails(int teamId)
        {
            using (var db = new DataBase())
            {
                db.Open();

                var query = "SELECT * FROM Teams WHERE Id = @Id";
                var command = new SqlCommand(query, db.connection);
                command.Parameters.AddWithValue("@Id", teamId);

                using (var reader = command.ExecuteReader())
                {
                    if (!reader.Read())
                        return null;

                    var team = new Team
                    {
                        Id = reader.GetInt32(reader.GetOrdinal("Id")),
                        Name = reader.GetString(reader.GetOrdinal("Name")),
                        Members = new List<string>(),
                        Projects = new List<Project>()
                    };

                    reader.Close();

                    query = "SELECT * FROM TeamMembers WHERE TeamId = @TeamId";
                    command = new SqlCommand(query, db.connection);
                    command.Parameters.AddWithValue("@TeamId", teamId);

                    using (var membersReader = command.ExecuteReader())
                    {
                        while (membersReader.Read())
                        {
                            team.Members.Add(membersReader.GetString(membersReader.GetOrdinal("Member")));
                        }
                    }

                    query = "SELECT * FROM Projects WHERE TeamId = @TeamId";
                    command = new SqlCommand(query, db.connection);
                    command.Parameters.AddWithValue("@TeamId", teamId);

                    using (var projectsReader = command.ExecuteReader())
                    {
                        while (projectsReader.Read())
                        {
                            var project = new Project
                            {
                                Id = projectsReader.GetInt32(projectsReader.GetOrdinal("Id")),
                                Name = projectsReader.GetString(projectsReader.GetOrdinal("Name"))
                            };

                            team.Projects.Add(project);
                        }
                    }

                    return team;
                }
            }
        }
        public void AddUserToTeam(int teamId, string memberName)
        {
            using (var db = new DataBase())
            {
                db.Open();

                var query = "INSERT INTO TeamMembers (TeamId, Member) VALUES (@TeamId, @Member)";
                var command = new SqlCommand(query, db.connection);
                command.Parameters.AddWithValue("@TeamId", teamId);
                command.Parameters.AddWithValue("@Member", memberName);
                command.ExecuteNonQuery();
            }
        }
    }


}
