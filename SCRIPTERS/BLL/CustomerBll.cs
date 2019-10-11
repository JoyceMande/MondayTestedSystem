using System.Collections.Generic;
using SCRIPTERS.Core.Models;
using SCRIPTERS.DAL;

namespace SCRIPTERS.BLL
{
    public class CustomerBll
    {
        readonly CustomerDal _customerDal = new CustomerDal();
        bool _status;

        internal List<Customer> List()
        {
            List<Customer> customers = _customerDal.List();
            return customers;
        }
        internal dynamic GenerateAutoCode()
        {
            var autoCode = _customerDal.GenerateAutoCode();
            return autoCode;
        }

        internal bool Create(Customer customer)
        {
            _status = _customerDal.Create(customer);
            return _status;
        }

        internal bool Delete(int id)
        {
            _status = _customerDal.Delete(id);
            return _status;
        }

        internal Customer GetById(int? id)
        {
            Customer customer = _customerDal.GetById(id);
            return customer;
        }

        internal bool Edit(Customer customer)
        {
            _status = _customerDal.Edit(customer);
            return _status;
        }
    }
}