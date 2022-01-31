using IMEXPort.BusinessLogic;
using IMEXPort.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Mail;
using QRCoder;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;


namespace IMEXPort.Controllers
{
	public class PurchaseManagerController : Controller
	{
		Staff CURRENT_STAFF = new Staff();
		protected override void OnActionExecuting(ActionExecutingContext filterContext)
		{
			
			if (Session["Staff"] == null)
			{
				filterContext.Result = RedirectToAction("Login", "Access");
			}
			CURRENT_STAFF = (Staff)Session["Staff"];
			ViewBag.CurrentStaff = CURRENT_STAFF;
			base.OnActionExecuting(filterContext);
		}

		public ActionResult Home()
		{
			int StaffID = ViewBag.CurrentStaff.StaffID;
			DataTable dt = PurchaseOrderLogic.SelectAllPOByMe(StaffID);
			DataTable dt1 = PurchaseOrderLogic.SelectAllPOForMe(StaffID);
			ViewBag.AllPOByMe = dt;
			ViewBag.AllPOForMe = dt1;

			return View();
		}

//----------------------------------------------------------------------------------------------------------------------------------------------

		public ActionResult SupplierRegistration()
		{
			return View();
		}

		[HttpPost]
		public ActionResult SupplierNewSubmit()
		{
			Supplier S = new Supplier();
			S.Name = Request.Params["Name"];
			S.Email = Request.Params["Email"];
			S.Mobile = Request.Params["Mobile"];

			if (Request.Files["Photo"].ContentLength > 0)
			{
				string filename = DateTime.Now.Ticks.ToString() + "_" + Request.Files["Photo"].FileName;
				string PhysicalFileName = Server.MapPath("~/ProfilePhotos/SupplierPics/" + filename);
				Request.Files["Photo"].SaveAs(PhysicalFileName);
				S.Photo = filename;
			}
			
			S.Username = Request.Params["Username"];
			S.Password = Request.Params["Password"];
			S.IsActive = (Request.Params["IsActive"] == "1");

			S.Country = Request.Params["Country"];

            MailMessage mailMessage = new MailMessage("query.imexport@gmail.com", S.Email);
            mailMessage.Subject = "Test Email";
            mailMessage.Body = "Welcome" + S.Name + "to our first mail";

            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);
            smtpClient.Credentials = new System.Net.NetworkCredential()
            {
                UserName = "query.imexport@gmail.com",
                Password = "nisarglovesemail"
            };
            smtpClient.EnableSsl = true;
            smtpClient.Send(mailMessage);
            SupplierLogic.Insert(S);

			return RedirectToAction("SupplierList");
		}

		public ActionResult SupplierList()
		{
			DataTable dt = SupplierLogic.SelectALL();
			return View(dt);
		}

		public ActionResult SupplierEdit()
		{

			Supplier s = SupplierLogic.SelectByPK(Convert.ToInt32(Request.Params["SupplierID"]));

			return View(s);
		}

		[HttpPost]
		public ActionResult SupplierEditSubmit()
		{
			Supplier S = SupplierLogic.SelectByPK(Convert.ToInt32(Request.Params["SupplierID"]));
			S.Name = Request.Params["Name"];
			S.Email = Request.Params["Email"];
			S.Mobile = Request.Params["Mobile"];

			if (Request.Files["Photo"].ContentLength > 0)
			{
				string filename = DateTime.Now.Ticks.ToString() + "_" + Request.Files["Photo"].FileName;
				string PhysicalFileName = Server.MapPath("~/ProfilePhotos/SupplierPics/" + filename);
				Request.Files["Photo"].SaveAs(PhysicalFileName);
				S.Photo = filename;
			}

			S.Username = Request.Params["Username"];
			S.Password = Request.Params["Password"];
			S.IsActive = (Request.Params["IsActive"] == "1");
			S.Country = Request.Params["Country"];
			S.SupplierID = Convert.ToInt32(Request.Params["SupplierID"]);

			SupplierLogic.Update(S);

			return RedirectToAction("SupplierList");
		}

