using System.Collections.Generic;
using SCRIPTERS.Core.Models.Operation;
using SCRIPTERS.DAL;

namespace SCRIPTERS.BLL
{
    public class InventoryCommonBll
    {
        InventoryCommonDal _commonDal = new InventoryCommonDal();

        internal dynamic GetItemStockById(int id)
        {
            List<OrderDetail> purchaseDetails = _commonDal.GetOrderDetailsById(id);
            List<InventorySalesDetail> salesDetails = _commonDal.GetInventorySalesDetailsById(id);
            var totalOrder = 0;
            var totalSales = 0;
            foreach (var item in purchaseDetails)
            {
                totalOrder = totalOrder + item.Quantity;
            }
            foreach (var item in salesDetails)
            {
                totalSales = totalSales + item.Quantity;
            }
            var ItemStock = totalOrder - totalSales;

            if (ItemStock < 0)
            {
                ItemStock = 0;
            }
            return ItemStock;

        }

    }
}