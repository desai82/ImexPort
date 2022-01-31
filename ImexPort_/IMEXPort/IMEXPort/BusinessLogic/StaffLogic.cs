using IMEXPort.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace IMEXPort.BusinessLogic
{
	public class StaffLogic
	{
		public static int Insert(Staff S)
		{
			string query = "INSERT INTO Staff VALUES(@Name,@Email,@Mobile,@Photo,@Username,@Password,@IsActive,@DepartmentID,@SupervisorID,@Designation)";
			List<SqlParameter> parameters = new List<SqlParameter>();
			parameters.Add(new SqlParameter("@Name", S.Name));
			parameters.Add(new SqlParameter("@Email", S.Email));
			parameters.Add(new SqlParameter("@Mobile", S.Mobile));
			parameters.Add(new SqlParameter("@Photo", S.Photo));
			parameters.Add(new SqlParameter("@Username", S.Username));
			parameters.Add(new SqlParameter("@Password", S.Password));
			parameters.Add(new SqlParameter("@IsActive", S.IsActive));
			parameters.Add(new SqlParameter("@DepartmentID", S.DepartmentID));
			parameters.Add(new SqlParameter("@SupervisorID", S.SupervisorID));
			parameters.Add(new SqlParameter("@Designation", S.Designation));

			return DBHelper.ModifyData(query, parameters);
		}

		public static int Update(Staff S)
		{
			string query = "UPDATE Staff SET Name = @Name, Email = @Email, Mobile = @Mobile,Photo = @Photo,Username = @Username,Password = @Password, IsActive = @IsActive,DepartmentID = @DepartmentID,SupervisorID = @SupervisorID, Designation = @Designation WHERE StaffID = @StaffID";
			List<SqlParameter> parameters = new List<SqlParameter>();
			parameters.Add(new SqlParameter("@StaffID", S.StaffID));
			parameters.Add(new SqlParameter("@Name", S.Name));
			parameters.Add(new SqlParameter("@Email", S.Email));
			parameters.Add(new SqlParameter("@Mobile", S.Mobile));
			parameters.Add(new SqlParameter("@Photo", S.Photo));
			parameters.Add(new SqlParameter("@Username", S.Username));
			parameters.Add(new SqlParameter("@Password", S.Password));
			parameters.Add(new SqlParameter("@IsActive", S.IsActive));
			parameters.Add(new SqlParameter("@DepartmentID", S.DepartmentID));
			parameters.Add(new SqlParameter("@SupervisorID", S.SupervisorID));
			parameters.Add(new SqlParameter("@Designation", S.Designation));

			return DBHelper.ModifyData(query, parameters);
		}

		public static Staff SelectByPK(int ID)
		{
			string query = "SELECT * FROM Staff WHERE StaffID = @ID";
			List<SqlParameter> parameters = new List<SqlParameter>();
			parameters.Add(new SqlParameter("@ID", ID));
			DataTable dt = DBHelper.SelectData(query, parameters);

			Staff s = new Staff();
			s.StaffID = ID;
			s.Name = dt.Rows[0]["Name"].ToString();
			s.Email = dt.Rows[0]["Email"].ToString();
			s.Mobile = dt.Rows[0]["Mobile"].ToString();
			s.Photo = dt.Rows[0]["Photo"].ToString();
			s.Username = dt.Rows[0]["Username"].ToString();
			s.Password = dt.Rows[0]["Password"].ToString();
			s.IsActive = Convert.ToBoolean(dt.Rows[0]["IsActive"].ToString());
			s.DepartmentID = Convert.ToInt32(dt.Rows[0]["DepartmentID"].ToString());
			s.SupervisorID = Convert.ToInt32(dt.Rows[0]["SupervisorID"].ToString());
			s.Designation = dt.Rows[0]["Designation"].ToString();

			return s;
		}

		public static int Delete(int ID)
		{
			string query = "DELETE Staff WHERE StaffID = @ID";
			List<SqlParameter> parameters = new List<SqlParameter>();
			parameters.Add(new SqlParameter("@ID", ID));
			return DBHelper.ModifyData(query, parameters);
		}



		public static DataTable SelectALL()
		{
			string query = "SELECT * FROM Staff";
			List<SqlParameter> parameters = new List<SqlParameter>();
			return DBHelper.SelectData(query, parameters);
		}


		public static Staff SelectByUnPw(string Username, string Password)
		{
			string query = "SELECT * FROM Staff WHERE Username = @Username AND Password = @Password";
			List<SqlParameter> parameters = new List<SqlParameter>();
			parameters.Add(new SqlParameter("@Username", Username));
			parameters.Add(new SqlParameter("@Password", Password));
			DataTable dt = DBHelper.SelectData(query, parameters);

			Staff s = new Staff();
			if (dt.Rows.Count > 0)
			{
				s.StaffID = Convert.ToInt32(dt.Rows[0]["StaffID"].ToString());
				s.Name = dt.Rows[0]["Name"].ToString();
				s.Email = dt.Rows[0]["Email"].ToString();
				s.Mobile = dt.Rows[0]["Mobile"].ToString();
				s.Photo = dt.Rows[0]["Photo"].ToString();
				s.Username = dt.Rows[0]["Username"].ToString();
				s.Password = dt.Rows[0]["Password"].ToString();
				s.IsActive = Convert.ToBoolean(dt.Rows[0]["IsActive"].ToString());
				s.DepartmentID = Convert.ToInt32(dt.Rows[0]["DepartmentID"].ToString());
				s.SupervisorID = Convert.ToInt32(dt.Rows[0]["SupervisorID"].ToString());
				s.Designation = dt.Rows[0]["Designation"].ToString();
			}
			return s;
		}

		public static DataTable SelectByPsPm()
		{
			string query = "SELECT * FROM Staff WHERE Designation='Purchase Manager' OR Designation='Purchase Staff'";
			List<SqlParameter> parameters = new List<SqlParameter>();
			return DBHelper.SelectData(query, parameters);
		}

		public static DataTable SelectBySearch(string Name,string Designation)
		{
			string query = "SELECT * FROM Staff WHERE Name LIKE '%'+@Name+'%' AND Designation LIKE '%'+@Designation+'%' ";
			List<SqlParameter> parameters = new List<SqlParameter>();
			parameters.Add(new SqlParameter("@Name", Name));
			
			parameters.Add(new SqlParameter("@Designation", Designation));
			return DBHelper.SelectData(query, parameters);
		}
        public static DataTable SelectAllPOOnIDPen(int ID)
        {
            string query = "SELECT * FROM PurchaseOrder Where FinanceID = @ID AND Status='Import Duty Pending'";
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@ID", ID));
            return DBHelper.SelectData(query, parameters);
        }
        public static DataTable SelectAllPOOnIDPai(int ID)
        {
            string query = "SELECT * FROM PurchaseOrder Where FinanceID = @ID AND Status='Import Duty Paid'";
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@ID", ID));
            return DBHelper.SelectData(query, parameters);
        }
        public static DataTable SelectAllPOOnIClea(int ID)
        {
            string query = "SELECT * FROM PurchaseOrder Where FinanceID = @ID AND Status='Import Cleared'";
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@ID", ID));
            return DBHelper.SelectData(query, parameters);
        }
        public static DataTable SelectAllPO()
        {
            string query = "SELECT * FROM PurchaseOrder Where Status !='Received At Plant' AND Status !='Cancelled'";
            List<SqlParameter> parameters = new List<SqlParameter>();
            return DBHelper.SelectData(query, parameters);
        }
        public static DataTable SelectAllPODelivered()
        {
            string query = "SELECT * FROM PurchaseOrder Where Status ='Received At Plant'";
            List<SqlParameter> parameters = new List<SqlParameter>();
            return DBHelper.SelectData(query, parameters);
        }
        public static DataTable SelectAllPOCAncelled()
        {
            string query = "SELECT * FROM PurchaseOrder Where Status ='Cancelled'";
            List<SqlParameter> parameters = new List<SqlParameter>();
            return DBHelper.SelectData(query, parameters);
        }
        public static DataTable ForgetPasswordSta(string email)
        {
            string query = "SELECT * FROM Staff Where Email=@email";
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@email", email));
            DataTable dt= DBHelper.SelectData(query, parameters);
            return dt;
        }
        public static DataTable ImportDutyPendingHome()
        {
            string query = "SELECT * FROM PurchaseOrder Where Status ='Import Duty Pending' ";
            List<SqlParameter> parameters = new List<SqlParameter>();
            return DBHelper.SelectData(query, parameters);
        }
        public static DataTable Hradminemail()
        {
            string query = "SELECT * FROM Staff Where Designation='Admin'";
            List<SqlParameter> parameters = new List<SqlParameter>();
         
            DataTable dt = DBHelper.SelectData(query, parameters);
            return dt;
        }

        public static DataTable SelectEmail(int ID)
        {
            string query = "SELECT * FROM Staff Where StaffID=@ID";
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@ID", ID));
            DataTable dt = DBHelper.SelectData(query, parameters);
            return dt;
        }
    }
}