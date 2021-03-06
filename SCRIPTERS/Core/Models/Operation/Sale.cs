﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SCRIPTERS.Core.Models.Operation
{
    public class Sale
    {
        public Sale()
        {
            SalesDetails = new List<SalesDetail>();
        }
        [Key]
        public int Id { get; set; }
        public bool IsDeleted { get; set; }

        [Display(Name = "Outlet")]
        public int OutletId { get; set; }
        public virtual Outlet Outlet { get; set; }

        [Display(Name = "Sale Number")]
        public string SaleNumber { get; set; }

        [Display(Name = "Employee")]
        public int EmployeeId { get; set; }
        public virtual Employee Employee { get; set; }

        [Required]
        [Display(Name = "Sale Date")]
        public DateTime SaleDate { get; set; }

        [Required]
        [Display(Name = "Customer Contract")]
        public string CusContractNo { get; set; }

        [Required]
        [Display(Name = "Customer Name")]
        public string CusName { get; set; }

        [Display(Name = "Sub Total")]
        public decimal SubTotal { get; set; }
        public decimal Vat { get; set; }
        public decimal Discount { get; set; }

        public decimal Total { get; set; }

        [Display(Name = "Due Amount")]
        public decimal DueAmount { get; set; }

        public virtual List<SalesDetail> SalesDetails { get; set; }
    }
}