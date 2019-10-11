using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SCRIPTERS.Core.Models.Operation
{
    public class Purchase
    {
        public Purchase()
        {
            PurchaseDetail = new List<PurchaseDetail>();
        }
        [Key]
        public int Id { get; set; }
        public bool IsDeleted { get; set; }

        [Display(Name = "Outlet")]
        public int OutletId { get; set; }
        public virtual Outlet Outlet { get; set; }

        public string PurchaseNumber { get; set; }

        [Display(Name = "Employee")]
        public int EmployeeId { get; set; }
        public virtual Employee Employee { get; set; }

        [Required]
        [Display(Name = "Purchase Date")]
        public DateTime PurchaseDate { get; set; }

        [Display(Name = "Supplier Name")]
        public int SupplierId { get; set; }
        public virtual Supplier Supplier { get; set; }

        [DataType(DataType.MultilineText)]
        public string Remarks { get; set; }

        public decimal Total { get; set; }

        [Display(Name = "Due Amount")]
        public decimal DueAmount { get; set; }

        public virtual List<PurchaseDetail> PurchaseDetail { get; set; }
    }
}