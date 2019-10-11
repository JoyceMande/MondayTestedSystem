using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SCRIPTERS.Core.Models.Operation
{
    public class PurchaseDetail
    {
        [Key]
        public int Id { get; set; }
        public bool IsDeleted { get; set; }

        public int ItemId { get; set; }
        public virtual Book Item { get; set; }

        public int Quantity { get; set; }
        public decimal Price { get; set; }

        public int PurchaseId { get; set; }
        public virtual Purchase Purchase { get; set; }
    }
}