using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web.Mvc;
using SCRIPTERS.Core.Models;
using SCRIPTERS.Models;

namespace SCRIPTERS.DAL
{
    public class InventoryDal
    {
        ApplicationDbContext db = new ApplicationDbContext();
        private Audit transaction;
        bool status = false;
        int start = 0;

        internal List<Inventory> List()
        {
            List<Inventory> Inventories = db.Inventories.ToList();

            return Inventories;
        }

        internal object GenerateAutoCode()
        {
            string autoCode = "";
            string lastCode = db.Inventories.Max(Inventory => Inventory.Code);

            if (lastCode != null)
            {
                string resultString = Regex.Match(lastCode, @"\d+").Value;
                start = Int32.Parse(resultString);

                autoCode = "II" + (start + 1).ToString("000");
            }
            autoCode = "II" + (start + 1).ToString("000");

            return autoCode;
        }

        internal object GetInventoryCategory()
        {
            var InventoryCategory = new SelectList(db.InventoryCategories, "Id", "Name");
            return InventoryCategory;
        }

        internal Inventory GetById(int? id)
        {
            Inventory Inventory = db.Inventories.FirstOrDefault(m => m.Id == id);
            return Inventory;
        }

        internal bool Edit(Inventory Inventory)
        {
            db.Inventories.Attach(Inventory);
            db.Entry(Inventory).State = EntityState.Modified;
            transaction = new Audit();
            transaction.TransactionDate = DateTime.Now.Date;
            transaction.TransactionTime = DateTime.Now;
            transaction.User = "User";
            transaction.TransactionType = "Modified inventory item " + " " + Inventory.Id;
            transaction.TransactionDetails =Inventory.Name;
            db.Audits.Add(transaction);
            int AffectedRow = db.SaveChanges();
            if (AffectedRow > 0)
            {
                status = true;
            }
            return status;
        }

        internal bool Delete(int id)
        {
            var InventoryById = db.Inventories.FirstOrDefault(m => m.Id == id);

            if (InventoryById != null)
            {
                db.Entry(InventoryById).State = EntityState.Deleted;
                transaction = new Audit();
                transaction.TransactionDate = DateTime.Now.Date;
                transaction.TransactionTime = DateTime.Now;
                transaction.User = "User";
                transaction.TransactionType = "Deleted inventory item " + " " + InventoryById.Id;
                transaction.TransactionDetails = InventoryById.Name;
                db.Audits.Add(transaction);
                int affectedRow = db.SaveChanges();

                if (affectedRow > 0)
                {
                    status = true;
                }
            }
            return status;
        }

        internal bool Create(Inventory inventory)
        {
            transaction = new Audit();
            transaction.TransactionDate = DateTime.Now.Date;
            transaction.TransactionTime = DateTime.Now;
            transaction.User = "User";
            transaction.TransactionType = "Added inventory item " + " " + inventory.Id;
            transaction.TransactionDetails = inventory.Name;
            db.Audits.Add(transaction);
            db.Inventories.Add(inventory);
            int RowAffected = db.SaveChanges();

            if (RowAffected > 0)
            {
                status = true;
            }
            return status;
        }
    }
}