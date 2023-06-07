using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public class DeleterRow
    {
        string path;
        public DeleterRow(string path) { this.path = path; }

        private void AbsDeleteRow(string command)
        {
            using (SqlConnection connection = new SqlConnection(path))
            {
                SqlCommand query = new SqlCommand(command, connection);
                connection.Open();
                query.ExecuteNonQuery();
            }
        }

        public void DeleteFormArivalsDispatch(int id)
        {
            string command = "DELETE FROM ArivalsDispatch WHERE ArivalDispatchID = " + id;
            AbsDeleteRow(command);
        }

        public void DeleteFormEmployes(int id)
        {
            string command = "DELETE FROM Employes WHERE EmployesID = " + id;
            AbsDeleteRow(command);
        }

        public void DeleteFormFlights(int id)
        {
            string command = "DELETE FROM Flights WHERE FlightID = " + id;
            AbsDeleteRow(command);
        }

        public void DeleteFormPlanes(int id)
        {
            string command = "DELETE FROM Planes WHERE PlaneID = " + id;
            AbsDeleteRow(command);
        }

        public void DeleteFormPlaneTickets(int id)
        {
            string command = "DELETE FROM PlaneTickets WHERE TiketID = " + id;
            AbsDeleteRow(command);
        }

        public void DeleteFormPosts(int id)
        {
            string command = "DELETE FROM Posts WHERE PostID = " + id;
            AbsDeleteRow(command);
        }

        public void DeleteFormTypeOfPlanes(int id)
        {
            string command = "DELETE FROM TypeOfPlanes WHERE TypeOfPlaneID = " + id;
            AbsDeleteRow(command);
        }
    }
}
