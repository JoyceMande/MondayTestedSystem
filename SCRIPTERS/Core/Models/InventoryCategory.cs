using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SCRIPTERS.Core.Models
{
    public class InventoryCategory
    {
        public InventoryCategory()
        {
            Inventories = new List<Inventory>();
        }
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Code { get; set; }

        [DataType(DataType.MultilineText)]
        [StringLength(maximumLength: 1000, ErrorMessage = "Description can not be more then 1000 characters")]
        public string Description { get; set; }

        public List<Inventory> Inventories { get; set; }


    }
}