		public ActionResult SupplierSearchSubmit()
		{
			DataTable dt = SupplierLogic.SelectBySearch(Request.Params["Name"], Request.Params["Country"]);

			return View("SupplierList", dt);
		}
	
//----------------------------------------------------------------------------------------------------------------------------------------------

		public ActionResult ICARegistration()
		{
			return View();
		}

		[HttpPost]
		public ActionResult ICANewSubmit()
		{
			CustomsAgent C = new CustomsAgent();
			C.Name = Request.Params["Name"];
			C.Email = Request.Params["Email"];
			C.Mobile = Request.Params["Mobile"];
			if (Request.Files["Photo"].ContentLength > 0)
			{
				string filename = DateTime.Now.Ticks.ToString() + "_" + Request.Files["Photo"].FileName;
				string PhysicalFileName = Server.MapPath("~/ProfilePhotos/CustomsAgentPics/" + filename);
				Request.Files["Photo"].SaveAs(PhysicalFileName);
				C.Photo = filename;
			}
			
			C.Username = Request.Params["Username"];
			C.Password = Request.Params["Password"];
			C.IsActive = (Request.Params["IsActive"] == "1");

			C.Country = Request.Params["Country"];
            C.Port = Request.Params["Port"];
            MailMessage mailMessage = new MailMessage("query.imexport@gmail.com", C.Email);
            mailMessage.Subject = "Test Email";
            mailMessage.Body = "Welcome" + C.Name + "to our first mail";

            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);
            smtpClient.Credentials = new System.Net.NetworkCredential()
            {
                UserName = "query.imexport@gmail.com",
                Password = "nisarglovesemail"
            };
            smtpClient.EnableSsl = true;
            smtpClient.Send(mailMessage);
            CustomsAgentLogic.Insert(C);

			return RedirectToAction("CustomsAgentList");
		}

		public ActionResult ICAList()
		{
			DataTable dt = CustomsAgentLogic.SelectByICACountry();
			return View(dt);
		}

		public ActionResult ICAEdit()
		{

			CustomsAgent C = CustomsAgentLogic.SelectByPK(Convert.ToInt32(Request.Params["CustomsAgentID"]));

			return View(C);
		}

		[HttpPost]
		public ActionResult ICAEditSubmit()
		{
			CustomsAgent C = CustomsAgentLogic.SelectByPK(Convert.ToInt32(Request.Params["CustomsAgentID"]));
			C.Name = Request.Params["Name"];
			C.Email = Request.Params["Email"];
			C.Mobile = Request.Params["Mobile"];

			if (Request.Files["Photo"].ContentLength > 0)
			{
				string filename = DateTime.Now.Ticks.ToString() + "_" + Request.Files["Photo"].FileName;
				string PhysicalFileName = Server.MapPath("~/ProfilePhotos/CustomsAgentPics/" + filename);
				Request.Files["Photo"].SaveAs(PhysicalFileName);
				C.Photo = filename;
			}
			C.Username = Request.Params["Username"];
			C.Password = Request.Params["Password"];
			C.IsActive = (Request.Params["IsActive"] == "1");
			C.Country = Request.Params["Country"];
            C.Port = Request.Params["Port"];
            C.CustomsAgentID = Convert.ToInt32(Request.Params["CustomsAgentID"]);

			CustomsAgentLogic.Update(C);

			return RedirectToAction("ICAList");
		}

//----------------------------------------------------------------------------------------------------------------------------------------------

		public ActionResult CustomsAgentList()
		{
			DataTable dt = CustomsAgentLogic.SelectALL();
			return View(dt);
		}

		public ActionResult CustomsAgentSearchSubmit()
		{
			DataTable dt = CustomsAgentLogic.SelectBySearch(Request.Params["Name"], Request.Params["Country"]);

			return View("CustomsAgentList", dt);
		}

