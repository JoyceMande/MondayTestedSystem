using System.Collections.Generic;
using SCRIPTERS.Core.Models.Operation;
using SCRIPTERS.DAL;

namespace SCRIPTERS.BLL
{
    public class CommonBll
    {
        CommonDal _commonDal = new CommonDal();

        internal dynamic GetItemStockById(int id)
        {
            List<PurchaseDetail> purchaseDetails = _commonDal.GetPurchaseDetailsById(id);
            List<SalesDetail> salesDetails = _commonDal.GetSalesDetailsById(id);
            var totalPurchase = 0;
            var totalSales = 0;
            foreach (var item in purchaseDetails)
            {
                totalPurchase = totalPurchase + item.Quantity;
            }
            foreach (var item in salesDetails)
            {
                totalSales = totalSales + item.Quantity;
            }
            var ItemStock = totalPurchase - totalSales;

            if (ItemStock < 0)
            {
                ItemStock = 0;
            }
            return ItemStock;

        }

    }
}