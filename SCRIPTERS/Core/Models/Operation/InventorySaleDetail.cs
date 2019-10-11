using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SCRIPTERS.Core.Models.Operation
{
    public class InventorySalesDetail
    {
        [Key]
        public int Id { get; set; }
        public bool IsDeleted { get; set; }
        public int InventoryId { get; set; }
        public virtual Inventory Inventory { get; set; }
       

       
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        public int InventorySaleId { get; set; }
        public virtual InventorySale InventorySale { get; set; }
    }
}