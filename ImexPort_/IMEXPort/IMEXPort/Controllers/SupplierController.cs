using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using IMEXPort.BusinessLogic;
using IMEXPort.Models;
using QRCoder;
using System.Drawing;
using System.Net.Mail;

namespace IMEXPort.Controllers
{
    public class SupplierController : Controller
    {
        Supplier CURRENT_SUPPLIER = new Supplier();
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            
            if (Session["Supplier"] == null)
            {
                filterContext.Result = RedirectToAction("Login", "Access");
            }
            CURRENT_SUPPLIER = (Supplier)Session["Supplier"];
            ViewBag.CurrentSupplier = CURRENT_SUPPLIER;
            base.OnActionExecuting(filterContext);
        }

        // GET: Supplier
        public ActionResult Home()
        {
			int SupplierID = ViewBag.CurrentSupplier.SupplierID;
			DataTable dt = SupplierLogic.SelectAllPOForSupp(SupplierID);
			DataTable dt1 = SupplierLogic.SelectAllPOOnSupp(SupplierID);
			DataTable dt2 = SupplierLogic.SelectAllPORejectbySupp(SupplierID);
			DataTable dt3 = SupplierLogic.SelectAllPODispatchedbySupp(SupplierID);
			DataTable dt4 = SupplierLogic.SelectAllPODelivered(SupplierID);
			ViewBag.NewPO = dt;
			ViewBag.OngoingPO = dt1;
			ViewBag.RejectedPO = dt2;
			ViewBag.DispatchedPO = dt3;
			ViewBag.DeliveredPO = dt4;


			return View();
        }

		public ActionResult MyProfileSupplier()
        {
            Supplier CurrentSupplier = new Supplier();
            if (Session["Supplier"] == null)
            {
                return RedirectToAction("Login", "Access");
            }
            CurrentSupplier = (Supplier)Session["Supplier"];
            ViewBag.CurrentSupplier = CurrentSupplier;
            int SupplierID = ViewBag.CurrentSupplier.SupplierID;
            Supplier Sup = SupplierLogic.SelectByPK(SupplierID);

            return View(Sup);
        }

		public ActionResult NewPO()
        {
            int SupplierID = ViewBag.CurrentSupplier.SupplierID;
            DataTable dt = SupplierLogic.SelectAllPOForSupp(SupplierID);
            return View(dt);
           
        }

		public ActionResult OngoingPO()
        {
            int SupplierID = ViewBag.CurrentSupplier.SupplierID;
            DataTable dt = SupplierLogic.SelectAllPOOnSupp(SupplierID);
            
            return View(dt);
           
        }

		public ActionResult RejectedPO()
        {

            int SupplierID = ViewBag.CurrentSupplier.SupplierID;
            DataTable dt = SupplierLogic.SelectAllPORejectbySupp(SupplierID);
            return View(dt);
        }

        public ActionResult DispatchedPO()
        {

            int SupplierID = ViewBag.CurrentSupplier.SupplierID;
            DataTable dt = SupplierLogic.SelectAllPODispatchedbySupp(SupplierID);
           
            return View(dt);
        }

		public ActionResult DeliveredPO()
        {

            int SupplierID = ViewBag.CurrentSupplier.SupplierID;
            DataTable dt = SupplierLogic.SelectAllPODelivered(SupplierID);
            return View(dt);
        }


//-----------------------------------------------------------------------------------------------------------------------
		public ActionResult PORejectBySupplier()
        {
            PurchaseOrder PO = PurchaseOrderLogic.SelectByPK(Convert.ToInt32(Request.Params["ID"]));
            PO.Status = "Rejected by Supplier";
            PO.InvoiceFile = "";
            PO.ExportClearanceFile = "";
            PO.ImportDutyChallanFile = "";
            PO.ImportDutyPaymentFile = "";
            PO.ImportClearanceFile = "";
            PurchaseOrderLogic.Update(PO);

            POActivity POA = new POActivity();
            POA.ActivityDate = DateTime.Now;
            POA.ActivityType = "Rejected by Supplier";
            POA.PurchaseOrderID = PO.PurchaseOrderID;
			POA.SupplierID = PO.SupplierID;
            POA.Remarks = Request.Params["Remarks"];
          

            POActivityLogic.Insert(POA);
            return RedirectToAction("RejectedPO");


        }