//----------------------------------------------------------------------------------------------------------------------------------------------


		public ActionResult FCARegistration()
		{
			return View();
		}

		[HttpPost]
		public ActionResult FCANewSubmit()
		{
			CustomsAgent C = new CustomsAgent();
			C.Name = Request.Params["Name"];
			C.Email = Request.Params["Email"];
			C.Mobile = Request.Params["Mobile"];

			if (Request.Files["Photo"].ContentLength > 0)
			{
				string filename = DateTime.Now.Ticks.ToString() + "_" + Request.Files["Photo"].FileName;
				string PhysicalFileName = Server.MapPath("~/ProfilePhotos/CustomsAgentPics/" + filename);
				Request.Files["Photo"].SaveAs(PhysicalFileName);
				C.Photo = filename;
			}
		

			C.Username = Request.Params["Username"];
			C.Password = Request.Params["Password"];
			C.IsActive = (Request.Params["IsActive"] == "1");

			C.Country = Request.Params["Country"];
            C.Port = Request.Params["Port"];

            MailMessage mailMessage = new MailMessage("query.imexport@gmail.com", C.Email);
            mailMessage.Subject = "Test Email";
            mailMessage.Body = "Welcome" + C.Name + "to our first mail";

            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);
            smtpClient.Credentials = new System.Net.NetworkCredential()
            {
                UserName = "query.imexport@gmail.com",
                Password = "nisarglovesemail"
            };
            smtpClient.EnableSsl = true;
            smtpClient.Send(mailMessage);
            CustomsAgentLogic.Insert(C);

			return RedirectToAction("FCAList");
		}

		public ActionResult FCAList()
		{
			DataTable dt = CustomsAgentLogic.SelectByFCACountry();
			return View(dt);
		}

		public ActionResult FCAEdit()
		{

			CustomsAgent C = CustomsAgentLogic.SelectByPK(Convert.ToInt32(Request.Params["CustomsAgentID"]));

			return View(C);
		}

		[HttpPost]
		public ActionResult FCAEditSubmit()
		{
			CustomsAgent C = CustomsAgentLogic.SelectByPK(Convert.ToInt32(Request.Params["CustomsAgentID"]));
			C.Name = Request.Params["Name"];
			C.Email = Request.Params["Email"];
			C.Mobile = Request.Params["Mobile"];

			if (Request.Files["Photo"].ContentLength > 0)
			{
				string filename = DateTime.Now.Ticks.ToString() + "_" + Request.Files["Photo"].FileName;
				string PhysicalFileName = Server.MapPath("~/ProfilePhotos/CustomsAgentPics/" + filename);
				Request.Files["Photo"].SaveAs(PhysicalFileName);
				C.Photo = filename;
			}

			C.Username = Request.Params["Username"];
			C.Password = Request.Params["Password"];
			C.IsActive = (Request.Params["IsActive"] == "1");
			C.Country = Request.Params["Country"];
            C.Port = Request.Params["Port"];
            C.CustomsAgentID = Convert.ToInt32(Request.Params["CustomsAgentID"]);

			CustomsAgentLogic.Update(C);

			return RedirectToAction("FCAList");
		}


//-----------------------------------------------------------------------------------------------------------------------------------------------

		public ActionResult RawMaterialNew()
		{
			DataTable SID = StaffLogic.SelectByPsPm();
			ViewBag.SID = SID;
		
			return View();
		}

		[HttpPost]
		public ActionResult RawMaterialNewSubmit()
		{
			Material M = new Material();
			M.MaterialName = Request.Params["MaterialName"];
			M.MaterialCode = Request.Params["MaterialCode"];
			M.StaffID = Convert.ToInt32(Request.Params["StaffID"]);

			MaterialLogic.Insert(M);

			return RedirectToAction("RawMaterialList");

		}

		public ActionResult RawMaterialList()
		{
			DataTable dt = MaterialLogic.SelectALL();
			return View(dt);
		}

		public ActionResult RawMaterialEdit()
		{
			DataTable SID = StaffLogic.SelectByPsPm();
			ViewBag.SID = SID;

			Material M = MaterialLogic.SelectByPK(Convert.ToInt32(Request.Params["MaterialID"]));

			return View(M);
		}

		[HttpPost]
		public ActionResult RawMaterialEditSubmit()
		{
			Material M = MaterialLogic.SelectByPK(Convert.ToInt32(Request.Params["MaterialID"]));

			M.MaterialName = Request.Params["MaterialName"];
			M.MaterialCode = Request.Params["MaterialCode"];
			M.StaffID = Convert.ToInt32(Request.Params["StaffID"]);
			M.MaterialID = Convert.ToInt32(Request.Params["MaterialID"]);


			MaterialLogic.Update(M);

			return RedirectToAction("RawMaterialList");
		}

		public ActionResult MaterialSearchSubmit()
		{
			DataTable dt = MaterialLogic.SelectBySearch(Request.Params["Name"]);

			return View("RawMaterialList", dt);
		}

