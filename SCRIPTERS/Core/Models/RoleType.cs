using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SCRIPTERS.Core.Models
{
    public class RoleType
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public bool CanOrder { get; set; }
        public bool CanPurchase { get; set; }
        public bool CanSale { get; set; }
        public bool CanHr { get; set; }
        public bool CanManageInventory { get; set; }
        public bool CanManageInventoryType { get; set; }
        public bool CanManageBooks { get; set; }
        public bool CanManageBookType { get; set; }
        public bool CanManageReports { get; set; }
        public bool CanManageCustomers { get; set; }
      
      

    }
}