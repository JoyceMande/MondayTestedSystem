using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using SCRIPTERS.Core.Models.Operation;

namespace SCRIPTERS.Core.Models.ViewModel
{
    public class InventoryIncomeVm
    {
        public int Id { get; set; }

        [NotMapped]
        public List<InventorySale> Sales { get; set; }

        [NotMapped]
        public List<Order> Orders { get; set; }

        public decimal SalesTotal { get; set; }
        public decimal OrdersTotal { get; set; }
        public decimal TotalIncome { get; set; }

    }
}