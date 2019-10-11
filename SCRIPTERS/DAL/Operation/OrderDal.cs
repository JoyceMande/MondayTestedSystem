using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web.Mvc;
using SCRIPTERS.Core.Models;
using SCRIPTERS.Core.Models.Operation;
using SCRIPTERS.Models;

namespace SCRIPTERS.DAL.Operation
{
    public class OrderDal
    {
        ApplicationDbContext _db = new ApplicationDbContext();
        ApplicationUser user = new ApplicationUser();
        private Audit transaction;
        bool _status = false;
        int _start = 0;
        int _id;

        internal List<Order> List()
        {
            List<Order> sales = _db.Orders.ToList();
            return sales;
        }

        internal object GetOutlet()
        {
            var outlet = new SelectList(_db.Outlets, "Id", "Name");
            return outlet;
        }

        internal object GenerateAutoCode()
        {
            string autoCode = "";
            string lastCode = _db.Orders.Max(item => item.OrderNumber);

            if (lastCode != null)
            {
                string resultString = Regex.Match(lastCode, @"\d+").Value;
                _start = Int32.Parse(resultString);

                autoCode = "Order" + (_start + 1).ToString("000");
            }
            autoCode = "Order" + (_start + 1).ToString("000");

            return autoCode;
        }

        internal object GetEmployee()
        {
            var employee = new SelectList(_db.Employees, "Id", "Name");
            return employee;
        }

        internal Order Details(int id)
        {
            Order order = _db.Orders.SingleOrDefault(m => m.Id == id && m.IsDeleted == false);
            return order;
        }

        internal Order GetById(int? id)
        {
            Order order = _db.Orders.FirstOrDefault(m => m.Id == id && m.IsDeleted == false);
            return order;
        }

        internal bool Edit(Order order)
        {
            _db.Orders.Attach(order);
            _db.Entry(order).State = EntityState.Modified;
            transaction = new Audit();
            transaction.TransactionDate = DateTime.Now.Date;
            transaction.TransactionTime = DateTime.Now;
            transaction.User = user.UserName;
            transaction.TransactionType = "Modified Order transaction" + " " + order.Id;
            transaction.TransactionDetails = order.OrderNumber;
            _db.Audits.Add(transaction);
            int affectedRow = _db.SaveChanges();
            if (affectedRow > 0)
            {
                _status = true;
            }
            return _status;
        }

        internal object Supplier()
        {
            var supplier = new SelectList(_db.Suppliers.Where(m => m.SupplierType == "Inventory Supplier"), "Id", "Name");
            return supplier;
        }

        internal object GetItem()
        {
            var item = new SelectList(_db.Inventories, "Id", "Name");
            return item;
        }

        internal bool Delete(int id)
        {
            var orderById = _db.Orders.FirstOrDefault(m => m.Id == id);

            if (orderById != null)
            {
                _db.Entry(orderById).State = EntityState.Deleted;
                transaction = new Audit();
                transaction.TransactionDate = DateTime.Now.Date;
                transaction.TransactionTime = DateTime.Now;
                transaction.User = "User";
                transaction.TransactionType = "Deleted Order transaction" + " " + orderById.Id;
                transaction.TransactionDetails = orderById.OrderNumber;
                _db.Audits.Add(transaction);

                int affectedRow = _db.SaveChanges();

                if (affectedRow > 0)
                {
                    _status = true;
                }
            }
            return _status;
        }

        internal int Create(Order order)
        {
            transaction = new Audit();
            transaction.TransactionDate = DateTime.Now.Date;
            transaction.TransactionTime = DateTime.Now;
            transaction.User = "User";
            transaction.TransactionType = "Created Order transaction" + " " + order.Id;
            transaction.TransactionDetails = order.OrderNumber;
            _db.Audits.Add(transaction);
            _db.Orders.Add(order);
            int rowAffected = _db.SaveChanges();
            if (rowAffected > 0)
            {
                _id = _db.Orders.Max(m => m.Id);
            }
            return _id;
        }
    }
}