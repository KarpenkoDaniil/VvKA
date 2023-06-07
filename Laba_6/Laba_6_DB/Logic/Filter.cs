using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Logic
{
    public class Filter
    {
        string path;
        public Filter(string path) { this.path = path; }

        private DataTable AbsFilter(string command, string tableName)
        {
            DataTable dt = new DataTable(tableName);
            using (SqlConnection connection = new SqlConnection(path))
            {
                SqlCommand query = new SqlCommand(command, connection);

                SqlDataAdapter adapter = new SqlDataAdapter(query);

                adapter.Fill(dt);
            }
            return dt;
        }

        public DataTable FilterTable(List<string> columnsName, string[] filtered, string tableName)
        {
            int count = 0;
            string command = "SELECT ";
            for (int i = 0; i < columnsName.Count; i++)
            {
                if (columnsName[i] != "")
                {
                    if (columnsName.Count - 1 != i)
                    {
                        command = command + " " + columnsName[i] + ", ";
                    }
                    else
                    {
                        command = command + " " + columnsName[i] + " ";
                    }
                    count++;
                }
                
            }

            command = command + " FROM " + tableName;

            if (filtered.Length > 0)
            {
                int j = 0;
                
                for (int i = 0; i < columnsName.Count; i++)
                {
                    if (filtered[i] != "")
                    {
                        command = command + " WHERE ";
                        break;
                    }
                }
                double e;
                int k;
                for (int i = 0; i < columnsName.Count; i++)
                {
                    if (filtered[i] != "")
                    {
                        if (int.TryParse(columnsName[i], out k) || double.TryParse(columnsName[i], out e))
                        {
                            if (j > 0 && j != filtered.Length - 1)
                            {
                                command = command + " AND ";
                            }
                            command = command + columnsName[i] + " = " + filtered[i];
                            j++;
                        }
                        else
                        {
                            if (j > 0 && j != filtered.Length - 1)
                            {
                                command = command + " AND ";
                            }
                            command = command + columnsName[i] + " = \'" + filtered[i] + "\'";
                            j++;
                        }
                    }
                }
            }

            if(count != 0)
            return AbsFilter(command, tableName);
            else
            return null;
        }
    }
}
