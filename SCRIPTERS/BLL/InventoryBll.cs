using System.Collections.Generic;
using SCRIPTERS.Core.Models;
using SCRIPTERS.DAL;

namespace SCRIPTERS.BLL
{
    public class InventoryBll
    {
        InventoryDal _inventoryDal = new InventoryDal();
        bool _status;

        internal List<Inventory> List()
        {
            List<Inventory> inventories = _inventoryDal.List();
            return inventories;
        }

        internal dynamic GenerateAutoCode()
        {
            var autoCode = _inventoryDal.GenerateAutoCode();
            return autoCode;
        }

        internal bool Create(Inventory inventory)
        {
            _status = _inventoryDal.Create(inventory);
            return _status;
        }

        internal bool Delete(int id)
        {
            _status = _inventoryDal.Delete(id);
            return _status;
        }

        internal dynamic GetItemCategory()
        {
            var inventoryCategory = _inventoryDal.GetInventoryCategory();
            return inventoryCategory;
        }

        internal Inventory GetById(int? id)
        {
            Inventory inventory = _inventoryDal.GetById(id);
            return inventory;
        }

        internal bool Edit(Inventory inventory)
        {
            _status = _inventoryDal.Edit(inventory);
            return _status;
        }
    }
}