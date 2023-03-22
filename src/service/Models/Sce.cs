using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace WorkerOE.Models
{
   public  class Sce
    {
        internal static int conectarSce(string WpOrdenExterna1, string WpAlmacen, string _connectionStringDBSCEE)
        {
            string sql = "select EXTERNORDERKEY,WHSEID,STATUS FROM [LPNFD].[" + WpAlmacen + "].[ORDERS] WHERE EXTERNORDERKEY = '" + WpOrdenExterna1 + "'";
            int count = 0;
            using (SqlConnection connection = new SqlConnection(_connectionStringDBSCEE))
            //using (SqlConnection connection = new SqlConnection(@"Data Source=DBSCEFARMATEST;Initial catalog=LPNFD;Integrated Security=true"))
            {
                connection.Open();
               
                SqlCommand cmd = new SqlCommand(sql, connection);
                SqlDataReader reader = cmd.ExecuteReader();                
                try
                {
                    while (reader.Read())
                    {
                        count++;
                        Console.WriteLine(String.Format("EXTERNORDERKEY: {0},WHSEID: {1},STATUS: {2}",
                      reader["EXTERNORDERKEY"], reader["WHSEID"], reader["STATUS"]));// etc
                    }
                }
                finally
                {
                    // Always call Close when done reading.
                    reader.Close();
                }
            }
            return count;
        }

    }
}
