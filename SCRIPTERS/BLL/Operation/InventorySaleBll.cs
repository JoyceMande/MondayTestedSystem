using System.Collections.Generic;
using SCRIPTERS.Core.Models.Operation;
using SCRIPTERS.DAL.Operation;

namespace SCRIPTERS.BLL.Operation
{
    public class InventorySaleBll
    {
        InventorySaleDal _inventorySaleDal = new InventorySaleDal();
        bool status;
        int id;

        internal List<InventorySale> List()
        {
            List<InventorySale> inventorySales = _inventorySaleDal.List();
            return inventorySales;
        }

        internal dynamic GenerateAutoCode()
        {
            var autoCode = _inventorySaleDal.GenerateAutoCode();
            return autoCode;
        }

        internal int Create(InventorySale inventorySale)
        {
            id = _inventorySaleDal.Create(inventorySale);
            return id;
        }

        internal bool Delete(int id)
        {
            status = _inventorySaleDal.Delete(id);
            return status;
        }

        internal dynamic GetOutlet()
        {
            var outlet = _inventorySaleDal.GetOutlet();
            return outlet;
        }
        internal dynamic GetEmployee()
        {
            var Employee = _inventorySaleDal.GetEmployee();
            return Employee;
        }

        internal dynamic GetItem()
        {
            var item = _inventorySaleDal.GetItem();
            return item;
        }

        internal InventorySale GetById(int? id)
        {
            InventorySale inventorySale = _inventorySaleDal.GetById(id);
            return inventorySale;
        }

        internal bool Edit(InventorySale inventorySale)
        {
            status = _inventorySaleDal.Edit(inventorySale);
            return status;
        }

    }
}