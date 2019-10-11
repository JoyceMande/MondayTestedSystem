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
    public class SaleDal
    {
        ApplicationDbContext _db = new ApplicationDbContext();
        private Audit transaction;
        bool _status = false;
        int _start = 0;
        int _id;
        internal List<Sale> List()
        {
            List<Sale> sales = _db.Sales.ToList();
            return sales;
        }



        internal object GetOutlet()
        {
            var Outlet = new SelectList(_db.Outlets, "Id", "Name");
            return Outlet;
        }

        internal object GenerateAutoCode()
        {
            string autoCode = "";
            string lastCode = _db.Purchases.Max(item => item.PurchaseNumber);

            if (lastCode != null)
            {
                string resultString = Regex.Match(lastCode, @"\d+").Value;
                _start = Int32.Parse(resultString);

                autoCode = "Book(s) Sale" + (_start + 1).ToString("000");
            }
            autoCode = "Book(s) Sale" + (_start + 1).ToString("000");

            return autoCode;
        }

        internal object GetEmployee()
        {
            var Employee = new SelectList(_db.Employees, "Id", "Name");
            return Employee;
        }

        internal Sale GetById(int? id)
        {
            Sale sale = _db.Sales.FirstOrDefault(m => m.Id == id && m.IsDeleted == false);
            return sale;
        }

        internal bool Edit(Sale sale)
        {
            _db.Sales.Attach(sale);
            _db.Entry(sale).State = EntityState.Modified;
            transaction = new Audit();
            transaction.TransactionDate = DateTime.Now.Date;
            transaction.TransactionTime = DateTime.Now;
            transaction.User = "User";
            transaction.TransactionType = "Modified Book Sale transaction" + " " + sale.Outlet;
            transaction.TransactionDetails = sale.SaleNumber;
            _db.Audits.Add(transaction);
            int AffectedRow = _db.SaveChanges();
            if (AffectedRow > 0)
            {
                _status = true;
            }
            return _status;
        }

        internal object GetItem()
        {
            var Item = new SelectList(_db.Books, "Id", "Name");
            return Item;
        }

        internal bool Delete(int id)
        {
            var SaleById = _db.Sales.FirstOrDefault(m => m.Id == id);

            if (SaleById != null)
            {
                _db.Entry(SaleById).State = EntityState.Deleted;
                transaction = new Audit();
                transaction.TransactionDate = DateTime.Now.Date;
                transaction.TransactionTime = DateTime.Now;
                transaction.User = "User";
                transaction.TransactionType = "Deleted Book Sale transaction" + " " + SaleById.Outlet;
                transaction.TransactionDetails = SaleById.SaleNumber;
                _db.Audits.Add(transaction);
                int affectedRow = _db.SaveChanges();

                if (affectedRow > 0)
                {
                    _status = true;
                }
            }
            return _status;
        }

        internal int Create(Sale sale)
        {
            transaction = new Audit();
            transaction.TransactionDate = DateTime.Now.Date;
            transaction.TransactionTime = DateTime.Now;
            transaction.User = "User";
            transaction.TransactionType = "Created Book Sale transaction" + " " + sale.Outlet;
            transaction.TransactionDetails = sale.SaleNumber;
            _db.Audits.Add(transaction);
            _db.Sales.Add(sale);
            int rowAffected = _db.SaveChanges();

            if (rowAffected > 0)
            {
                _id = _db.Sales.Max(m => m.Id);
            }
            return _id;
        }
    }
}