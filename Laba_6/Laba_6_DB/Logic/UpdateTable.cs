using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public class UpdateTable
    {
        string path;
        public UpdateTable(string path) { this.path = path; }

        private void AbsUpdateTable(string command)
        {
            using (SqlConnection connection = new SqlConnection(path))
            {
                SqlCommand query = new SqlCommand(command, connection);
                connection.Open();
                query.ExecuteNonQuery();
            }
        }

        public void UpdateTables(List<string> data, string nameTable, List<string> nameColumns)
        {
            string command = "UPDATE " + nameTable + " SET ";
            for (int i = 0; i < data.Count && data[i] != ""; i++)
            {
                if (int.TryParse(data[i], out int result) || (double.TryParse(data[i], out double res)))
                {
                    if (data.Count - 1 == i)
                    {
                        command = command + nameColumns[i] + " = " + data[i] + " ";
                        command = command + " WHERE " + nameColumns[0] + " = " + data[0];
                        AbsUpdateTable(command);
                    }
                    else
                    {
                        if(i != 0)
                        command = command + nameColumns[i] + " = " + data[i] + ", ";
                    }
                }
                else
                {
                    if (data.Count - 1 == i)
                    {
                        command = command + nameColumns[i] + " = \'" + data[i] + "\' ";
                        command = command + " WHERE " + nameColumns[0] + " = " + data[0];
                        AbsUpdateTable(command);
                    }
                    else
                    {
                        if(i != 0)
                        command = command + nameColumns[i] + " = \'" + data[i] + "\', ";
                    }
                }
            }
        }
    }
}
