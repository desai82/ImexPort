using IMEXPort.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace IMEXPort.BusinessLogic
{
	public class CustomsAgentLogic
	{
		public static int Insert(CustomsAgent C)
		{
			string query = "INSERT INTO CustomsAgent VALUES(@Name,@Email,@Mobile,@Photo,@Username,@Password,@IsActive,@Country,@Port)";
			List<SqlParameter> parameters = new List<SqlParameter>();
			parameters.Add(new SqlParameter("@Name", C.Name));
			parameters.Add(new SqlParameter("@Email", C.Email));
			parameters.Add(new SqlParameter("@Mobile", C.Mobile));
			parameters.Add(new SqlParameter("@Photo", C.Photo));
			parameters.Add(new SqlParameter("@Username", C.Username));
			parameters.Add(new SqlParameter("@Password", C.Password));
			parameters.Add(new SqlParameter("@IsActive", C.IsActive));
			parameters.Add(new SqlParameter("@Country", C.Country));
            parameters.Add(new SqlParameter("@Port", C.Port));
            return DBHelper.ModifyData(query, parameters);
		}

		public static int Update(CustomsAgent C)
		{
			string query = "UPDATE CustomsAgent SET Name = @Name, Email = @Email, Mobile = @Mobile,Photo = @Photo,Username = @Username,Password = @Password, IsActive = @IsActive,Country = @Country,Port=@Port WHERE CustomsAgentID = @CustomsAgentID";
			List<SqlParameter> parameters = new List<SqlParameter>();
			parameters.Add(new SqlParameter("@CustomsAgentID", C.CustomsAgentID));
			parameters.Add(new SqlParameter("@Name", C.Name));
			parameters.Add(new SqlParameter("@Email", C.Email));
			parameters.Add(new SqlParameter("@Mobile", C.Mobile));
			parameters.Add(new SqlParameter("@Photo", C.Photo));
			parameters.Add(new SqlParameter("@Username", C.Username));
			parameters.Add(new SqlParameter("@Password", C.Password));
			parameters.Add(new SqlParameter("@IsActive", C.IsActive));
			parameters.Add(new SqlParameter("@Country", C.Country));
            parameters.Add(new SqlParameter("@Port", C.Port));
            return DBHelper.ModifyData(query, parameters);
		}

		public static CustomsAgent SelectByPK(int ID)
		{
			string query = "SELECT * FROM CustomsAgent WHERE CustomsAgentID = @ID";
			List<SqlParameter> parameters = new List<SqlParameter>();
			parameters.Add(new SqlParameter("@ID", ID));
			DataTable dt = DBHelper.SelectData(query, parameters);

			CustomsAgent C = new CustomsAgent();
			C.CustomsAgentID = ID;
			C.Name = dt.Rows[0]["Name"].ToString();
			C.Email = dt.Rows[0]["Email"].ToString();
			C.Mobile = dt.Rows[0]["Mobile"].ToString();
			C.Photo = dt.Rows[0]["Photo"].ToString();
			C.Username = dt.Rows[0]["Username"].ToString();
			C.Password = dt.Rows[0]["Password"].ToString();
			C.IsActive = Convert.ToBoolean(dt.Rows[0]["IsActive"].ToString());
			C.Country = dt.Rows[0]["Country"].ToString();
            C.Port = dt.Rows[0]["Port"].ToString();
            return C;
		}

		public static int Delete(int ID)
		{
			string query = "DELETE CustomsAgent WHERE CustomsAgentID = @ID";
			List<SqlParameter> parameters = new List<SqlParameter>();
			parameters.Add(new SqlParameter("@ID", ID));
			return DBHelper.ModifyData(query, parameters);
		}



		public static DataTable SelectALL()
		{
			string query = "SELECT * FROM CustomsAgent";
			List<SqlParameter> parameters = new List<SqlParameter>();
			return DBHelper.SelectData(query, parameters);
		}


		public static CustomsAgent SelectByUnPw(string Username, string Password)
		{
			string query = "SELECT * FROM CustomsAgent WHERE Username = @Username AND Password = @Password";
			List<SqlParameter> parameters = new List<SqlParameter>();
			parameters.Add(new SqlParameter("@Username", Username));
			parameters.Add(new SqlParameter("@Password", Password));
			DataTable dt = DBHelper.SelectData(query, parameters);

			CustomsAgent C = new CustomsAgent();
			if (dt.Rows.Count > 0)
			{
				C.CustomsAgentID = Convert.ToInt32(dt.Rows[0]["CustomsAgentID"].ToString());
				C.Name = dt.Rows[0]["Name"].ToString();
				C.Email = dt.Rows[0]["Email"].ToString();
				C.Mobile = dt.Rows[0]["Mobile"].ToString();
				C.Photo = dt.Rows[0]["Photo"].ToString();
				C.Username = dt.Rows[0]["Username"].ToString();
				C.Password = dt.Rows[0]["Password"].ToString();
				C.IsActive = Convert.ToBoolean(dt.Rows[0]["IsActive"].ToString());
				C.Country = dt.Rows[0]["Country"].ToString();
                C.Port = dt.Rows[0]["Port"].ToString();
            }
			return C;
		}

		public static DataTable SelectBySearch(string Name, string Country)
		{
			string query = "SELECT * FROM CustomsAgent WHERE Name LIKE '%'+@Name+'%' AND Country LIKE '%'+@Country+'%' ";
			List<SqlParameter> parameters = new List<SqlParameter>();
			parameters.Add(new SqlParameter("@Name", Name));

			parameters.Add(new SqlParameter("@Country", Country));
			return DBHelper.SelectData(query, parameters);
		}

		public static DataTable SelectByICACountry()
		{
				string query = "SELECT * FROM CustomsAgent WHERE Country = 'INDIA'";
				List<SqlParameter> parameters = new List<SqlParameter>();
				return DBHelper.SelectData(query, parameters);
		}

		public static DataTable SelectByFCACountry()
		{
			string query = "SELECT * FROM CustomsAgent WHERE Country != 'INDIA'";
			List<SqlParameter> parameters = new List<SqlParameter>();
			return DBHelper.SelectData(query, parameters);
		}

		public static DataTable SelectByCountry(string Country)
		{
			string query = "SELECT * FROM CustomsAgent WHERE Country = @Country";
			List<SqlParameter> parameters = new List<SqlParameter>();
			parameters.Add(new SqlParameter("@Country",Country));
			return DBHelper.SelectData(query,parameters);
		}

		public static DataTable ForgetPasswordCusag(string email)
		{
			string query = "SELECT * FROM CustomsAgent Where Email=@email";
			List<SqlParameter> parameters = new List<SqlParameter>();
			parameters.Add(new SqlParameter("@email", email));
			DataTable dt = DBHelper.SelectData(query, parameters);
			return dt;
		}



	}
}