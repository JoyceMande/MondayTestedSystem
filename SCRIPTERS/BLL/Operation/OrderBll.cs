using System.Collections.Generic;
using SCRIPTERS.Core.Models.Operation;
using SCRIPTERS.DAL.Operation;

namespace SCRIPTERS.BLL.Operation
{
    public class OrderBll
    {
        OrderDal _orderDal = new OrderDal();
        bool _status;
        int id;

        internal List<Order> List()
        {
            List<Order> orders = _orderDal.List();
            return orders;
        }

        internal dynamic GenerateAutoCode()
        {
            var autoCode = _orderDal.GenerateAutoCode();
            return autoCode;
        }

        internal int Create(Order order)
        {
            id = _orderDal.Create(order);
            return id;
        }
        internal bool Delete(int id)
        {
            _status = _orderDal.Delete(id);
            return _status;
        }

        internal Order Details(int id)
        {
            Order order = _orderDal.Details(id);
            return order;
        }

        internal dynamic GetOutlet()
        {
            var outlet = _orderDal.GetOutlet();
            return outlet;
        }
        internal dynamic GetEmployee()
        {
            var employee = _orderDal.GetEmployee();
            return employee;
        }

        internal dynamic Supplier()
        {
            var supplier = _orderDal.Supplier();
            return supplier;
        }

        internal dynamic GetItem()
        {
            var item = _orderDal.GetItem();
            return item;
        }
        internal Order GetById(int? id)
        {
            Order order = _orderDal.GetById(id);
            return order;
        }
        internal bool Edit(Order order)
        {
            _status = _orderDal.Edit(order);
            return _status;
        }
    }
}