		public ActionResult ACCEPT()
        {
            PurchaseOrder PO = PurchaseOrderLogic.SelectByPK(Convert.ToInt32(Request.Params["ID"]));
            PO.Status = "Accepted By Supplier";
			PO.TentativeDate = Convert.ToDateTime(Request.Params["TentativeDate"]);
            PO.InvoiceFile = "";
            PO.ExportClearanceFile = "";
            PO.ImportDutyChallanFile = "";
            PO.ImportDutyPaymentFile = "";
            PO.ImportClearanceFile = "";
            PurchaseOrderLogic.Update(PO);

            POActivity POA = new POActivity();
            POA.ActivityDate = DateTime.Now;
            POA.ActivityType = "Accepted By Supplier";
            POA.PurchaseOrderID = PO.PurchaseOrderID;
			POA.SupplierID = PO.SupplierID;
			POA.Remarks = Request.Params["Remarks"];
			POActivityLogic.Insert(POA);

			int ID = Convert.ToInt32(PO.StaffID);
            DataTable dts = StaffLogic.SelectEmail(ID);

            MailMessage mailMessage = new MailMessage("query.imexport@gmail.com", dts.Rows[0]["Email"].ToString());
            mailMessage.Subject = "Purchase Order Accepted";
            mailMessage.Body = "Purchase Order is:" + PO.PurchaseOrderID + "\n \t" +
                               "Created on:" + PO.CreateDate+ "\n \t" +
                               "is accepted by" + "\n \t" + "Supplier ID is:" + PO.SupplierID;

            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);
            smtpClient.Credentials = new System.Net.NetworkCredential()
            {
                UserName = "query.imexport@gmail.com",
                Password = "nisarglovesemail"
            };
            smtpClient.EnableSsl = true;
            smtpClient.Send(mailMessage);


           
            return RedirectToAction("OngoingPO");


        }


        public ActionResult DISPATCHED()
        {
            PurchaseOrder PO = PurchaseOrderLogic.SelectByPK(Convert.ToInt32(Request.Params["ID"]));
            PO.Status = "Supplier Dispatched";
			if (Request.Files["InvoiceFile"].ContentLength > 0)
			{
				string filename = DateTime.Now.Ticks.ToString() + "_" + Request.Files["InvoiceFile"].FileName;
				string PhysicalFileName = Server.MapPath("~/AllFiles/InvoiceFile/" + filename);
				Request.Files["InvoiceFile"].SaveAs(PhysicalFileName);
				PO.InvoiceFile = filename;
			}
			PO.ExportClearanceFile = "";
            PO.ImportDutyChallanFile = "";
            PO.ImportDutyPaymentFile = "";
            PO.ImportClearanceFile = "";
			PO.DispatchdatebySupp = DateTime.Now;

            String PurchaseOrderID = Convert.ToInt32(Request.Params["ID"]).ToString();
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(PurchaseOrderID, QRCodeGenerator.ECCLevel.Q);
            QRCode qrCode = new QRCode(qrCodeData);
            Bitmap qrCodeImage = qrCode.GetGraphic(20);

            string filename1 = DateTime.Now.Ticks.ToString() + "QrCode.jpeg"  ;
            string PhysicalFileName1 = Server.MapPath("~/ProfilePhotos/QRCode/" + filename1);
            qrCodeImage.Save(PhysicalFileName1);

            PO.QRCode = filename1;

            PurchaseOrderLogic.Update(PO);

            POActivity POA = new POActivity();
            POA.ActivityDate = DateTime.Now;
            POA.ActivityType = "Supplier Dispatched";
            POA.PurchaseOrderID = PO.PurchaseOrderID;
			POA.SupplierID = PO.SupplierID;
			POA.Remarks = Request.Params["Remarks"];

            int ID = Convert.ToInt32(PO.StaffID);
            DataTable dts = StaffLogic.SelectEmail(ID);

            MailMessage mailMessage = new MailMessage("query.imexport@gmail.com", dts.Rows[0]["Email"].ToString());
            mailMessage.Subject = "Purchase Order Dispatched";
            mailMessage.Body = "Purchase Order is:" + PO.PurchaseOrderID + "\n \t" +
                               "Created on:" + PO.CreateDate + "\n \t" +
                               "is dispatched by" + PO.SupplierID;

            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);
            smtpClient.Credentials = new System.Net.NetworkCredential()
            {
                UserName = "query.imexport@gmail.com",
                Password = "nisarglovesemail"
            };
            smtpClient.EnableSsl = true;
            smtpClient.Send(mailMessage);


            POActivityLogic.Insert(POA);
            return RedirectToAction("PODetails", "Alluser", new { PurchaseOrderID = Request.Params["ID"], filename1= filename1 });


        }
    }
}