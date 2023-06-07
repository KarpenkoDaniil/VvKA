using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public class ShowTables
    {
        string path;
        public ShowTables(string path) { this.path = path; }

        private DataTable AbsTable(string tableName, string command)
        {
            DataTable dt = new DataTable(tableName);
            using (SqlConnection connection = new SqlConnection(path))
            {
                SqlCommand query = new SqlCommand(command, connection);

                // создаем объект SqlDataAdapter для заполнения объекта DataTable данными из базы данных
                SqlDataAdapter adapter = new SqlDataAdapter(query);

                adapter.Fill(dt);
            }
            return dt;
        }

        public DataTable ShowTkitTable()
        {
            string command = "SELECT * FROM PlaneTickets";
            string tableName = "PlaneTickets";
            return AbsTable(tableName, command);
        }

        public DataTable ShowPostsTable()
        {
            string command = "SELECT * FROM Posts";
            string tableName = "Posts";
            return AbsTable(tableName, command);
        }

        public DataTable ShowTypeOfPlanesTable()
        {
            string command = "SELECT * FROM TypeOfPlanes";
            string tableName = "TypeOfPlanes";
            return AbsTable(tableName, command);
        }

        public DataTable ShowPlanesTable()
        {
            string command = "SELECT * FROM Planes";
            string tableName = "Planes";
            return AbsTable(tableName, command);
        }

        public DataTable ShowFlightsTable()
        {
            string command = "SELECT * FROM Flights";
            string tableName = "Flights";
            return AbsTable(tableName, command);
        }

        public DataTable ShowArivalsDispatchTable()
        {
            string command = "SELECT * FROM ArivalsDispatch";
            string tableName = "ArivalsDispatch";
            return AbsTable(tableName, command);
        }

        public DataTable ShowEmployesTable()
        {
            string command = "SELECT * FROM Employes";
            string tableName = "Employes";
            return AbsTable(tableName, command);
        }
    }
}
