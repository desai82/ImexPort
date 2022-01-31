using IMEXPort.BusinessLogic;
using IMEXPort.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using System.Data;
using System.Web.Script.Serialization;
using System.Net.Mail;
using System.IO;

namespace IMEXPort.Controllers
{
	public class MobileAppController : Controller
	{

		public ActionResult Login()
		{
			CustomsAgent C = CustomsAgentLogic.SelectByUnPw(Request.Params["Username"], Request.Params["Password"]);
			ViewBag.CustomAgentID = C.CustomsAgentID;
			if (C.CustomsAgentID > 0)
			{
				return Json(C, JsonRequestBehavior.AllowGet);
			}
			else
			{
				return Content("0");
			}
		}

		public ActionResult ProfileCA()
		{
			CustomsAgent C = CustomsAgentLogic.SelectByPK(Convert.ToInt32(Request.Params["ID"]));

			return Json(C, JsonRequestBehavior.AllowGet);
		}

		public ActionResult FetchPODetails()
		{
			DataTable dtPO = PurchaseOrderLogic.SelectByPKdt(Convert.ToInt32(Request.Params["ID"]));
			string strPO = DataTabletoJSON(dtPO);

			DataTable dtPOI = POItemLogic.SelectByPOrderID(Convert.ToInt32(Request.Params["ID"]));
			string strPOI = DataTabletoJSON(dtPOI);

			return Content(strPO + "#####--#####" + strPOI);
		}

		public ActionResult WaitingPOFCA()
		{
			DataTable dt = PurchaseOrderLogic.SelectByStatusForFCA(Request.Params["Status"], Convert.ToInt32(Request.Params["ID"]));
			string WaitingPOFCA = DataTabletoJSON(dt);
			return Content(WaitingPOFCA);
		}
		public ActionResult WaitingPOICA()
		{
			DataTable dt = PurchaseOrderLogic.SelectByStatusForICA(Request.Params["Status"], Convert.ToInt32(Request.Params["ID"]));
			string WaitingPOICA = DataTabletoJSON(dt);
			return Content(WaitingPOICA);
		}

		public ActionResult ScanReceivedPOFCA()
		{
			DataTable dt = PurchaseOrderLogic.SelectScanReceivedPOFCA(Convert.ToInt32(Request.Params["POID"]), Convert.ToInt32(Request.Params["FCAID"]));
			if (dt.Rows.Count > 0)
			{
				PurchaseOrder PO = PurchaseOrderLogic.SelectByPK(Convert.ToInt32(Request.Params["POID"]));
				PO.Status = "FCA Received";
				PO.InvoiceFile = "";
				PO.ExportClearanceFile = "";
				PO.ImportDutyChallanFile = "";
				PO.ImportDutyPaymentFile = "";
				PO.ImportClearanceFile = "";
				PurchaseOrderLogic.Update(PO);

				POActivity POA = new POActivity();
				POA.ActivityDate = DateTime.Now;
				POA.ActivityType = "FCA Received";
				POA.PurchaseOrderID = PO.PurchaseOrderID;
				POA.SupplierID = PO.SupplierID;
				POA.Remarks = "Received By FCA";


				POActivityLogic.Insert(POA);
			}
			string ScanReceivedPOFCA = DataTabletoJSON(dt);
			return Content(ScanReceivedPOFCA + "#####--#####" + "Success");
		}

		public ActionResult ScanReceivedPOICA()
		{
			DataTable dt = PurchaseOrderLogic.SelectScanReceivedPOICA(Convert.ToInt32(Request.Params["POID"]), Convert.ToInt32(Request.Params["ICAID"]));
			if (dt.Rows.Count > 0)
			{
				PurchaseOrder PO = PurchaseOrderLogic.SelectByPK(Convert.ToInt32(Request.Params["POID"]));
				PO.Status = "ICA Arrived";
				PO.InvoiceFile = "";
				PO.ExportClearanceFile = "";
				PO.ImportDutyChallanFile = "";
				PO.ImportDutyPaymentFile = "";
				PO.ImportClearanceFile = "";
				PurchaseOrderLogic.Update(PO);

				POActivity POA = new POActivity();
				POA.ActivityDate = DateTime.Now;
				POA.ActivityType = "ICA Arrived";
				POA.PurchaseOrderID = PO.PurchaseOrderID;
				POA.SupplierID = PO.SupplierID;
				POA.Remarks = "Received By ICA";


				POActivityLogic.Insert(POA);
			}
			string ScanReceivedPOFCA = DataTabletoJSON(dt);
			return Content(ScanReceivedPOFCA + "#####--#####" + "Success");
		}

		public ActionResult ReceivedPOFCA()
		{
			DataTable dt = PurchaseOrderLogic.SelectByStatusForFCA(Request.Params["Status"], Convert.ToInt32(Request.Params["ID"]));
			string ReceivedPOFCA = DataTabletoJSON(dt);
			return Content(ReceivedPOFCA);
		}
		public ActionResult ReceivedPOICA()
		{
			DataTable dt = PurchaseOrderLogic.SelectByStatusForICA(Request.Params["Status"], Convert.ToInt32(Request.Params["ID"]));
			string ReceivedPOICA = DataTabletoJSON(dt);
			return Content(ReceivedPOICA);
		}

		public ActionResult CompletedPOFCA()
		{
			DataTable dt = PurchaseOrderLogic.SelectByStatusForFCA(Request.Params["Status"], Convert.ToInt32(Request.Params["ID"]));
			string ReceivedPOFCA = DataTabletoJSON(dt);
			return Content(ReceivedPOFCA);
		}
		public ActionResult CompletedPOICA()
		{
			DataTable dt = PurchaseOrderLogic.SelectByStatusForICA(Request.Params["Status"], Convert.ToInt32(Request.Params["ID"]));
			string ReceivedPOICA = DataTabletoJSON(dt);
			return Content(ReceivedPOICA);
		}

