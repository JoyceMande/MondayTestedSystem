using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using SCRIPTERS.Core.Models.Operation;
using SCRIPTERS.Models;

namespace SCRIPTERS.DAL.Operation
{
    public class ExpenseDal
    {
        ApplicationDbContext db = new ApplicationDbContext();
        bool status = false;
        int id;

        internal List<Expense> List()
        {
            List<Expense> Sales = db.Expenses.ToList();
            return Sales;
        }

        internal object GetOutlet()
        {
            var Outlet = new SelectList(db.Outlets, "Id", "Name");
            return Outlet;
        }

        internal object GetEmployee()
        {
            var Employee = new SelectList(db.Employees, "Id", "Name");
            return Employee;
        }

        internal Expense GetById(int? id)
        {
            Expense expense = db.Expenses.FirstOrDefault(m => m.Id == id && m.IsDeleted == false);
            return expense;
        }

        internal object GetExpenseItem()
        {
            var Item = new SelectList(db.ExpenseItems, "Id", "Name");
            return Item;
        }

        internal bool Delete(int id)
        {
            var ExpenseById = db.Expenses.FirstOrDefault(m => m.Id == id);

            if (ExpenseById != null)
            {
                db.Entry(ExpenseById).State = EntityState.Deleted;
                int affectedRow = db.SaveChanges();

                if (affectedRow > 0)
                {
                    status = true;
                }
            }
            return status;
        }

        internal int Create(Expense expense)
        {
            db.Expenses.Add(expense);
            int RowAffected = db.SaveChanges();

            if (RowAffected > 0)
            {
                id = db.Expenses.Max(m => m.Id);
            }
            return id;
        }
    }
}