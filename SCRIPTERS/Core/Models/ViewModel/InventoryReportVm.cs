using System;

namespace SCRIPTERS.Core.Models.ViewModel
{
    public class InventoryReportVm
    {
        public int? OutletId { get; set; }
        public int? Code { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }

    }
}