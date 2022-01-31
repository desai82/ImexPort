using IMEXPort.BusinessLogic;
using IMEXPort.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IMEXPort.Controllers
{
    public class FinanceController : Controller
    {
		Staff CURRENT_STAFF = new Staff();
		protected override void OnActionExecuting(ActionExecutingContext filterContext)
		{
			Staff CURRENT_STAFF = new Staff();
			if (Session["Staff"] == null)
			{
				filterContext.Result = RedirectToAction("Login", "Access");
			}
			CURRENT_STAFF = (Staff)Session["Staff"];
			ViewBag.CurrentStaff = CURRENT_STAFF;
			base.OnActionExecuting(filterContext);
		}

		// GET: Finance
		public ActionResult Home()
        {
            DataTable dt = StaffLogic.ImportDutyPendingHome();
            return View(dt);
        }
        public ActionResult ImportDutyPending()
        {
            int StaffID = ViewBag.CurrentStaff.StaffID;
            DataTable dt = StaffLogic.SelectAllPOOnIDPen(StaffID);
            return View(dt);

        }
        
        public ActionResult ImportDutyPaid()
        {
            int StaffID = ViewBag.CurrentStaff.StaffID;
            DataTable dt = StaffLogic.SelectAllPOOnIDPai(StaffID);
            return View(dt);
        }

        public ActionResult ImportCleared()
        {
            int StaffID = ViewBag.CurrentStaff.StaffID;
            DataTable dt = StaffLogic.SelectAllPOOnIClea(StaffID);
            return View(dt);
        }
    }
}