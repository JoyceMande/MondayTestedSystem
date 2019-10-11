using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text.RegularExpressions;
using SCRIPTERS.Core.Models;
using SCRIPTERS.Models;

namespace SCRIPTERS.DAL
{
    public class ExpenseCategoryDal
    {
        ApplicationDbContext db = new ApplicationDbContext();
        private Audit transaction;
        bool _status = false;
        int _start = 0;
        public bool Create(ExpenseCategory expenseCategory)
        {

            db.ExpenseCategories.Add(expenseCategory);
            transaction = new Audit();
            transaction.TransactionDate = DateTime.Now.Date;
            transaction.TransactionTime = DateTime.Now;
            transaction.User = "User";
            transaction.TransactionType = "Added expense category" + " " + expenseCategory.Id;
            transaction.TransactionDetails = expenseCategory.Name;
            db.Audits.Add(transaction);
            int count = db.SaveChanges();

            if(count>0)
            {
                _status = true;
            }
            return _status;          
        }

        internal List<ExpenseCategory> List()
        {
            
           List<ExpenseCategory> listOfExpenseCategory = db.ExpenseCategories.ToList();

            return listOfExpenseCategory;
        }

        internal ExpenseCategory GetById(int? id)
        {           
            ExpenseCategory expenseCategory = db.ExpenseCategories.SingleOrDefault(m => m.Id == id);
                return expenseCategory;
        }

        internal bool Edit(ExpenseCategory expenseCategories)
        {

            db.ExpenseCategories.Attach(expenseCategories);
            db.Entry(expenseCategories).State= EntityState.Modified;
            transaction = new Audit();
            transaction.TransactionDate = DateTime.Now.Date;
            transaction.TransactionTime = DateTime.Now;
            transaction.User = "User";
            transaction.TransactionType = "Edited expense category" + " " + expenseCategories.Id;
            transaction.TransactionDetails = expenseCategories.Name;
            db.Audits.Add(transaction);

            int affectedRow = db.SaveChanges();
            if(affectedRow>0)
            {
                _status = true;
            }

            return _status;
        }

        internal object GenerateAutoCode()
        {
            string autoCode = "";
            string lastCode = db.ExpenseCategories.Max(item => item.Code);

            if (lastCode != null)
            {
                string resultString = Regex.Match(lastCode, @"\d+").Value;
                _start = Int32.Parse(resultString);

                autoCode = "EC" + (_start + 1).ToString("000");
            }
            autoCode = "EC" + (_start + 1).ToString("000");

            return autoCode;
        }

        internal bool Delete(int id)
        {
            var expenseCategorybyid = db.ExpenseCategories.FirstOrDefault(x => x.Id == id);
            if (expenseCategorybyid != null)
            {
                db.Entry(expenseCategorybyid).State = EntityState.Deleted;
                transaction = new Audit();
                transaction.TransactionDate = DateTime.Now.Date;
                transaction.TransactionTime = DateTime.Now;
                transaction.User = "User";
                transaction.TransactionType = "Deleted expense category" + " " + expenseCategorybyid.Id;
                transaction.TransactionDetails = expenseCategorybyid.Name;
                db.Audits.Add(transaction);

                int affectedRow = db.SaveChanges();

                if (affectedRow > 0)
                {
                    _status = true;
                }
            }
            return _status;
        }

        internal List<ExpenseCategory> GetParentCategories()
        {
            List<ExpenseCategory> expenseCategories = db.ExpenseCategories.ToList();

            return expenseCategories;
        }
    }
}