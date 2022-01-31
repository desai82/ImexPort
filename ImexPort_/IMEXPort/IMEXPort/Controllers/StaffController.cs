using IMEXPort.BusinessLogic;
using IMEXPort.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IMEXPort.Controllers
{
    public class StaffController : Controller
    {
        // GET: Staff
        public ActionResult Index()
        {
            return View();
        }

		public ActionResult MyProfile()
		{
			Staff CurrentStaff = new Staff();
			if (Session["Staff"] == null)
			{
			 return RedirectToAction("Login", "Access");
			}
			CurrentStaff = (Staff)Session["Staff"];
			ViewBag.CurrentStaff = CurrentStaff;
			int DepartmentID = ViewBag.CurrentStaff.DepartmentID;
			Department D = DepartmentLogic.SelectByPK(DepartmentID);
			
			return View(D);
		}
	}
}