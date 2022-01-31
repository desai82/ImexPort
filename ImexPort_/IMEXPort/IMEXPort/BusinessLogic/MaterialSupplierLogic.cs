using IMEXPort.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace IMEXPort.BusinessLogic
{
	public class MaterialSupplierLogic
	{
		
		public static int Insert(MaterialSupplier MS)
		{
			string query = "INSERT INTO MaterialSupplier VALUES(@MaterialID,@SupplierID)";
			List<SqlParameter> parameters = new List<SqlParameter>();
			parameters.Add(new SqlParameter("@MaterialID",MS.MaterialID));
			parameters.Add(new SqlParameter("@SupplierID", MS.SupplierID));
			return DBHelper.ModifyData(query, parameters);
		}

		public static int Delete(int MaterialID,int SupplierID)
		{
			string query = "DELETE MaterialSupplier WHERE MaterialID = @MaterialID AND SupplierID = @SupplierID";
			List<SqlParameter> parameters = new List<SqlParameter>();
			parameters.Add(new SqlParameter("@MaterialID", MaterialID));
			parameters.Add(new SqlParameter("@SupplierID", SupplierID));
			return DBHelper.ModifyData(query, parameters);
		}


		

		public static DataTable SelectALL()
		{
			string query = "SELECT * FROM MaterialSupplier";
			List<SqlParameter> parameters = new List<SqlParameter>();
			return DBHelper.SelectData(query, parameters);
		}

		
	}
}