using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IMEXPort.BusinessLogic;
using IMEXPort.Models;
using System.Data;


namespace IMEXPort.Controllers
{
    public class AlluserController : Controller
    {
		// GET: Alluser
		Staff CURRENT_STAFF = new Staff();
        Supplier CURRENT_SUPPLIER = new Supplier();

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
		{
			Supplier CURRENT_SUPPLIER = new Supplier();
			Staff CURRENT_STAFF = new Staff();
			if (Session["Staff"] == null && Session["Supplier"] == null)
			{
				filterContext.Result = RedirectToAction("Login", "Access");
			}
			CURRENT_STAFF = (Staff)Session["Staff"];
			CURRENT_SUPPLIER = (Supplier)Session["Supplier"];
			ViewBag.CurrentSupplier = CURRENT_SUPPLIER;
			ViewBag.CurrentStaff = CURRENT_STAFF;
			base.OnActionExecuting(filterContext);
		}

		public ActionResult Index()
        {
            return View();
        }

		public ActionResult PODetails()
        {
            int PurchaseOrderID = Convert.ToInt32(Request.Params["PurchaseOrderID"]);
            PurchaseOrder PO = PurchaseOrderLogic.SelectByPK(PurchaseOrderID);
			ViewBag.filename1 = Request.Params["filename1"];
            Supplier Sup = SupplierLogic.SelectByPK(PO.SupplierID);
            ViewBag.SName = Sup.Name;
            DataTable ApproverID = StaffLogic.SelectALL();
            ViewBag.ApproverID = ApproverID;

            DataTable POItem = POItemLogic.SelectByPOrderID(PurchaseOrderID);
            ViewBag.POItem = POItem;
            DataTable MatID = MaterialLogic.SelectALL();
            ViewBag.MatID = MatID;

            DataTable FCAID = CustomsAgentLogic.SelectByCountry(Sup.Country);
            ViewBag.FCAID = FCAID;
            DataTable ICAID = CustomsAgentLogic.SelectByICACountry();
            ViewBag.ICAID = ICAID;

            return View(PO);
        }

		public ActionResult GetNumber()
		{
            int Count=0;
            if (Session["Staff"] != null)
            {
                Staff CurrentStaff = (Staff)Session["Staff"];

                if (CurrentStaff.Designation == "Purchase Manager" || CurrentStaff.Designation == "Purchase Staff")
                {
                    DataTable dtpurchase = PurchaseOrderLogic.SelectForPurchaseNoti(CurrentStaff.StaffID);
                     Count = dtpurchase.Rows.Count;
                    
                }
                else if(CurrentStaff.Designation == "Finance Manager" || CurrentStaff.Designation == "Finance Staff")
                {
                    DataTable dtpurchase = PurchaseOrderLogic.SelectForPurchaseNotiFina(CurrentStaff.StaffID);
                    Count = dtpurchase.Rows.Count;

                }
                //DataTable dtstaff 

            }
            else if(Session["Supplier"] != null)
            {
                Supplier CurrentSupplier = (Supplier)Session["Supplier"];

                DataTable dtpurchase = SupplierLogic.SelectForPurchaseNotiSupp(CurrentSupplier.SupplierID);
                Count = dtpurchase.Rows.Count;
            }

            return Content(Count+"");

        }
	}
}