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
    public class PurchaseDal
    {
        readonly ApplicationDbContext _db = new ApplicationDbContext();
        private Audit transaction;
        bool _status = false;
        int _start = 0;
        int _id;

        internal List<Purchase> List()
        {
            List<Purchase> sales = _db.Purchases.ToList();
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
            string lastCode = _db.Purchases.Max(item => item.PurchaseNumber);

            if (lastCode != null)
            {
                string resultString = Regex.Match(lastCode, @"\d+").Value;
                _start = Int32.Parse(resultString);

                autoCode = "Purchase" + (_start + 1).ToString("000");
            }
            autoCode = "Purchase" + (_start + 1).ToString("000");

            return autoCode;
        }
        internal object GetEmployee()
        {
            var employee = new SelectList(_db.Employees, "Id", "Name");
            return employee;
        }

        internal Purchase Details(int id)
        {
            Purchase purchase = _db.Purchases.SingleOrDefault(m => m.Id == id && m.IsDeleted == false);
            return purchase;
        }

        internal Purchase GetById(int? id)
        {
            Purchase purchase = _db.Purchases.FirstOrDefault(m => m.Id == id && m.IsDeleted == false);
            return purchase;
        }

        internal bool Edit(Purchase purchase)
        {
          
            _db.Purchases.Attach(purchase);
            _db.Entry(purchase).State = EntityState.Modified;
            transaction = new Audit();
            transaction.TransactionDate = DateTime.Now.Date;
            transaction.TransactionTime = DateTime.Now;
            transaction.User = "User";
            transaction.TransactionType = "Modified Purchase transaction" + " " + purchase.SupplierId;
            transaction.TransactionDetails = purchase.PurchaseNumber;
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
            var supplier = new SelectList(_db.Suppliers.Where(m => m.SupplierType == "Book Supplier"), "Id", "Name");
            return supplier;
        }

        internal object GetItem()
        {
            var item = new SelectList(_db.Books, "Id", "Name");
            return item;
        }

        internal bool Delete(int id)
        {
            var purchaseById = _db.Purchases.FirstOrDefault(m => m.Id == id);

            if (purchaseById != null)
            {
                _db.Entry(purchaseById).State = EntityState.Deleted;
                transaction = new Audit();
                transaction.TransactionDate = DateTime.Now.Date;
                transaction.TransactionTime = DateTime.Now;
                transaction.User = "User";
                transaction.TransactionType = "Deleted Purchase transaction" + " " + purchaseById.PurchaseNumber;
                transaction.TransactionDetails = purchaseById.PurchaseNumber;
                _db.Audits.Add(transaction);
                int affectedRow = _db.SaveChanges();

                if (affectedRow > 0)
                {
                    _status = true;
                }
            }
            return _status;
        }

        internal int Create(Purchase purchase)
        {
            transaction = new Audit();
            transaction.TransactionDate = DateTime.Now.Date;
            transaction.TransactionTime = DateTime.Now;
            transaction.User = "User";
            transaction.TransactionType = "Created Purchase transaction" + " " + purchase.PurchaseNumber;
            transaction.TransactionDetails = purchase.PurchaseNumber;
            _db.Audits.Add(transaction);
            _db.Purchases.Add(purchase);
            int rowAffected = _db.SaveChanges();
            if (rowAffected > 0)
            {
                _id = _db.Purchases.Max(m => m.Id);
            }
            return _id;
        }
    }
}