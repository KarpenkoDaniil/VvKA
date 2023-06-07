using System.Collections.Generic;
using System.Data.SqlClient;

namespace Logic
{
    public class CreaterOfRows
    {
        string path;
        public CreaterOfRows(string path) { this.path = path; }

        private void AbsCreateRowInTable(string command)
        {
            using (SqlConnection connection = new SqlConnection(path))
            {
                SqlCommand query = new SqlCommand(command, connection);
                connection.Open();
                query.ExecuteNonQuery();
            }
        }

        public void CreateRowTable(List<string> data, string nameTable, List<string> nameColumns)
        {
            string command = "INSERT INTO " + nameTable + " ( ";
            for (int i = 1; i < nameColumns.Count; i++)
            {
                if (nameColumns.Count - 1 != i)
                    command = command + " " + nameColumns[i] + ", ";
                else
                    command = command + " " + nameColumns[i] + " ";

            }

            command = command  + " ) VALUES ( ";

            for (int i = 0; i < data.Count && data[i] != ""; i++)
            {
                if (int.TryParse(data[i], out int result) || (double.TryParse(data[i], out double res)))
                {
                    if (data.Count - 1 == i)
                    {
                        command = command + " " + data[i] + " )";

                    }
                    else
                    {
                        command = command + " " + data[i] + ", ";
                    }
                }
                else
                {
                    if (data.Count - 1 == i)
                    {
                        command = command + " \'" + data[i] + "\' )";

                    }
                    else
                    {
                        command = command + " \'" + data[i] + "\', ";
                    }
                }
            }

            AbsCreateRowInTable(command);
        }
    }
}
