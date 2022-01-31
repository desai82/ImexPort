using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IMEXPort.Models
{
	public class POItem
	{
		public int POItemID, PurchaseOrderID, MaterialID;
		public string Remarks;
		public float Quantity, Rate;
	}
}