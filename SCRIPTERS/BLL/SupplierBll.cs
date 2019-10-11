using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SCRIPTERS.Core.Models;
using SCRIPTERS.DAL;

namespace SCRIPTERS.BLL
{
    public class SupplierBll
    {
        SupplierDal _supplierDal = new SupplierDal();
        bool status;

        internal List<Supplier> List()
        {
            List<Supplier> suppliers = _supplierDal.List();
            return suppliers;
        }
        internal dynamic GenerateAutoCode()
        {
            var autoCode = _supplierDal.GenerateAutoCode();
            return autoCode;
        }

        internal bool Create(Supplier Supplier)
        {
            status = _supplierDal.Create(Supplier);
            return status;
        }

        internal bool Delete(int id)
        {
            status = _supplierDal.Delete(id);
            return status;
        }

        internal Supplier GetById(int? id)
        {
            Supplier supplier = _supplierDal.GetById(id);
            return supplier;
        }

        internal bool Edit(Supplier supplier)
        {
            status = _supplierDal.Edit(supplier);
            return status;
        }
    }
}