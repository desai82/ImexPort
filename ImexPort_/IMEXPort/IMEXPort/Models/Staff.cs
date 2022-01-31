using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IMEXPort.Models
{
	public class Staff
	{
		public int  StaffID, DepartmentID, SupervisorID;
		public string Mobile, Name, Email,Photo, Username, Password,  Designation;
		public bool IsActive;
	}
}