		public ActionResult ForgetPswdCustomsAgent()
		{
			string Email = Request.Params["Email"];
			DataTable CusAg = CustomsAgentLogic.ForgetPasswordCusag(Email);
			
			MailMessage mailMessage = new MailMessage("query.imexport@gmail.com", CusAg.Rows[0]["Email"].ToString());
			mailMessage.Subject = "Forgot password";
			mailMessage.Body = "Your username is:" + CusAg.Rows[0]["Username"] + "\n" +
							   "Your password is:" + CusAg.Rows[0]["Password"];

			SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);
			smtpClient.Credentials = new System.Net.NetworkCredential()
			{
				UserName = "query.imexport@gmail.com",
				Password = "nisarglovesemail"
			};
			smtpClient.EnableSsl = true;
			smtpClient.Send(mailMessage);
			return Content("Success");
		}







		public static string DataTabletoJSON(DataTable dt)
		{
			JavaScriptSerializer jss = new JavaScriptSerializer();
			Dictionary<string, object> row = null;
			List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
			foreach (DataRow dr in dt.Rows)
			{
				row = new Dictionary<string, object>();
				foreach (DataColumn dc in dt.Columns)
				{
					if (dc.DataType == typeof(DateTime))
					{
						row.Add(dc.ColumnName, dr[dc] != DBNull.Value ? Convert.ToDateTime(dr[dc]).ToString("yyyy-MM-dd#HH:mm") : "");
					}
					else
					{
						row.Add(dc.ColumnName, dr[dc] != DBNull.Value ? dr[dc] : "");
					}
				}
				rows.Add(row);
			}
			return jss.Serialize(rows);
		}

		public ActionResult PortClearanceDocsFCA()
		{
			String Photo = Request.Params["photoupload"];
			String imagename = SaveImage(Photo);

			PurchaseOrder PO = PurchaseOrderLogic.SelectByPK(Convert.ToInt32(Request.Params["POID"]));
		
			PO.InvoiceFile = "";
			PO.ExportClearanceFile = imagename;
			PO.ImportDutyChallanFile = "";
			PO.ImportDutyPaymentFile = "";
			PO.ImportClearanceFile = "";
			PurchaseOrderLogic.Update(PO);
			return Content("Success");
		}

		public ActionResult PortClearanceDocsICA()
		{
			String Photo = Request.Params["photoupload"];
			String imagename = SaveImage(Photo);

			PurchaseOrder PO = PurchaseOrderLogic.SelectByPK(Convert.ToInt32(Request.Params["POID"]));

			PO.InvoiceFile = "";
			PO.ExportClearanceFile = "";
			PO.ImportDutyChallanFile = imagename;
			PO.ImportDutyPaymentFile = "";
			PO.ImportClearanceFile = "";
			PurchaseOrderLogic.Update(PO);
			return Content("Success");
		}


		public String SaveImage(String ImgStr)
		{
			//  Image image = Base64ToImage(ImgStr);

			String path = "~/AllFiles/PortClearanceDocs/";
			string imageName = DateTime.Now.Ticks.ToString() + ".jpg";
			string imgPath = Server.MapPath(path + imageName);
			Base64ToFile(ImgStr, imgPath);
			// image.Save(imgPath, System.Drawing.Imaging.ImageFormat.Jpeg);
			return imageName;
		}





		public static void Base64ToFile(string base64Str, string imgPath)
		{
			byte[] imageBytes = Convert.FromBase64String(base64Str);
			System.IO.File.WriteAllBytes(imgPath, imageBytes);
		}

        public ActionResult InvoicePdf()
        {

            return Content("Invoice.pdf");
        }

		public ActionResult POReleaseFCA()
		{
			
				PurchaseOrder PO = PurchaseOrderLogic.SelectByPK(Convert.ToInt32(Request.Params["POID"]));
				PO.Status = "FCA Dispatched";
				PO.InvoiceFile = "";
				PO.ExportClearanceFile = "";
				PO.ImportDutyChallanFile = "";
				PO.ImportDutyPaymentFile = "";
				PO.ImportClearanceFile = "";
				PurchaseOrderLogic.Update(PO);

				POActivity POA = new POActivity();
				POA.ActivityDate = DateTime.Now;
				POA.ActivityType = "FCA Dispatched";
				POA.PurchaseOrderID = PO.PurchaseOrderID;
				POA.SupplierID = PO.SupplierID;
				POA.Remarks = "Dispatched By FCA";


				POActivityLogic.Insert(POA);
			
			
			return Content("Success");
		}

		public ActionResult POReleaseICA()
		{

			PurchaseOrder PO = PurchaseOrderLogic.SelectByPK(Convert.ToInt32(Request.Params["POID"]));
			PO.Status = "Import Cleared";
			PO.InvoiceFile = "";
			PO.ExportClearanceFile = "";
			PO.ImportDutyChallanFile = "";
			PO.ImportDutyPaymentFile = "";
			PO.ImportClearanceFile = "";
			PurchaseOrderLogic.Update(PO);

			POActivity POA = new POActivity();
			POA.ActivityDate = DateTime.Now;
			POA.ActivityType = "Import Cleared";
			POA.PurchaseOrderID = PO.PurchaseOrderID;
			POA.SupplierID = PO.SupplierID;
			POA.Remarks = "PO Released  By ICA";


			POActivityLogic.Insert(POA);


			return Content("Success");
		}


	}
}
