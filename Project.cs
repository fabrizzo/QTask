using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTask
{
    internal class Project
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<Task> Tasks { get; set; }
    
        public void AddProject()
        {
            using (var db = new DataBase())
            {
                db.Open();
                // выполните SQL-запрос на добавление проекта в таблицу базы данных
                db.Close();
            }
        }
        public void DeleteProject()
        {
            using (var db = new DataBase())
            {
                db.Open();
                // выполните SQL-запрос на удаление проекта из таблицы базы данных
                db.Close();
            }
        }
        public void UpdateProjectDetails()
        {
            using (var db = new DataBase())
            {
                db.Open();
                // выполните SQL-запрос на обновление информации о проекте в таблице базы данных
                db.Close();
            }
        }
        public void GetProjectDetails()
        {
            using (var db = new DataBase())
            {
                db.Open();
                // выполните SQL-запрос на получение информации о проекте из таблицы базы данных
                db.Close();
            }
        }
        // назначение пользователя на проект
        public void AssignUserToProject()
        {
            using (var db = new DataBase())
            {
                db.Open();
                // выполните SQL-запрос на назначение пользователя на проект в таблице базы данных
                db.Close();
            }
        }
    }
}
