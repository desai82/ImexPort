using IMEXPort.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace IMEXPort.BusinessLogic
{
	public class POItemLogic
	{
		public static int Insert(POItem PO)
		{
			string query = "INSERT INTO POItem VALUES(@PurchaseOrderID,@MaterialID,@Quantity,@Rate,@Remarks)";
			List<SqlParameter> parameters = new List<SqlParameter>();
			parameters.Add(new SqlParameter("@PurchaseOrderID", PO.PurchaseOrderID));
			parameters.Add(new SqlParameter("@MaterialID", PO.MaterialID));
			parameters.Add(new SqlParameter("@Quantity", PO.Quantity));
			parameters.Add(new SqlParameter("@Rate", PO.Rate));
			parameters.Add(new SqlParameter("@Remarks", PO.Remarks));
			return DBHelper.ModifyData(query, parameters);
		}

		public static int Update(POItem PO)
		{
			string query = "UPDATE POItem SET PurchaseOrderID = @PurchaseOrderID, MaterialID = @MaterialID, Quantity = @Quantity,Rate = @Rate,Remarks = @Remarks WHERE POItemID = @POItemID";
			List<SqlParameter> parameters = new List<SqlParameter>();
			parameters.Add(new SqlParameter("@POItemID", PO.POItemID));
			parameters.Add(new SqlParameter("@PurchaseOrderID", PO.PurchaseOrderID));
			parameters.Add(new SqlParameter("@MaterialID", PO.MaterialID));
			parameters.Add(new SqlParameter("@Quantity", PO.Quantity));
			parameters.Add(new SqlParameter("@Rate", PO.Rate));
			parameters.Add(new SqlParameter("@Remarks", PO.Remarks));

			return DBHelper.ModifyData(query, parameters);
		}

		public static POItem SelectByPK(int ID)
		{
			string query = "SELECT * FROM POItem WHERE POItemID = @ID";
			List<SqlParameter> parameters = new List<SqlParameter>();
			parameters.Add(new SqlParameter("@ID", ID));
			DataTable dt = DBHelper.SelectData(query, parameters);

			POItem PO = new POItem();
			PO.POItemID = ID;
			PO.PurchaseOrderID = Convert.ToInt32(dt.Rows[0]["PurchaseOrderID"].ToString());
			PO.MaterialID = Convert.ToInt32(dt.Rows[0]["MaterialID"].ToString());
			PO.Quantity = Convert.ToInt32(dt.Rows[0]["Quantity"].ToString());
			PO.Rate = Convert.ToInt32(dt.Rows[0]["Rate"].ToString());
			PO.Remarks = dt.Rows[0]["Remarks"].ToString();
			
			return PO;
		}

		public static int Delete(int ID)
		{
			string query = "DELETE POItem WHERE POItemID = @ID";
			List<SqlParameter> parameters = new List<SqlParameter>();
			parameters.Add(new SqlParameter("@ID", ID));
			return DBHelper.ModifyData(query, parameters);
		}



		public static DataTable SelectALL()
		{
			string query = "SELECT * FROM POItem";
			List<SqlParameter> parameters = new List<SqlParameter>();
			return DBHelper.SelectData(query, parameters);
		}

		public static DataTable SelectByPOrderID(int ID)
		{
			string query = @"SELECT POItem.*, M.MaterialName
							FROM POItem 
								INNER JOIN Material M ON M.MaterialID = POItem.MaterialID
							WHERE PurchaseOrderID= @ID";
			List<SqlParameter> parameters = new List<SqlParameter>();
			parameters.Add(new SqlParameter("@ID", ID));
			return DBHelper.SelectData(query, parameters);
		}

		public static int DeleteByPOID(int ID)
		{
			string query = "DELETE POItem WHERE PurchaseOrderID = @ID";
			List<SqlParameter> parameters = new List<SqlParameter>();
			parameters.Add(new SqlParameter("@ID", ID));
			return DBHelper.ModifyData(query, parameters);

		}


	}
}