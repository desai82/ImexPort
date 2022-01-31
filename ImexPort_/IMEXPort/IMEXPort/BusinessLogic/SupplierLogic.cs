using IMEXPort.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace IMEXPort.BusinessLogic
{
	public class SupplierLogic
	{
		public static int Insert(Supplier S)
		{
			string query = "INSERT INTO Supplier VALUES(@Name,@Email,@Mobile,@Photo,@Username,@Password,@IsActive,@Country)";
			List<SqlParameter> parameters = new List<SqlParameter>();
			parameters.Add(new SqlParameter("@Name", S.Name));
			parameters.Add(new SqlParameter("@Email", S.Email));
			parameters.Add(new SqlParameter("@Mobile", S.Mobile));
			parameters.Add(new SqlParameter("@Photo", S.Photo));
			parameters.Add(new SqlParameter("@Username", S.Username));
			parameters.Add(new SqlParameter("@Password", S.Password));
			parameters.Add(new SqlParameter("@IsActive", S.IsActive));
			parameters.Add(new SqlParameter("@Country", S.Country));
			return DBHelper.ModifyData(query, parameters);
		}

		public static int Update(Supplier S)
		{
			string query = "UPDATE Supplier SET Name = @Name, Email = @Email, Mobile = @Mobile,Photo = @Photo,Username = @Username,Password = @Password, IsActive = @IsActive,Country = @Country WHERE SupplierID = @SupplierID";
			List<SqlParameter> parameters = new List<SqlParameter>();
			parameters.Add(new SqlParameter("@SupplierID", S.SupplierID));
			parameters.Add(new SqlParameter("@Name", S.Name));
			parameters.Add(new SqlParameter("@Email", S.Email));
			parameters.Add(new SqlParameter("@Mobile", S.Mobile));
			parameters.Add(new SqlParameter("@Photo", S.Photo));
			parameters.Add(new SqlParameter("@Username", S.Username));
			parameters.Add(new SqlParameter("@Password", S.Password));
			parameters.Add(new SqlParameter("@IsActive", S.IsActive));
			parameters.Add(new SqlParameter("@Country", S.Country));
		
			return DBHelper.ModifyData(query, parameters);
		}

		public static Supplier SelectByPK(int ID)
		{
			string query = "SELECT * FROM Supplier WHERE SupplierID = @ID";
			List<SqlParameter> parameters = new List<SqlParameter>();
			parameters.Add(new SqlParameter("@ID", ID));
			DataTable dt = DBHelper.SelectData(query, parameters);

			Supplier s = new Supplier();
			s.SupplierID = ID;
			s.Name = dt.Rows[0]["Name"].ToString();
			s.Email = dt.Rows[0]["Email"].ToString();
			s.Mobile = dt.Rows[0]["Mobile"].ToString();
			s.Photo = dt.Rows[0]["Photo"].ToString();
			s.Username = dt.Rows[0]["Username"].ToString();
			s.Password = dt.Rows[0]["Password"].ToString();
			s.IsActive = Convert.ToBoolean(dt.Rows[0]["IsActive"].ToString());
			s.Country = dt.Rows[0]["Country"].ToString();

			return s;
		}

		public static int Delete(int ID)
		{
			string query = "DELETE Supplier WHERE SupplierID = @ID";
			List<SqlParameter> parameters = new List<SqlParameter>();
			parameters.Add(new SqlParameter("@ID", ID));
			return DBHelper.ModifyData(query, parameters);
		}



		public static DataTable SelectALL()
		{
			string query = "SELECT * FROM Supplier";
			List<SqlParameter> parameters = new List<SqlParameter>();
			return DBHelper.SelectData(query, parameters);
		}


		public static Supplier SelectByUnPw(string Username, string Password)
		{
			string query = "SELECT * FROM Supplier WHERE Username = @Username AND Password = @Password";
			List<SqlParameter> parameters = new List<SqlParameter>();
			parameters.Add(new SqlParameter("@Username", Username));
			parameters.Add(new SqlParameter("@Password", Password));
			DataTable dt = DBHelper.SelectData(query, parameters);

			Supplier s = new Supplier();
			if (dt.Rows.Count > 0)
			{
				s.SupplierID = Convert.ToInt32(dt.Rows[0]["SupplierID"].ToString());
				s.Name = dt.Rows[0]["Name"].ToString();
				s.Email = dt.Rows[0]["Email"].ToString();
				s.Mobile = dt.Rows[0]["Mobile"].ToString();
				s.Photo = dt.Rows[0]["Photo"].ToString();
				s.Username = dt.Rows[0]["Username"].ToString();
				s.Password = dt.Rows[0]["Password"].ToString();
				s.IsActive = Convert.ToBoolean(dt.Rows[0]["IsActive"].ToString());
				
				s.Country = dt.Rows[0]["Country"].ToString();
			}
			return s;
		}

		public static DataTable SelectBySearch(string Name, string Country)
		{
			string query = "SELECT * FROM Supplier WHERE Name LIKE '%'+@Name+'%' AND Country LIKE '%'+@Country+'%' ";
			List<SqlParameter> parameters = new List<SqlParameter>();
			parameters.Add(new SqlParameter("@Name", Name));

			parameters.Add(new SqlParameter("@Country", Country));
			return DBHelper.SelectData(query, parameters);
		}

		public static DataTable SelectMOS(int ID)
		{

			string query = "SELECT MaterialName FROM Material m1 INNER JOIN MaterialSupplier m2 ON m1.MaterialID=m2.MaterialID where m2.SupplierID=@ID ";
			List<SqlParameter> parameters = new List<SqlParameter>();
			parameters.Add(new SqlParameter("@ID", ID));
			return DBHelper.SelectData(query, parameters);

		}

		public static int SelectBySupplierName(String SupplierName)
		{
			string query = "SELECT SupplierID FROM Supplier WHERE Name=@SupplierName";
			List<SqlParameter> parameters = new List<SqlParameter>();
			parameters.Add(new SqlParameter("@SupplierName", SupplierName));
			DataTable dt = DBHelper.SelectData(query, parameters);

			int SupplierID = Convert.ToInt32(dt.Rows[0]["SupplierID"]);
			return SupplierID;
		}


//------------------------------------------------------------------------------------------------------------

		public static DataTable SelectAllPOForSupp(int ID)
		{
			string query = "SELECT * FROM PurchaseOrder Where SupplierID = @ID AND Status='Released By Supervisor'";
			List<SqlParameter> parameters = new List<SqlParameter>();
			parameters.Add(new SqlParameter("@ID", ID));
			return DBHelper.SelectData(query, parameters);
		}

		public static DataTable SelectAllPOOnSupp(int ID)
		{
			string query = "SELECT * FROM PurchaseOrder Where SupplierID = @ID AND Status='Accepted By Supplier'";
			List<SqlParameter> parameters = new List<SqlParameter>();
			parameters.Add(new SqlParameter("@ID", ID));
			return DBHelper.SelectData(query, parameters);
		}

		public static DataTable SelectAllPORejectbySupp(int ID)
		{
			string query = "SELECT * FROM PurchaseOrder Where SupplierID = @ID AND Status='Rejected By Supplier'";
			List<SqlParameter> parameters = new List<SqlParameter>();
			parameters.Add(new SqlParameter("@ID", ID));
			return DBHelper.SelectData(query, parameters);
		}

		public static DataTable SelectAllPODispatchedbySupp(int ID)
		{
			string query = "SELECT * FROM PurchaseOrder Where SupplierID = @ID AND (Status='Supplier Dispatched' OR Status='FCA Recieved' OR Status='FCA Dispatched' OR Status='ICA Arrived' OR Status='Import Duty Pending' OR Status='Import Duty Paid' OR Status='Import Cleared')";
			List<SqlParameter> parameters = new List<SqlParameter>();
			parameters.Add(new SqlParameter("@ID", ID));
			return DBHelper.SelectData(query, parameters);
		}

		public static DataTable SelectAllPODelivered(int ID)
		{
			string query = "SELECT * FROM PurchaseOrder Where SupplierID = @ID AND Status='Received At Plant'";
			List<SqlParameter> parameters = new List<SqlParameter>();
			parameters.Add(new SqlParameter("@ID", ID));
			return DBHelper.SelectData(query, parameters);
		}

        public static DataTable ForgetPasswordSupp(string email)
        {
            string query = "SELECT * FROM Supplier Where Email=@email";
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@email", email));
            DataTable dt= DBHelper.SelectData(query, parameters);
            return dt;
        }
        public static DataTable SelectForPurchaseNotiSupp(int ID)
        {
            string query = "SELECT * FROM PurchaseOrder Where SupplierID = @ID AND Status='Released By Supervisor'";
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@ID", ID));
            return DBHelper.SelectData(query, parameters);
        }



    }
}