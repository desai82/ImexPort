using IMEXPort.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace IMEXPort.BusinessLogic
{
	public class PurchaseOrderLogic
	{
		public static int Insert(PurchaseOrder PO)
		{
			string query = "INSERT INTO PurchaseOrder VALUES(@StaffID,@CreateDate,@SupplierID,@Status,@ApproverID," +
						   "@RequestedDate,@TentativeDate,@InvoiceFile,@FCAID,@ICAID,@FinanceID," +
						   "@ExportClearanceFile,@ImportDutyChallanFile,@ImportDutyPaymentFile,@ImportClearanceFile,@DispatchdatebySupp,@DispatchdatebyFCA,@QRCode); SELECT @@IDENTITY";
			List<SqlParameter> parameters = new List<SqlParameter>();
			parameters.Add(new SqlParameter("@StaffID", PO.StaffID));
			parameters.Add(new SqlParameter("@CreateDate", PO.CreateDate));
			parameters.Add(new SqlParameter("@SupplierID", PO.SupplierID));
			parameters.Add(new SqlParameter("@Status", PO.Status));
			parameters.Add(new SqlParameter("@ApproverID", PO.ApproverID));
			parameters.Add(new SqlParameter("@RequestedDate", PO.RequestedDate));
			parameters.Add(new SqlParameter("@TentativeDate", PO.TentativeDate));
			parameters.Add(new SqlParameter("@InvoiceFile", PO.InvoiceFile));
			parameters.Add(new SqlParameter("@FCAID", PO.FCAID));
			parameters.Add(new SqlParameter("@ICAID", PO.ICAID));
            parameters.Add(new SqlParameter("@FinanceID", PO.FinanceID));
            parameters.Add(new SqlParameter("@ExportClearanceFile", PO.ExportClearanceFile));
			parameters.Add(new SqlParameter("@ImportDutyChallanFile", PO.ImportDutyChallanFile));
			parameters.Add(new SqlParameter("@ImportDutyPaymentFile", PO.ImportDutyPaymentFile));
			parameters.Add(new SqlParameter("@ImportClearanceFile", PO.ImportClearanceFile));
			parameters.Add(new SqlParameter("@DispatchdatebySupp", PO.DispatchdatebySupp));
			parameters.Add(new SqlParameter("@DispatchdatebyFCA", PO.DispatchdatebyFCA));
            parameters.Add(new SqlParameter("@QRCode", PO.QRCode));

            DataTable dt =  DBHelper.SelectData(query, parameters);
			return Convert.ToInt32(dt.Rows[0][0]);
		}

		public static int Update(PurchaseOrder PO)
		{
			string query = "UPDATE PurchaseOrder SET StaffID = @StaffID, CreateDate = @CreateDate, SupplierID = @SupplierID, Status = @Status," +
						   "ApproverID = @ApproverID, RequestedDate = @RequestedDate, TentativeDate = @TentativeDate, InvoiceFile = @InvoiceFile," +
                           "FCAID = @FCAID, ICAID = @ICAID,FinanceID=@FinanceID, ExportClearanceFile = @ExportClearanceFile, ImportDutyChallanFile = @ImportDutyChallanFile," +
                           "ImportDutyPaymentFile = @ImportDutyPaymentFile, ImportClearanceFile = @ImportClearanceFile ,DispatchdatebyFCA = @DispatchdatebyFCA,QRCode=@QRCode,DispatchdatebySupp=@DispatchdatebySupp WHERE PurchaseOrderID = @PurchaseOrderID";
			List<SqlParameter> parameters = new List<SqlParameter>();
			parameters.Add(new SqlParameter("@PurchaseOrderID", PO.PurchaseOrderID));
			parameters.Add(new SqlParameter("@StaffID", PO.StaffID));
			parameters.Add(new SqlParameter("@CreateDate", PO.CreateDate));
			parameters.Add(new SqlParameter("@SupplierID", PO.SupplierID));
			parameters.Add(new SqlParameter("@Status", PO.Status));
			parameters.Add(new SqlParameter("@ApproverID", PO.ApproverID));
			parameters.Add(new SqlParameter("@RequestedDate", PO.RequestedDate));
			parameters.Add(new SqlParameter("@TentativeDate", PO.TentativeDate));
			parameters.Add(new SqlParameter("@InvoiceFile", PO.InvoiceFile));
			parameters.Add(new SqlParameter("@FCAID", PO.FCAID));
			parameters.Add(new SqlParameter("@ICAID", PO.ICAID));
            parameters.Add(new SqlParameter("@FinanceID", PO.FinanceID));
            parameters.Add(new SqlParameter("@ExportClearanceFile", PO.ExportClearanceFile));
			parameters.Add(new SqlParameter("@ImportDutyChallanFile", PO.ImportDutyChallanFile));
			parameters.Add(new SqlParameter("@ImportDutyPaymentFile", PO.ImportDutyPaymentFile));
			parameters.Add(new SqlParameter("@ImportClearanceFile", PO.ImportClearanceFile));
			parameters.Add(new SqlParameter("@DispatchdatebySupp", PO.DispatchdatebySupp));
            parameters.Add(new SqlParameter("@DispatchdatebyFCA", PO.DispatchdatebyFCA));
            parameters.Add(new SqlParameter("@QRCode", PO.QRCode));
            return DBHelper.ModifyData(query, parameters);
		}

		public static PurchaseOrder SelectByPK(int ID)
		{
			string query = "SELECT * FROM PurchaseOrder WHERE PurchaseOrderID = @ID";
			List<SqlParameter> parameters = new List<SqlParameter>();
			parameters.Add(new SqlParameter("@ID", ID));
			DataTable dt = DBHelper.SelectData(query, parameters);
			
			PurchaseOrder PO = new PurchaseOrder();
			PO.PurchaseOrderID = ID;
			PO.StaffID = Convert.ToInt32(dt.Rows[0]["StaffID"].ToString());
			PO.CreateDate = Convert.ToDateTime(dt.Rows[0]["CreateDate"].ToString());
			PO.SupplierID = Convert.ToInt32(dt.Rows[0]["SupplierID"].ToString());
			PO.Status = dt.Rows[0]["Status"].ToString();
			PO.ApproverID =Convert.ToInt32(dt.Rows[0]["ApproverID"].ToString());
			PO.RequestedDate = Convert.ToDateTime(dt.Rows[0]["RequestedDate"].ToString());
			PO.TentativeDate = Convert.ToDateTime(dt.Rows[0]["TentativeDate"].ToString());
			PO.FCAID = Convert.ToInt32(dt.Rows[0]["FCAID"].ToString());
			PO.ICAID = Convert.ToInt32(dt.Rows[0]["ICAID"].ToString());
            PO.FinanceID = Convert.ToInt32(dt.Rows[0]["FinanceID"].ToString());
            PO.ExportClearanceFile = dt.Rows[0]["ExportClearanceFile"].ToString();
			PO.ImportDutyChallanFile = dt.Rows[0]["ImportDutyChallanFile"].ToString();
			PO.ImportDutyPaymentFile = dt.Rows[0]["ImportDutyPaymentFile"].ToString();
			PO.ImportClearanceFile = dt.Rows[0]["ImportClearanceFile"].ToString();
            PO.DispatchdatebySupp = Convert.ToDateTime( dt.Rows[0]["DispatchdatebySupp"].ToString());
            PO.DispatchdatebyFCA = Convert.ToDateTime (dt.Rows[0]["DispatchdatebyFCA"].ToString());
            PO.QRCode = dt.Rows[0]["QRCode"].ToString();

            return PO;
		}

		public static DataTable SelectByPKdt(int ID)
		{
			string query = @"SELECT PO.*, S.Name AS 'SupplierName', ICA.Name AS 'ICAName', FCA.Name AS 'FCAName'
							FROM PurchaseOrder PO
								INNER JOIN Supplier S ON S.SupplierID = PO.SupplierID
								INNER JOIN CustomsAgent ICA ON ICA.CustomsAgentID = PO.ICAID
								INNER JOIN CustomsAgent FCA ON FCA.CustomsAgentID = PO.FCAID
							WHERE PO.PurchaseOrderID = @ID";
			List<SqlParameter> parameters = new List<SqlParameter>();
			parameters.Add(new SqlParameter("@ID", ID));
			return DBHelper.SelectData(query, parameters);

		}


		public static int Delete(int ID)
		{
			string query = "DELETE PurchaseOrder WHERE PurchaseOrderID = @ID";
			List<SqlParameter> parameters = new List<SqlParameter>();
			parameters.Add(new SqlParameter("@ID", ID));
			return DBHelper.ModifyData(query, parameters);
		}

		public static DataTable SelectALL()
		{
			string query = "SELECT * FROM PurchaseOrder";
			List<SqlParameter> parameters = new List<SqlParameter>();
			return DBHelper.SelectData(query, parameters);
		}

		public static DataTable SelectAllPOByMe(int ID)
		{
			string query = "SELECT * FROM PurchaseOrder Where StaffID = @ID ";
			List<SqlParameter> parameters = new List<SqlParameter>();
			parameters.Add(new SqlParameter("@ID", ID));
			return DBHelper.SelectData(query,parameters);
		}

		public static DataTable SelectAllPOForMe(int ID)
		{
			string query = "SELECT * FROM PurchaseOrder Where ApproverID = @ID AND Status ='Submitted for Approval'";
			List<SqlParameter> parameters = new List<SqlParameter>();
			parameters.Add(new SqlParameter("@ID", ID));
			return DBHelper.SelectData(query, parameters);
		}

		public static DataTable SelectByStatusForFCA(string Status,int ID)
		{
			string query = "SELECT * FROM PurchaseOrder Where Status=@Status AND FCAID = @ID ";
			List<SqlParameter> parameters = new List<SqlParameter>();
			parameters.Add(new SqlParameter("@Status", Status));
			parameters.Add(new SqlParameter("@ID", ID));

			return DBHelper.SelectData(query, parameters);
		}
		public static DataTable SelectByStatusForICA(string Status, int ID)
		{
			string query = "SELECT * FROM PurchaseOrder Where Status=@Status AND ICAID = @ID ";
			List<SqlParameter> parameters = new List<SqlParameter>();
			parameters.Add(new SqlParameter("@Status", Status));
			parameters.Add(new SqlParameter("@ID", ID));

			return DBHelper.SelectData(query, parameters);
		}

		public static DataTable SelectScanReceivedPOFCA(int POID ,int FCAID)
		{
			string query = "SELECT * FROM PurchaseOrder WHERE PurchaseOrderID = @POID AND FCAID = @FCAID";
			List<SqlParameter> parameters = new List<SqlParameter>();
			parameters.Add(new SqlParameter("@POID", POID));
			parameters.Add(new SqlParameter("@FCAID", FCAID));
			DataTable dt = DBHelper.SelectData(query, parameters);
			return dt;
		}

		public static DataTable SelectScanReceivedPOICA(int POID, int ICAID)
		{
			string query = "SELECT * FROM PurchaseOrder WHERE PurchaseOrderID = @POID AND ICAID = @ICAID";
			List<SqlParameter> parameters = new List<SqlParameter>();
			parameters.Add(new SqlParameter("@POID", POID));
			parameters.Add(new SqlParameter("@ICAID", ICAID));
			DataTable dt = DBHelper.SelectData(query, parameters);
			return dt;
		}


		public static DataTable SelectForPurchaseNoti(int ID)
        {
            string query = "SELECT * FROM PurchaseOrder Where ApproverID = @ID AND Status ='Submitted for Approval'";
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@ID", ID));
            return DBHelper.SelectData(query, parameters);
        }
        public static DataTable SelectForPurchaseNotiFina(int ID)
        {
            string query = "SELECT * FROM PurchaseOrder Where FinanceID = @ID AND Status ='Import Duty Pending'";
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@ID", ID));
            return DBHelper.SelectData(query, parameters);
        }

    }
}