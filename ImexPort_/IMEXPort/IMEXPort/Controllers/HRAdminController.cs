using IMEXPort.BusinessLogic;
using IMEXPort.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Mail;


namespace IMEXPort.Controllers
{
    public class HRAdminController : Controller
    {
		protected override void OnActionExecuting(ActionExecutingContext filterContext)
		{
			Staff CurrentStaff = new Staff();
			if (Session["Staff"] == null)
			{
				filterContext.Result = RedirectToAction("Login", "Access");
			}
			CurrentStaff = (Staff)Session["Staff"];
			ViewBag.CurrentStaff = CurrentStaff;
			base.OnActionExecuting(filterContext);
		}

		public ActionResult Home()
        {
            DataTable PO = PurchaseOrderLogic.SelectALL();
            ViewBag.PurchaseOrder = PO;
            return View();
        }

		public ActionResult DepartmentNew()
		{
			return View();
		}

		[HttpPost]
		public ActionResult DepartmentNewSubmit()
		{
			Department D = new Department();
			D.DeptName = Request.Params["DeptName"];


			DepartmentLogic.Insert(D);

			return RedirectToAction("DepartmentList");
		}

		public ActionResult DepartmentList()
		{
			DataTable dt = DepartmentLogic.SelectALL();
			return View(dt);
		}

		public ActionResult DepartmentEdit()
		{
			 
			Department D = DepartmentLogic.SelectByPK(Convert.ToInt32(Request.Params["DepartmentID"]));

			return View(D);
		}

		[HttpPost]
		public ActionResult DepartmentEditSubmit()
		{
			Department D = DepartmentLogic.SelectByPK(Convert.ToInt32(Request.Params["DepartmentID"]));

			D.DeptName = Request.Params["DeptName"];
			D.DepartmentID = Convert.ToInt32(Request.Params["DepartmentID"]);
			DepartmentLogic.Update(D);

			return RedirectToAction("DepartmentList");
		}

//----------------------------------------------------------------------------------------------------------------------------------------------

		public ActionResult StaffRegistration()
		{

			DataTable DeptID = DepartmentLogic.SelectALL();
			ViewBag.DeptID = DeptID;

			DataTable StaffID = StaffLogic.SelectALL();
			ViewBag.StaffID = StaffID;

			return View();
		}

		[HttpPost]
		public ActionResult StaffNewSubmit()
		{
			

			Staff S = new Staff();
			S.Name = Request.Params["Name"];
			S.Email = Request.Params["Email"];
			S.Mobile = Request.Params["Mobile"];
			if (Request.Files["Photo"].ContentLength > 0)
			{
				string filename = DateTime.Now.Ticks.ToString() + "_" + Request.Files["Photo"].FileName;
				string PhysicalFileName = Server.MapPath("~/ProfilePhotos/StaffPics/" + filename);
				Request.Files["Photo"].SaveAs(PhysicalFileName);
				S.Photo = filename;
			}
			

			S.Username = Request.Params["Username"];
			S.Password = Request.Params["Password"];
			S.IsActive = (Request.Params["IsActive"] == "1");
			S.DepartmentID = Convert.ToInt32(Request.Params["DepartmentID"]);
			S.SupervisorID = Convert.ToInt32(Request.Params["SupervisorID"]);
			S.Designation = Request.Params["Designation"];

			MailMessage mailMessage = new MailMessage("query.imexport@gmail.com", S.Email);
			mailMessage.Subject = "Test Email";
			mailMessage.Body = "Welcome" + S.Name +"\t"+ "to our first mail";

			SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);
			smtpClient.Credentials = new System.Net.NetworkCredential()
			{
				UserName = "query.imexport@gmail.com",
				Password = "nisarglovesemail"
			};
			smtpClient.EnableSsl = true;
			smtpClient.Send(mailMessage);

			StaffLogic.Insert(S);

			
			return RedirectToAction("StaffList");
		}

		public ActionResult StaffList()
		{
			
			DataTable dt = StaffLogic.SelectALL();
			return View(dt);
		}

		public ActionResult StaffEdit()
		{
			DataTable DeptID = DepartmentLogic.SelectALL();
			ViewBag.DeptID = DeptID;

			DataTable StaffID = StaffLogic.SelectALL();
			ViewBag.StaffID = StaffID;

			Staff s = StaffLogic.SelectByPK(Convert.ToInt32(Request.Params["StaffID"]));

			return View(s);
		}

		[HttpPost]
		public ActionResult StaffEditSubmit()
		{
			Staff S = StaffLogic.SelectByPK(Convert.ToInt32(Request.Params["StaffID"]));
			S.Name = Request.Params["Name"];
			S.Email = Request.Params["Email"];
			S.Mobile = Request.Params["Mobile"];

			if (Request.Files["Photo"].ContentLength > 0)
			{
				string filename = DateTime.Now.Ticks.ToString() + "_" + Request.Files["Photo"].FileName;
				string PhysicalFileName = Server.MapPath("~/ProfilePhotos/StaffPics/" + filename);
				Request.Files["Photo"].SaveAs(PhysicalFileName);
				S.Photo = filename;
			}

			S.Username = Request.Params["Username"];
			S.Password = Request.Params["Password"];
			S.IsActive = (Request.Params["IsActive"] == "1");
			S.DepartmentID = Convert.ToInt32(Request.Params["DepartmentID"]);
			S.SupervisorID = Convert.ToInt32(Request.Params["SupervisorID"]);
			S.Designation = Request.Params["Designation"];
			S.StaffID = Convert.ToInt32(Request.Params["StaffID"]);

			StaffLogic.Update(S);

			return RedirectToAction("StaffList");
		}

		public ActionResult StaffSearchSubmit()
		{
			DataTable dt = StaffLogic.SelectBySearch(Request.Params["Name"],Request.Params["Designation"]);

			return View("StaffList",dt);
		}

		public ActionResult TrackPO()
		{
			DataTable PO = POActivityLogic.SelectALL();

			DataTable Staff = StaffLogic.SelectALL();
			ViewBag.Staff = Staff;
			DataTable Supplier = SupplierLogic.SelectALL();
			ViewBag.Supplier = Supplier;
			return View(PO);
		}

		public ActionResult WorkHistory()
		{
			int StaffID = Convert.ToInt32(Request.Params[ "StaffID"]);
			
			DataTable StaffWork = PurchaseOrderLogic.SelectAllPOByMe(StaffID);
			ViewBag.StaffWork = StaffWork;

			
			return View();
		}
        //----------------------------------------------------------------------------------------------------------------------------------------------
        
        public ActionResult OngoingPO()
        {
            DataTable dt = StaffLogic.SelectAllPO();
            return View(dt);
           
        }
        public ActionResult DeliveredPO()
        {
            DataTable dt = StaffLogic.SelectAllPODelivered();
            return View(dt);
        }
        public ActionResult CancelledPO()
        {
            DataTable dt = StaffLogic.SelectAllPOCAncelled();
            return View(dt);
        }



    }
}