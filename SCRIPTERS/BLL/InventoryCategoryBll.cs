using System.Collections.Generic;
using SCRIPTERS.Core.Models;
using SCRIPTERS.DAL;

namespace SCRIPTERS.BLL
{
    public class InventoryCategoryBll
    {
        InventoryCategoryDal _inventoryCategoryDal = new InventoryCategoryDal();
        bool _status;

        internal List<InventoryCategory> List()
        {
            List<InventoryCategory> inventoryCategories = _inventoryCategoryDal.List();

            return inventoryCategories;
        }

        internal dynamic GenerateAutoCode()
        {
            var autoCode = _inventoryCategoryDal.GenerateAutoCode();
            return autoCode;
        }

        internal bool Create(InventoryCategory itemCategory)
        {
            _status = _inventoryCategoryDal.Create(itemCategory);

            return _status;
        }

        internal bool Delete(int id)
        {
            _status = _inventoryCategoryDal.Delete(id);

            return _status;
        }

        internal InventoryCategory GetById(int? id)
        {
            InventoryCategory inventoryCategory = _inventoryCategoryDal.GetById(id);
            return inventoryCategory;
        }

        internal bool Edit(InventoryCategory inventoryCategory)
        {
            _status = _inventoryCategoryDal.Edit(inventoryCategory);
            return _status;
        }
    }
}