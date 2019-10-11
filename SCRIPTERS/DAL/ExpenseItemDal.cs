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
    public class ExpenseItemDal
    {
        ApplicationDbContext db = new ApplicationDbContext();
         private Audit transaction;
        bool status = false;
        int start = 0;
        internal List<ExpenseItem> List()
        {
            List<ExpenseItem> ExpenseItems = db.ExpenseItems.ToList();
            return ExpenseItems;
        }

        internal object GenerateAutoCode()
        {
            string autoCode = "";
            string lastCode = db.ExpenseItems.Max(item => item.Code);

            if (lastCode != null)
            {
                string resultString = Regex.Match(lastCode, @"\d+").Value;
                start = Int32.Parse(resultString);

                autoCode = "EI" + (start + 1).ToString("000");
            }
            autoCode = "EI" + (start + 1).ToString("000");

            return autoCode;
        }

        internal object GetExpenseItemCategory()
        {
            var expenseCategory = new SelectList(db.ExpenseCategories, "Id", "Name");
            return expenseCategory;
        }

        internal ExpenseItem GetById(int? id)
        {
            ExpenseItem expenseItem = db.ExpenseItems.FirstOrDefault(m => m.Id == id);
            return expenseItem;
        }

        internal bool Edit(ExpenseItem expenseItem)
        {
            db.ExpenseItems.Attach(expenseItem);
            db.Entry(expenseItem).State = EntityState.Modified;
            transaction = new Audit();
            transaction.TransactionDate = DateTime.Now.Date;
            transaction.TransactionTime = DateTime.Now;
            transaction.User = "User";
            transaction.TransactionType = "Modified expense item" + " " + expenseItem.Id;
            transaction.TransactionDetails = expenseItem.Name;
            db.Audits.Add(transaction);
            int affectedRow = db.SaveChanges();
            if (affectedRow > 0)
            {
                status = true;
            }
            return status;
        }

        internal bool Delete(int id)
        {
            var expenseItemById = db.ExpenseItems.FirstOrDefault(m => m.Id == id);

            if (expenseItemById != null)
            {
                db.Entry(expenseItemById).State = EntityState.Deleted;
                transaction = new Audit();
                transaction.TransactionDate = DateTime.Now.Date;
                transaction.TransactionTime = DateTime.Now;
                transaction.User = "User";
                transaction.TransactionType = "Deleted expense item" + " " + expenseItemById.Id;
                transaction.TransactionDetails = expenseItemById.Name;
                db.Audits.Add(transaction);
                int affectedRow = db.SaveChanges();

                if (affectedRow > 0)
                {
                    status = true;
                }
            }
            return status;
        }

        internal bool Create(ExpenseItem expenseItem)
        {
            db.ExpenseItems.Add(expenseItem);
            transaction = new Audit();
            transaction.TransactionDate = DateTime.Now.Date;
            transaction.TransactionTime = DateTime.Now;
            transaction.User = "User";
            transaction.TransactionType = "Added expense item" + " " + expenseItem.Id;
            transaction.TransactionDetails = expenseItem.Name;
            db.Audits.Add(transaction);
            int rowAffected = db.SaveChanges();

            if (rowAffected > 0)
            {
                status = true;
            }
            return status;
        }
    }
}