using System.Collections.Generic;
using SCRIPTERS.Core.Models.Operation;
using SCRIPTERS.DAL.Operation;

namespace SCRIPTERS.BLL.Operation
{
    public class ExpenseBll
    {
        ExpenseDal _expenseDal = new ExpenseDal();
        bool status;
        int id;
        internal List<Expense> List()
        {
            List<Expense> expenses = _expenseDal.List();
            return expenses;
        }
        internal int Create(Expense expense)
        {
            id = _expenseDal.Create(expense);
            return id;
        }
        internal bool Delete(int id)
        {
            status = _expenseDal.Delete(id);
            return status;
        }

        internal dynamic GetOutlet()
        {
            var Outlet = _expenseDal.GetOutlet();
            return Outlet;
        }
        internal dynamic GetEmployee()
        {
            var employee = _expenseDal.GetEmployee();
            return employee;
        }

        internal dynamic GetExpenseItem()
        {
            var item = _expenseDal.GetExpenseItem();
            return item;
        }
        internal Expense GetById(int? id)
        {
            Expense expense = _expenseDal.GetById(id);
            return expense;
        }
    }
}