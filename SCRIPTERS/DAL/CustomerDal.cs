using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using SCRIPTERS.Core.Models;
using SCRIPTERS.Models;

namespace SCRIPTERS.DAL
{

    public class CustomerDal
    {
        ApplicationDbContext _db = new ApplicationDbContext();
        private Audit transaction;
        //User.GetUserId();

        bool _status = false;
        int _start = 0;

        internal List<Customer> List()
        {
            List<Customer> customers = _db.Customers.ToList();
            return customers;
        }

        internal object GenerateAutoCode()
        {
            string autoCode = "";
            string lastCode = _db.Customers.Max(item => item.Code);

            if (lastCode != null)
            {
                string resultString = Regex.Match(lastCode, @"\d+").Value;
                _start = Int32.Parse(resultString);

                autoCode = "C" + (_start + 1).ToString("000");
            }
            autoCode = "C" + (_start + 1).ToString("000");

            return autoCode;
        }


        internal Customer GetById(int? id)
        {
            Customer Customer = _db.Customers.FirstOrDefault(m => m.Id == id);
            return Customer;
        }

        internal bool Edit(Customer customer)
        {
            _db.Customers.Attach(customer);
           
            _db.Entry(customer).State = EntityState.Modified;
            transaction = new Audit();
            transaction.TransactionDate = DateTime.Now.Date;
            transaction.TransactionTime = DateTime.Now;
            transaction.User = "User";
            transaction.TransactionType = "Modified Customer";
            transaction.TransactionDetails = customer.Name;
            _db.Audits.Add(transaction);
            int AffectedRow = _db.SaveChanges();
            if (AffectedRow > 0)
            {
                _status = true;
            }
            return _status;
        }

        internal bool Delete(int id)
        {
            var CustomerById = _db.Customers.FirstOrDefault(m => m.Id == id);

            if (CustomerById != null)
            {
                _db.Entry(CustomerById).State = EntityState.Deleted;
                transaction = new Audit();
                transaction.TransactionDate = DateTime.Now.Date;
                transaction.TransactionTime = DateTime.Now;
                transaction.User = "User";
                transaction.TransactionType = "Deleted Customer";
                transaction.TransactionDetails = CustomerById.Name;
                _db.Audits.Add(transaction);
                int affectedRow = _db.SaveChanges();

                if (affectedRow > 0)
                {
                    _status = true;
                }
            }
            return _status;
        }

        internal bool Create(Customer customer)
        {
            transaction = new Audit();
            transaction.TransactionDate = DateTime.Now.Date;
            transaction.TransactionTime = DateTime.Now;
            transaction.User = "User";
            transaction.TransactionType = "Added Customer";
            transaction.TransactionDetails = customer.Name;
            _db.Audits.Add(transaction);
            _db.Customers.Add(customer);
            int rowAffected = _db.SaveChanges();
            if (rowAffected > 0)
            {
                _status = true;
            }
            return _status;
        }
    }
}