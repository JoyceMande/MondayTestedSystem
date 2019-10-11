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
    public class InventorySaleDal
    {
        ApplicationDbContext _db = new ApplicationDbContext();
        private Audit transaction;
        Employee emp = new Employee();
        bool _status = false;
        int _start = 0;
        int _id;
        internal List<InventorySale> List()
        {
            List<InventorySale> inventorySales = _db.InventorySales.ToList();
            return inventorySales;
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

                autoCode = "Inventory(s) Sale" + (_start + 1).ToString("000");
            }
            autoCode = "Inventory(s) Sale" + (_start + 1).ToString("000");

            return autoCode;
        }

        internal object GetEmployee()
        {
            var employee = new SelectList(_db.Employees, "Id", "Name");
            return employee;
        }

        internal InventorySale GetById(int? id)
        {
            InventorySale inventorySale = _db.InventorySales.FirstOrDefault(m => m.Id == id && m.IsDeleted == false);
            return inventorySale;
        }

        internal bool Edit(InventorySale inventorySale)
        {
            _db.InventorySales.Attach(inventorySale);
            _db.Entry(inventorySale).State = EntityState.Modified;
            transaction = new Audit();
            transaction.TransactionDate = DateTime.Now.Date;
            transaction.TransactionTime = DateTime.Now;
            transaction.User = emp.Name;
            transaction.TransactionType = "Modified Sale transaction" + " " + inventorySale.Id;
            transaction.TransactionDetails = inventorySale.SaleNumber;
            _db.Audits.Add(transaction);
            int affectedRow = _db.SaveChanges();
            if (affectedRow > 0)
            {
                _status = true;
            }
            return _status;
        }

        internal object GetItem()
        {
            var Item = new SelectList(_db.Inventories, "Id", "Name");
            return Item;
        }

        internal bool Delete(int id)
        {
            var inventorySaleById = _db.InventorySales.FirstOrDefault(m => m.Id == id);

            if (inventorySaleById != null)
            {
                _db.Entry(inventorySaleById).State = EntityState.Deleted;

                transaction = new Audit();
                transaction.TransactionDate = DateTime.Now.Date;
                transaction.TransactionTime = DateTime.Now;
                transaction.User = "User";
                transaction.TransactionType = "Deleted Sale transaction" + " " + inventorySaleById.Id;
                transaction.TransactionDetails = inventorySaleById.SaleNumber;
                _db.Audits.Add(transaction);
                int affectedRow = _db.SaveChanges();

                if (affectedRow > 0)
                {
                    _status = true;
                }
            }
            return _status;
        }

        internal int Create(InventorySale inventorySale)
        {
            transaction = new Audit();
            transaction.TransactionDate = DateTime.Now.Date;
            transaction.TransactionTime = DateTime.Now;
            transaction.User = "User";
            transaction.TransactionType = "Created Sale transaction" + " " + inventorySale.Id;
            transaction.TransactionDetails = inventorySale.SaleNumber;
            _db.Audits.Add(transaction);
            _db.InventorySales.Add(inventorySale);
            int rowAffected = _db.SaveChanges();

            if (rowAffected > 0)
            {
                _id = _db.InventorySales.Max(m => m.Id);
            }
            return _id;
        }
    }
}