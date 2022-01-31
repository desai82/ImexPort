using IMEXPort.BusinessLogic;
using IMEXPort.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Mail;
using System.Data;

namespace IMEXPort.Controllers
{
    public class AccessController : Controller
    {
       
		
		// GET: Access
        public ActionResult Index()
        {
            return View();
        }

		public ActionResult Login()
		{
            int flag = 0;
			if(Session["Staff"] != null)
			{
				Staff S = (Staff)Session["Staff"];
				if (S.StaffID > 0)
				{
					if ((S.Designation == "Purchase Manager" || S.Designation == "Purchase Staff") && Convert.ToBoolean(S.IsActive))
					{
						return RedirectToAction("Home", "PurchaseManager");
					}
					else if (S.Designation == "Admin")
					{
						return RedirectToAction("Home", "HRAdmin");
					}
					else if((S.Designation == "Finance Manager" || S.Designation == "Finance Staff") && Convert.ToBoolean(S.IsActive))
					{
						return RedirectToAction("Home", "Finance");
					}
					else
					{
						return View();
					}
				}
			}
			else if (Session["Supplier"] != null)
			{
				return RedirectToAction("Home", "Supplier");

			}
            flag = Convert.ToInt32(Request.Params["flag"]);
           

			return View(flag);
			
		}

		public ActionResult Logout()
		{


			Session["Staff"] = null;
			Session["Supplier"] = null;
			return RedirectToAction("Login");

		}

		public ActionResult LoginSubmit()
		{

			string Username = Request.Params["Username"];
			string Password = Request.Params["Password"];
			Staff S = StaffLogic.SelectByUnPw(Username, Password);
            Supplier Sup = SupplierLogic.SelectByUnPw(Username, Password);
			if(S.StaffID>0)
			{
				Session["Staff"] = S;
				if((S.Designation=="Purchase Manager" || S.Designation == "Purchase Staff") && Convert.ToBoolean(S.IsActive))
				{
					return RedirectToAction("Home", "PurchaseManager");
				}
				else if(S.Designation=="Admin")
				{
					return RedirectToAction("Home", "HRAdmin");
				}
				else
				{
					return RedirectToAction("Login");
				}
			}
            else if(Sup.SupplierID>0)
            {
                Session["Supplier"] = Sup;
                return RedirectToAction("Home","Supplier");
            }
			else
			{
				return RedirectToAction("Login");
			}

            }
        public ActionResult ForgetPassword()
        {

            return View();
        }
        public ActionResult ForgetPasswordSumbit()
        {
            string Email = Request.Params["email"];
            DataTable S = StaffLogic.ForgetPasswordSta(Email);
            DataTable Sup = SupplierLogic.ForgetPasswordSupp(Email);
            if(S.Rows.Count>0)
            {
                MailMessage mailMessage = new MailMessage("query.imexport@gmail.com", S.Rows[0]["Email"].ToString());
                mailMessage.Subject = "Forgot password";
                mailMessage.Body = "Your username is:" + S.Rows[0]["Username"] +"\n"+ 
                                   "Your password is:"+ S.Rows[0]["Password"];

                SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);
                smtpClient.Credentials = new System.Net.NetworkCredential()
                {
                    UserName = "query.imexport@gmail.com",
                    Password = "nisarglovesemail"
                };
                smtpClient.EnableSsl = true;
                smtpClient.Send(mailMessage);

            }
            else if(Sup.Rows.Count > 0 )
            {
                MailMessage mailMessage = new MailMessage("query.imexport@gmail.com", Sup.Rows[0]["Email"].ToString());
                mailMessage.Subject = "Forgot password";
                mailMessage.Body = "Your username is:" + Sup.Rows[0]["Username"] +"\n"+
                                   "Your password is:" + Sup.Rows[0]["Password"];

                SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);
                smtpClient.Credentials = new System.Net.NetworkCredential()
                {
                    UserName = "query.imexport@gmail.com",
                    Password = "nisarglovesemail"
                };
                smtpClient.EnableSsl = true;
                smtpClient.Send(mailMessage);
            }
            return RedirectToAction("Login", new { flag=1});
        }

		


	}
}