//-------------------------------------------------------------------------------------------------------------------------------------------

		public ActionResult SupplierOfMaterial()
		{
			ViewBag.MaterialID = Convert.ToInt32(Request.Params["MaterialID"]);
			DataTable SupID = SupplierLogic.SelectALL();
			ViewBag.SupID = SupID;
			DataTable dt = MaterialLogic.SelectSOM(Convert.ToInt32(Request.Params["MaterialID"]));
			return View(dt);
		}

		[HttpPost]
		public ActionResult SupplierOfMaterialAdd()
		{
			int MaterialID = Convert.ToInt32(Request.Params["MaterialID"]);
			MaterialSupplier MS = new MaterialSupplier();
			MS.SupplierID = Convert.ToInt32(Request.Params["SupplierID"]);
			MS.MaterialID = MaterialID;

			MaterialSupplierLogic.Insert(MS);
			return RedirectToAction("SupplierOfMaterial", new { MaterialID = MaterialID });
		}

		public ActionResult SupplierOfMaterialDelete()
		{

			int MaterialID = Convert.ToInt32(Request.Params["MaterialID"]);
			string SupplierName = Request.Params["SupplierName"];
			int SupplierID = SupplierLogic.SelectBySupplierName(SupplierName);

			MaterialSupplierLogic.Delete(MaterialID, SupplierID);
			return RedirectToAction("SupplierOfMaterial", new { MaterialID = MaterialID });
		}

		public ActionResult MaterialOfSupplier()
		{
			ViewBag.SupplierID = Convert.ToInt32(Request.Params["SupplierID"]);
			DataTable MatID = MaterialLogic.SelectALL();
			ViewBag.MatID = MatID;
			DataTable dt = SupplierLogic.SelectMOS(Convert.ToInt32(Request.Params["SupplierID"]));
			return View(dt);
		}

		[HttpPost]
		public ActionResult MaterialOfSupplierAdd()
		{
			int SupplierID = Convert.ToInt32(Request.Params["SupplierID"]);
			MaterialSupplier MS = new MaterialSupplier();
			MS.SupplierID = SupplierID;
			MS.MaterialID = Convert.ToInt32(Request.Params["MaterialID"]);

			MaterialSupplierLogic.Insert(MS);
			return RedirectToAction("MaterialOfSupplier", new { SupplierID = SupplierID });
		}

		public ActionResult MaterialOfSupplierDelete()
		{

			int SupplierID = Convert.ToInt32(Request.Params["SupplierID"]);
			string MaterialName = Request.Params["MaterialName"];
			int MaterialID = MaterialLogic.SelectByMaterialName(MaterialName);

			MaterialSupplierLogic.Delete(MaterialID, SupplierID);
			return RedirectToAction("MaterialOfSupplier", new { SupplierID = SupplierID });
		}

