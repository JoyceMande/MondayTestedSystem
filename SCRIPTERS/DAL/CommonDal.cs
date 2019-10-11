using System.Collections.Generic;
using System.Linq;

using SCRIPTERS.Core.Models.Operation;
using SCRIPTERS.Models;

namespace SCRIPTERS.DAL
{
    public class CommonDal
    {
        ApplicationDbContext _db = new ApplicationDbContext();
        internal List<PurchaseDetail> GetPurchaseDetailsById(int id)
        {
            List<PurchaseDetail> purchaseDetails = _db.PurchaseDetails.Where(m => m.ItemId == id && m.IsDeleted==false).ToList();
            return purchaseDetails;
        }
      

        internal List<SalesDetail> GetSalesDetailsById(int id)
        {
            List<SalesDetail> salesDetails = _db.SalesDetails.Where(m => m.BookId == id && m.IsDeleted == false).ToList();
            return salesDetails;
        }
       
    }
}