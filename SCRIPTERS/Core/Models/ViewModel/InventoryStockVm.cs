namespace SCRIPTERS.Core.Models.ViewModel
{
    public class InventoryStockVm
    {
        public string ItemName { get; set; }
        public string CategoryFullPath { get; set; }
        public int StockQuantity { get; set; }
        public decimal Price { get; set; }
    }
}