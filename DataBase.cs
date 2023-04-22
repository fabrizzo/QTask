using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace QTask
{
    internal class DataBase : IDisposable
    {
        public SqlConnection connection;
        public DataBase()
        {
            string connectionString = "Server=DESKTOP - KK511UU;Database=QTask;Integrated Security = True;";
            connection = new SqlConnection(connectionString);
        }
        public void Open()
        {
            connection.Open();
        }

        public void Close()
        {
            connection.Close();
        }
        public void Dispose()
        {
            connection.Dispose();
        }
    }
}
