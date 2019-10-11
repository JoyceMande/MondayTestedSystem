using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using SCRIPTERS.Core.Models.Operation;

namespace SCRIPTERS.Core.Models.ViewModel
{
    public class IncomeVm
    {
        public int Id { get; set; }

        [NotMapped]
        public List<Sale> Sales { get; set; }

        [NotMapped]
        public List<Purchase> Purchases { get; set; }

        public decimal SalesTotal { get; set; }
        public decimal PurchasesTotal { get; set; }
        public decimal TotalIncome { get; set; }

    }
}