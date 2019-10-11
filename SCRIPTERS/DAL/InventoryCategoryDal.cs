using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text.RegularExpressions;
using SCRIPTERS.Core.Models;
using SCRIPTERS.Models;

namespace SCRIPTERS.DAL
{
    public class InventoryCategoryDal
    {
        ApplicationDbContext db = new ApplicationDbContext();
        private Audit transaction;
        bool _status=false;
        int _start=0;

        internal List<InventoryCategory> List()
        {
            List<InventoryCategory> inventoryCategories = db.InventoryCategories.ToList();

            return inventoryCategories;
        }

        internal object GenerateAutoCode()
        {
            string autoCode = "";
            string lastCode = db.InventoryCategories.Max(item => item.Code);
          
            if(lastCode!=null)
            {
                string resultString = Regex.Match(lastCode, @"\d+").Value;
                 _start = Int32.Parse(resultString);

                autoCode = "IC" + (_start + 1).ToString("000");
            }
            autoCode = "IC" + (_start + 1).ToString("000");

            return autoCode;
        }

        internal InventoryCategory GetById(int? id)
        {
            InventoryCategory inventoryCategory = db.InventoryCategories.FirstOrDefault(m => m.Id == id);
            return inventoryCategory;
        }

        internal bool Edit(InventoryCategory inventoryCategory)
        {
            db.InventoryCategories.Attach(inventoryCategory);
            db.Entry(inventoryCategory).State = EntityState.Modified;
            transaction = new Audit();
            transaction.TransactionDate = DateTime.Now.Date;
            transaction.TransactionTime = DateTime.Now;
            transaction.User = "User";
            transaction.TransactionType = "Modified inventory category" + " " + inventoryCategory.Id;
            transaction.TransactionDetails = inventoryCategory.Name;
            db.Audits.Add(transaction);
            int affectedRow= db.SaveChanges();
            if(affectedRow>0)
            {
                _status = true;
            }
            return _status;
        }

        internal bool Delete(int id)
        {
           var inventoryCategoryById= db.InventoryCategories.FirstOrDefault(m => m.Id == id);

            if(inventoryCategoryById!=null)
            {
                db.Entry(inventoryCategoryById).State = EntityState.Deleted;
                transaction = new Audit();
                transaction.TransactionDate = DateTime.Now.Date;
                transaction.TransactionTime = DateTime.Now;
                transaction.User = "User";
                transaction.TransactionType = "Deleted inventory category" + " " + inventoryCategoryById.Id;
                transaction.TransactionDetails = inventoryCategoryById.Name;
                db.Audits.Add(transaction);
                int affectedRow = db.SaveChanges();

                if (affectedRow > 0)
                {
                    _status = true;
                }
            }
            return _status;
        }

        internal bool Create(InventoryCategory inventoryCategory)
        {
            db.InventoryCategories.Add(inventoryCategory);
            transaction = new Audit();
            transaction.TransactionDate = DateTime.Now.Date;
            transaction.TransactionTime = DateTime.Now;
            transaction.User = "User";
            transaction.TransactionType = "Added inventory category" + " " + inventoryCategory.Id;
            transaction.TransactionDetails = inventoryCategory.Name;
            db.Audits.Add(transaction);
            int rowAffected= db.SaveChanges();

            if(rowAffected>0)
            {
                _status = true;
            }
            return _status;
        }
    }
}