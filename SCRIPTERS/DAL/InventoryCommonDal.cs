using System.Collections.Generic;
using System.Linq;

using SCRIPTERS.Core.Models.Operation;
using SCRIPTERS.Models;

namespace SCRIPTERS.DAL
{
    public class InventoryCommonDal
    {
        ApplicationDbContext _db = new ApplicationDbContext();
        internal List<OrderDetail> GetOrderDetailsById(int id)
        {
            List<OrderDetail> orderDetail = _db.OrderDetails.Where(m => m.ItemId == id && m.IsDeleted==false).ToList();
            return orderDetail;
        }
      

        internal List<InventorySalesDetail> GetInventorySalesDetailsById(int id)
        {
            List<InventorySalesDetail> salesDetails = _db.InventorySalesDetails.Where(m => m.InventoryId == id && m.IsDeleted == false).ToList();
            return salesDetails;
        }
       
    }
}