using IMEXPort.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace IMEXPort.BusinessLogic
{
	public class DepartmentLogic
	{
		
		public static int Insert(Department D)
		{
			string query = "INSERT INTO Department VALUES(@DeptName)";
			List<SqlParameter> parameters = new List<SqlParameter>();
			parameters.Add(new SqlParameter("@DeptName", D.DeptName));
			return DBHelper.ModifyData(query, parameters);
		}

		public static int Update(Department D)
		{
			string query = "UPDATE Department SET DeptName = @DeptName WHERE DepartmentID = @DepartmentID";
			List<SqlParameter> parameters = new List<SqlParameter>();
			parameters.Add(new SqlParameter("@DeptName", D.DeptName));
			parameters.Add(new SqlParameter("@DepartmentID", D.DepartmentID));
			return DBHelper.ModifyData(query, parameters);
		}

		public static int Delete(int ID)
		{
			string query = "DELETE Department WHERE DepartmentID = @ID";
			List<SqlParameter> parameters = new List<SqlParameter>();
			parameters.Add(new SqlParameter("@ID", ID));
			return DBHelper.ModifyData(query, parameters);
		}

		public static DataTable SelectALL()
		{
			string query = "SELECT * FROM Department";
			List<SqlParameter> parameters = new List<SqlParameter>();
			return DBHelper.SelectData(query, parameters);
		}

		public static Department SelectByPK(int ID)
		{
			string query = "SELECT * FROM Department WHERE DepartmentID = @ID";
			List<SqlParameter> parameters = new List<SqlParameter>();
			parameters.Add(new SqlParameter("@ID", ID));
			DataTable dt = DBHelper.SelectData(query, parameters);

			Department D = new Department();
			D.DepartmentID = ID;
			D.DeptName = dt.Rows[0]["DeptName"].ToString();
			

			return D;
		}
	}
}