using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IMEXPort.Models
{
	public class PurchaseOrder
	{
		public int PurchaseOrderID, StaffID, SupplierID, ApproverID, FCAID, ICAID, FinanceID;
		public string Status, QRCode,InvoiceFile, ExportClearanceFile, ImportDutyChallanFile,ImportDutyPaymentFile, ImportClearanceFile;
		public DateTime CreateDate, RequestedDate, TentativeDate,DispatchdatebySupp,DispatchdatebyFCA;
        
	}
}