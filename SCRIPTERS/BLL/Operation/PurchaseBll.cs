using System.Collections.Generic;
using SCRIPTERS.Core.Models.Operation;
using SCRIPTERS.DAL.Operation;

namespace SCRIPTERS.BLL.Operation
{
    public class PurchaseBll
    {
        PurchaseDal _purchaseDal = new PurchaseDal();
        bool status;
        int id;

        internal List<Purchase> List()
        {
            List<Purchase> purchases = _purchaseDal.List();
            return purchases;
        }

        internal dynamic GenerateAutoCode()
        {
            var autoCode = _purchaseDal.GenerateAutoCode();
            return autoCode;
        }

        internal int Create(Purchase purchase)
        {
            id = _purchaseDal.Create(purchase);
            return id;
        }
        internal bool Delete(int id)
        {
            status = _purchaseDal.Delete(id);
            return status;
        }

        internal Purchase Details(int id)
        {
            Purchase purchase = _purchaseDal.Details(id);
            return purchase;
        }

        internal dynamic GetOutlet()
        {
            var outlet = _purchaseDal.GetOutlet();
            return outlet;
        }
        internal dynamic GetEmployee()
        {
            var employee = _purchaseDal.GetEmployee();
            return employee;
        }

        internal dynamic Supplier()
        {
            var supplier = _purchaseDal.Supplier();
            return supplier;
        }

        internal dynamic GetItem()
        {
            var item = _purchaseDal.GetItem();
            return item;
        }
        internal Purchase GetById(int? id)
        {
            Purchase purchase = _purchaseDal.GetById(id);
            return purchase;
        }
        internal bool Edit(Purchase purchase)
        {
            status = _purchaseDal.Edit(purchase);
            return status;
        }
    }
}