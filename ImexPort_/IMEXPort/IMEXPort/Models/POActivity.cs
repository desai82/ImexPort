using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IMEXPort.Models
{
	public class POActivity
	{
		public int POActivityID, PurchaseOrderID, StaffID,SupplierID,FCAID,ICAID;
		public string Remarks ,ActivityType;
		public DateTime ActivityDate;

	}

}