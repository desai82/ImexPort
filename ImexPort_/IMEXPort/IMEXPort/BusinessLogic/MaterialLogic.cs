using IMEXPort.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace IMEXPort.BusinessLogic
{
	public class MaterialLogic
	{

		public static int Insert(Material M)
		{
			string query = "INSERT INTO Material VALUES(@MaterialName,@MaterialCode,@StaffID)";
			List<SqlParameter> parameters = new List<SqlParameter>();
			parameters.Add(new SqlParameter("@MaterialName", M.MaterialName));
			parameters.Add(new SqlParameter("@MaterialCode", M.MaterialCode));
			parameters.Add(new SqlParameter("@StaffID", M.StaffID));
			return DBHelper.ModifyData(query, parameters);
		}

		public static int Update(Material M)
		{
			string query = "UPDATE Material SET MaterialName = @MaterialName,MaterialCode=@MaterialCode,StaffID=@StaffID WHERE MaterialID = @MaterialID";
			List<SqlParameter> parameters = new List<SqlParameter>();
			parameters.Add(new SqlParameter("@MaterialName", M.MaterialName));
			parameters.Add(new SqlParameter("@MaterialCode", M.MaterialCode));
			parameters.Add(new SqlParameter("@StaffID", M.StaffID));
			parameters.Add(new SqlParameter("@MaterialID", M.MaterialID));
			return DBHelper.ModifyData(query, parameters);
		}

		public static int Delete(int ID)
		{
			string query = "DELETE Material WHERE MaterialID = @ID";
			List<SqlParameter> parameters = new List<SqlParameter>();
			parameters.Add(new SqlParameter("@ID", ID));
			return DBHelper.ModifyData(query, parameters);
		}

		public static DataTable SelectALL()
		{
			string query = "SELECT * FROM Material";
			List<SqlParameter> parameters = new List<SqlParameter>();
			return DBHelper.SelectData(query, parameters);
		}

		public static Material SelectByPK(int ID)
		{
			string query = "SELECT * FROM Material WHERE MaterialID = @ID";
			List<SqlParameter> parameters = new List<SqlParameter>();
			parameters.Add(new SqlParameter("@ID", ID));
			DataTable dt = DBHelper.SelectData(query, parameters);

			Material M = new Material();
			M.MaterialID = ID;
			M.MaterialName = dt.Rows[0]["MaterialName"].ToString();
			M.MaterialCode = dt.Rows[0]["MaterialCode"].ToString();
			M.StaffID = Convert.ToInt32(dt.Rows[0]["StaffID"].ToString());


			return M;
		}

		public static DataTable SelectBySearch(string Name)
		{
			string query = "SELECT * FROM Material WHERE MaterialName LIKE '%'+@Name+'%' ";
			List<SqlParameter> parameters = new List<SqlParameter>();
			parameters.Add(new SqlParameter("@Name", Name));
			
			
			return DBHelper.SelectData(query, parameters);
		}

		public static DataTable SelectSOM(int ID)
		{

			string query = "SELECT Name FROM Supplier s1 INNER JOIN MaterialSupplier s2 ON s1.SupplierID=s2.SupplierID where s2.MaterialID=@ID ";
			List<SqlParameter> parameters = new List<SqlParameter>();
			parameters.Add(new SqlParameter("@ID", ID));
			return DBHelper.SelectData(query, parameters);

		}

		public static int SelectByMaterialName(String MaterialName)
		{
			string query = "SELECT MaterialID FROM Material WHERE MaterialName=@MaterialName";
			List<SqlParameter> parameters = new List<SqlParameter>();
			parameters.Add(new SqlParameter("@MaterialName", MaterialName));
			DataTable dt = DBHelper.SelectData(query, parameters);

			int MaterialID = Convert.ToInt32(dt.Rows[0]["MaterialID"].ToString());
			
			return MaterialID;
		}


	}
}