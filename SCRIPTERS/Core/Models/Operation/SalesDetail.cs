using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SCRIPTERS.Core.Models.Operation
{
    public class SalesDetail
    {
        [Key]
        public int Id { get; set; }
        public bool IsDeleted { get; set; }
        public int BookId { get; set; }
        public virtual Book Book { get; set; }
       

       
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        public int SaleId { get; set; }
        public virtual Sale Sale { get; set; }
    }
}