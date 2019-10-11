using System.Collections.Generic;
using SCRIPTERS.Core.Models.Operation;
using SCRIPTERS.DAL.Operation;

namespace SCRIPTERS.BLL.Operation
{
    public class SaleBll
    {
        SaleDal _saleDal = new SaleDal();
        bool status;
        int id;

        internal List<Sale> List()
        {
            List<Sale> sales = _saleDal.List();
            return sales;
        }

        internal dynamic GenerateAutoCode()
        {
            var autoCode = _saleDal.GenerateAutoCode();
            return autoCode;
        }

        internal int Create(Sale sale)
        {
            id = _saleDal.Create(sale);
            return id;
        }

        internal bool Delete(int id)
        {
            status = _saleDal.Delete(id);
            return status;
        }

        internal dynamic GetOutlet()
        {
            var Outlet = _saleDal.GetOutlet();
            return Outlet;
        }
        internal dynamic GetEmployee()
        {
            var Employee = _saleDal.GetEmployee();
            return Employee;
        }

        internal dynamic GetItem()
        {
            var Item = _saleDal.GetItem();
            return Item;
        }

        internal Sale GetById(int? id)
        {
            Sale sale = _saleDal.GetById(id);
            return sale;
        }

        internal bool Edit(Sale sale)
        {
            status = _saleDal.Edit(sale);
            return status;
        }

    }
}