//-------------------------------------------------------------------------------------------------------------------------------------------

		public ActionResult PONew()
		{
			DataTable SupID = SupplierLogic.SelectALL();
			ViewBag.SupID = SupID;
			DataTable ApproverID = StaffLogic.SelectALL();
			ViewBag.ApproverID = ApproverID;
           
            return View();
		}

		[HttpPost]
		public ActionResult PONewSubmit()
		{
			PurchaseOrder PO = new PurchaseOrder();
			PO.StaffID = ViewBag.CurrentStaff.StaffID;
			PO.CreateDate = DateTime.Now;
			PO.SupplierID = Convert.ToInt32(Request.Params["SupplierID"]);
			PO.Status = "Draft";
			PO.ApproverID = Convert.ToInt32(Request.Params["ApproverID"]);
            PO.FinanceID= Convert.ToInt32(Request.Params["FinanceID"]);
            PO.RequestedDate = Convert.ToDateTime(Request.Params["RequestedDate"]);
			PO.TentativeDate = DateTime.Today;
			PO.InvoiceFile = "";
			PO.ExportClearanceFile = "";
			PO.ImportDutyChallanFile = "";
			PO.ImportDutyPaymentFile = "";
			PO.ImportClearanceFile = "";
			PO.DispatchdatebyFCA = DateTime.Today;
			PO.DispatchdatebySupp = DateTime.Today;
			PO.QRCode = "";

			int PurchaseOrderID = PurchaseOrderLogic.Insert(PO);
			
			
			return RedirectToAction("PODetails", "Alluser" , new { PurchaseOrderID = PurchaseOrderID });

		}

		[HttpPost]
		public ActionResult PODetailsSave()
		{
			PurchaseOrder PO = PurchaseOrderLogic.SelectByPK(Convert.ToInt32(Request.Params["PurchaseOrderID"]));
			PO.ApproverID = Convert.ToInt32(Request.Params["ApproverID"]);
            PO.FinanceID = Convert.ToInt32(Request.Params["FinanceID"]);
            PO.RequestedDate = Convert.ToDateTime(Request.Params["RequestedDate"]);
			PO.FCAID = Convert.ToInt32(Request.Params["FCAID"]);
			PO.ICAID = Convert.ToInt32(Request.Params["ICAID"]);
			PO.ExportClearanceFile = "";
			PO.ImportClearanceFile = "";
			PO.ImportDutyChallanFile = "";
			PO.InvoiceFile = "";
			PO.ImportDutyPaymentFile = "";

            PurchaseOrderLogic.Update(PO);
			return RedirectToAction("PODetails", "Alluser", new { PurchaseOrderID = Request.Params["PurchaseOrderID"] });

		}

		[HttpPost]
		public ActionResult POSubmit()
		{
			PurchaseOrder PO = PurchaseOrderLogic.SelectByPK(Convert.ToInt32(Request.Params["ID"]));
			PO.Status = "Submitted For Approval";
            PO.InvoiceFile = "";
            PO.ExportClearanceFile = "";
            PO.ImportDutyChallanFile = "";
            PO.ImportDutyPaymentFile = "";
            PO.ImportClearanceFile = "";
            PurchaseOrderLogic.Update(PO);

			POActivity POA = new POActivity();
			POA.ActivityDate = DateTime.Now;
			POA.ActivityType = "Submitted For Approval";
			POA.PurchaseOrderID = PO.PurchaseOrderID;
			POA.Remarks = Request.Params["Remarks"];
			POA.StaffID = CURRENT_STAFF.StaffID;

            POActivityLogic.Insert(POA);
			return RedirectToAction("PODetails", "Alluser", new { PurchaseOrderID = Request.Params["ID"] });
		}

		[HttpPost]
		public ActionResult PODelete()
		{
			PurchaseOrder PO = PurchaseOrderLogic.SelectByPK(Convert.ToInt32(Request.Params["ID"]));
			PO.Status = "Cancelled";
			PO.InvoiceFile = "";
			PO.ExportClearanceFile = "";
			PO.ImportDutyChallanFile = "";
			PO.ImportDutyPaymentFile = "";
			PO.ImportClearanceFile = "";

            PurchaseOrderLogic.Update(PO);
			return RedirectToAction("PONew");
		}

		[HttpPost]
		public ActionResult PORelease()
        {
            
            PurchaseOrder PO = PurchaseOrderLogic.SelectByPK(Convert.ToInt32(Request.Params["ID"]));
            PO.Status = "Released By Supervisor";
            PO.InvoiceFile = "";
            PO.ExportClearanceFile = "";
            PO.ImportDutyChallanFile = "";
            PO.ImportDutyPaymentFile = "";
            PO.ImportClearanceFile = "";
            PurchaseOrderLogic.Update(PO);
            
            POActivity POA = new POActivity();
            POA.ActivityDate = DateTime.Now;
            POA.ActivityType = "Released by Supervisor";
            POA.PurchaseOrderID = PO.PurchaseOrderID;
            POA.Remarks = Request.Params["Remarks"];
            POA.StaffID = CURRENT_STAFF.StaffID;

			POActivityLogic.Insert(POA);
			DataTable s = StaffLogic.Hradminemail();

            MailMessage mailMessage = new MailMessage("query.imexport@gmail.com", s.Rows[0]["Email"].ToString());
            mailMessage.Subject = "Purchase Order Released";
            mailMessage.Body = "Purchase Order is:" + PO.PurchaseOrderID + "\n \t" +
                               "Creater ID is:" + PO.StaffID + "\n \t" + 
                               "Approver ID is:"+PO.ApproverID+ "\n \t" + "Supplier ID is:"+PO.SupplierID;

            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);
            smtpClient.Credentials = new System.Net.NetworkCredential()
            {
                UserName = "query.imexport@gmail.com",
                Password = "nisarglovesemail"
            };
            smtpClient.EnableSsl = true;
            smtpClient.Send(mailMessage);

          
            return RedirectToAction("PODetails", "Alluser", new { PurchaseOrderID = Request.Params["ID"] });
           
        }

		[HttpPost]
		public ActionResult PORejectBySupervisor()
        {
            PurchaseOrder PO = PurchaseOrderLogic.SelectByPK(Convert.ToInt32(Request.Params["ID"]));
            PO.Status = "Rejected by Supervisor";
            PO.InvoiceFile = "";
            PO.ExportClearanceFile = "";
            PO.ImportDutyChallanFile = "";
            PO.ImportDutyPaymentFile = "";
            PO.ImportClearanceFile = "";
            PurchaseOrderLogic.Update(PO);

            POActivity POA = new POActivity();
            POA.ActivityDate = DateTime.Now;
            POA.ActivityType = "Rejected by Supervisor";
            POA.PurchaseOrderID = PO.PurchaseOrderID;
            POA.Remarks = Request.Params["Remarks"];
            POA.StaffID = CURRENT_STAFF.StaffID;

            POActivityLogic.Insert(POA);
            return RedirectToAction("POPendingApproval");

            
        }

		public ActionResult POItemAdd()
		{
			int PurchaseOrderID = Convert.ToInt32(Request.Params["PurchaseOrderID"]);
			POItem PO = new POItem();
			PO.PurchaseOrderID = Convert.ToInt32(Request.Params["PurchaseOrderID"]);
			PO.MaterialID = Convert.ToInt32(Request.Params["MaterialID"]);
			PO.Quantity = Convert.ToInt32(Request.Params["Quantity"]);
			PO.Rate = Convert.ToInt32(Request.Params["Rate"]);
			PO.Remarks = Request.Params["Remarks"];

			POItemLogic.Insert(PO);
			return RedirectToAction("PODetails", "Alluser", new { PurchaseOrderID = PurchaseOrderID});
		}

		public ActionResult POGeneratedByMe()
		{
			int StaffID = ViewBag.CurrentStaff.StaffID;
			DataTable dt = PurchaseOrderLogic.SelectAllPOByMe(StaffID);
			return View(dt);
		}

		public ActionResult POPendingApproval()
		{
			int StaffID = ViewBag.CurrentStaff.StaffID;
			DataTable dt = PurchaseOrderLogic.SelectAllPOForMe(StaffID);
			return View(dt);

		}

		public ActionResult POItemDelete()
		{
			POItemLogic.Delete(Convert.ToInt32(Request.Params["POItemID"]));
			return RedirectToAction("PODetails", "Alluser", new { PurchaseOrderID = Request.Params["PurchaseOrderID"] });
		}

		public ActionResult test1()
		{
			return View();
		}
	}
}
