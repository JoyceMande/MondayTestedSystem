using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text.RegularExpressions;

using SCRIPTERS.Core.Models;
using SCRIPTERS.Models;

namespace SCRIPTERS.DAL
{
    public class SupplierDal
    {
        ApplicationDbContext _db = new ApplicationDbContext();
        private Audit transaction;
        bool status = false;
        int start = 0;

        internal List<Supplier> List()
        {
            List<Supplier> Suppliers = _db.Suppliers.ToList();
            return Suppliers;
        }

        internal object GenerateAutoCode()
        {
            string autoCode = "";
            string lastCode = _db.Suppliers.Max(item => item.Code);

            if (lastCode != null)
            {
                string resultString = Regex.Match(lastCode, @"\d+").Value;
                start = Int32.Parse(resultString);

                autoCode = "S" + (start + 1).ToString("000");
            }
            autoCode = "S" + (start + 1).ToString("000");

            return autoCode;
        }


        internal Supplier GetById(int? id)
        {
            Supplier Supplier = _db.Suppliers.FirstOrDefault(m => m.Id == id);
            return Supplier;
        }

        internal bool Edit(Supplier supplier)
        {
            _db.Suppliers.Attach(supplier);
            _db.Entry(supplier).State = EntityState.Modified;
            transaction = new Audit();
            transaction.TransactionDate = DateTime.Now.Date;
            transaction.TransactionTime = DateTime.Now;
            transaction.User = "User";
            transaction.TransactionType = "Modified supplier" + " " + supplier.Id;
            transaction.TransactionDetails = supplier.Name;
            _db.Audits.Add(transaction);
            int affectedRow = _db.SaveChanges();
            if (affectedRow > 0)
            {
                status = true;
            }
            return status;
        }

        internal bool Delete(int id)
        {
            var supplierById = _db.Suppliers.FirstOrDefault(m => m.Id == id);

            if (supplierById != null)
            {
                _db.Entry(supplierById).State = EntityState.Deleted;
                transaction = new Audit();
                transaction.TransactionDate = DateTime.Now.Date;
                transaction.TransactionTime = DateTime.Now;
                transaction.User = "User";
                transaction.TransactionType = "Deleted supplier" + " " + supplierById.Id;
                transaction.TransactionDetails = supplierById.Name;
                _db.Audits.Add(transaction);
                int affectedRow = _db.SaveChanges();

                if (affectedRow > 0)
                {
                    status = true;
                }
            }
            return status;
        }

        internal bool Create(Supplier supplier)
        {
            transaction = new Audit();
            transaction.TransactionDate = DateTime.Now.Date;
            transaction.TransactionTime = DateTime.Now;
            transaction.User = "User";
            transaction.TransactionType = "Added supplier" + " " + supplier.Id;
            transaction.TransactionDetails = supplier.Name;
            _db.Audits.Add(transaction);
            _db.Suppliers.Add(supplier);
            int rowAffected = _db.SaveChanges();
            if (rowAffected > 0)
            {
                status = true;
            }
            return status;
        }
    }
}