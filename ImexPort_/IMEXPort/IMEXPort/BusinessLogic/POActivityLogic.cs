using IMEXPort.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;


namespace IMEXPort.BusinessLogic
{
    public class POActivityLogic
    {
        public static int Insert(POActivity POA)
        {
            string query = "INSERT INTO POActivity VALUES(@PurchaseOrderID,@ActivityType,@ActivityDate,@StaffID,@SupplierID,@Remarks,@FCAID,@ICAID)";
            List<SqlParameter> parameters = new List<SqlParameter>();

            parameters.Add(new SqlParameter("@PurchaseOrderID", POA.PurchaseOrderID));
            parameters.Add(new SqlParameter("@ActivityType", POA.ActivityType));
            parameters.Add(new SqlParameter("@ActivityDate", POA.ActivityDate));
            parameters.Add(new SqlParameter("@StaffID", POA.StaffID));
			parameters.Add(new SqlParameter("@SupplierID", POA.SupplierID));
			parameters.Add(new SqlParameter("@Remarks", POA.Remarks));
            parameters.Add(new SqlParameter("@FCAID", POA.FCAID));
            parameters.Add(new SqlParameter("@ICAID", POA.ICAID));
            return DBHelper.ModifyData(query, parameters);
        }

        public static int Update(POActivity POA)
        {
            string query = "UPDATE POActivity SET  PurchaseOrderID = @PurchaseOrderID, ActivityType = @ActivityType,ActivityDate = @ActivityDate,StaffID = @StaffID,SupplierID = @SupplierID,Remarks = @Remarks,FCAID=@FCAID,ICAID=@ICAID WHERE POActivityID = @POActivityID";
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@POActivityID", POA.POActivityID));
            parameters.Add(new SqlParameter("@PurchaseOrderID", POA.PurchaseOrderID));
            parameters.Add(new SqlParameter("@ActivityType", POA.ActivityType));
            parameters.Add(new SqlParameter("@ActivityDate", POA.ActivityDate));
            parameters.Add(new SqlParameter("@StaffID", POA.StaffID));
			parameters.Add(new SqlParameter("@SupplierID", POA.SupplierID));
			parameters.Add(new SqlParameter("@Remarks", POA.Remarks));
            parameters.Add(new SqlParameter("@FCAID", POA.FCAID));
            parameters.Add(new SqlParameter("@ICAID", POA.ICAID));
            return DBHelper.ModifyData(query, parameters);
        }

        public static POActivity SelectByPK(int ID)
        {
            string query = "SELECT * FROM POActivity WHERE POActivityID = @ID";
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@ID", ID));
            DataTable dt = DBHelper.SelectData(query, parameters);

            POActivity POA = new POActivity();
            POA.POActivityID = ID;
            POA.PurchaseOrderID = Convert.ToInt32(dt.Rows[0]["PurchaseOrderID"].ToString());
            POA.ActivityType = dt.Rows[0]["ActivityType"].ToString();
            POA.ActivityDate = Convert.ToDateTime(dt.Rows[0]["ActivityDate"].ToString());
            POA.StaffID = Convert.ToInt32(dt.Rows[0]["StaffID"].ToString());
			POA.SupplierID = Convert.ToInt32(dt.Rows[0]["SupplierID"].ToString());
			POA.Remarks = dt.Rows[0]["Remarks"].ToString();
            POA.FCAID = Convert.ToInt32(dt.Rows[0]["FCAID"].ToString());
            POA.ICAID = Convert.ToInt32(dt.Rows[0]["ICAID"].ToString());

            return POA;
        }

        public static int Delete(int ID)
        {
            string query = "DELETE POItem WHERE POActivityID = @ID";
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@ID", ID));
            return DBHelper.ModifyData(query, parameters);
        }



        public static DataTable SelectALL()
        {
            string query = "SELECT * FROM POActivity";
            List<SqlParameter> parameters = new List<SqlParameter>();
            return DBHelper.SelectData(query, parameters);
        }

        public static DataTable SelectByPOrderID(int ID)
        {
            string query = "SELECT * FROM POActivity WHERE POActivityID= @ID";
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@ID", ID));
            return DBHelper.SelectData(query, parameters);
        }

        public static int DeleteByPOID(int ID)
        {
            string query = "DELETE POActivity WHERE POActivityID = @ID";
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@ID", ID));
            return DBHelper.ModifyData(query, parameters);

        }
    }
}