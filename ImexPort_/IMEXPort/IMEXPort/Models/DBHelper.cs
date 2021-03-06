
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace IMEXPort.Models
{ 
	public class DBHelper
	{
		public static int ModifyData(string query, List<SqlParameter> parameters)
		{
			SqlConnection connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|ImexDB.mdf; Integrated Security = True");

            SqlCommand command = new SqlCommand(query, connection);
			for (int i = 0; i < parameters.Count; i++)
			{
				command.Parameters.Add(parameters[i]);
			}
			connection.Open();
			int x = command.ExecuteNonQuery();
			connection.Close();

			return x;
		}

		public static DataTable SelectData(string query, List<SqlParameter> parameters)
		{
            SqlConnection connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|ImexDB.mdf; Integrated Security = True");
            SqlCommand command = new SqlCommand(query, connection);
			for (int i = 0; i < parameters.Count; i++)
			{
				command.Parameters.Add(parameters[i]);
			}
			SqlDataAdapter adapter = new SqlDataAdapter(command);
			DataTable dt = new DataTable();
			connection.Open();
			adapter.Fill(dt);
			connection.Close();

			return dt;
		}
	}
}



