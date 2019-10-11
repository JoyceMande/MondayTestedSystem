using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using SCRIPTERS.Core.Models.Operation;

namespace SCRIPTERS.Core.Models
{
    public class Employee
    {
        public Employee()
        {
            Expenses = new List<Expense>();
            Purchases= new List<Purchase>();
            Sales = new List<Sale>();
            InventorySales = new List<InventorySale>();
            Orders = new List<Order>();
        }
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        public string Code { get; set; }

        public int OutletId { get; set; }
        public virtual Outlet Outlet { get; set; }

        [Display(Name = "Joining Date")]
        public DateTime JoiningDate { get; set; }

        public byte[] Image { get; set; }

       
        [Display(Name = "Contact Number")]
        [DataType(DataType.PhoneNumber)]
        [Required(ErrorMessage = "Pleas enter valid phone number")]
        public string ContactNo { get; set; }

        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "Pleas enter valid Email ID")]
        public string Email { get; set; }

        public int? ReferenceId { get; set; }
        public virtual Employee Reference { get; set; }
        public virtual List<Employee> Employees { get; set; }

        [DataType(DataType.PhoneNumber)]
        [Required(ErrorMessage = "Pleas enter valid phone number")]
        public string EmerContactNo { get; set; }

        [Display(Name = "National ID")]
        public string NationalId { get; set; }

        [Display(Name = "Father's Name")]
        public string FathersName { get; set; }

        [Display(Name = "Mother's Name")]
        public string MothersName { get; set; }

        [Display(Name = "Present Address")]
        public string PresentAddress { get; set; }

        [Required]
        [Display(Name = "Permanent Address")]
        public string PermanentAddress { get; set; }

        public virtual List<Sale> Sales { get; set; }
        public virtual List<InventorySale> InventorySales { get; set; }
        public virtual List<Purchase> Purchases { get; set; }
        public virtual List<Expense> Expenses { get; set; }
        public virtual List<Order> Orders { get; set; }

        //-----------------------------------------------
        public RoleType RoleType { get; set; }
        public int RoleTyeId { get; set; }

    }
}