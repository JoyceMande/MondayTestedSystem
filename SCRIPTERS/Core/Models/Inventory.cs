using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using SCRIPTERS.Core.Models.Operation;

namespace SCRIPTERS.Core.Models
{
    public class Inventory
    {
        public Inventory()
        {
            OrderDetails = new List<OrderDetail>();
            InventorySalesDetail = new List<InventorySalesDetail>();
        }
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }


        [Display(Name = "Cost Price")]
        public decimal CostPrice { get; set; }

        [Required]
        [Display(Name = "Sale Price")]
        public decimal SalePrice { get; set; }

        [Required]
        [Display(Name = "Inventory Code")]
        public string Code { get; set; }

        [DataType(DataType.MultilineText)]
        [StringLength(maximumLength: 1000, ErrorMessage = "Description can not be more then 1000 characters")]
        public string Description { get; set; }

        public int ItemCategoryId { get; set; }
        public virtual InventoryCategory ItemCategory { get; set; }

        public byte[] Image { get; set; }

        public virtual List<InventorySalesDetail> InventorySalesDetail { get; set; }
        public virtual List<OrderDetail> OrderDetails { get; set; }

    }
}