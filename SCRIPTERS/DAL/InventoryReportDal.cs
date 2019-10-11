using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using SCRIPTERS.Core.Models.Operation;
using SCRIPTERS.Core.Models.ViewModel;
using SCRIPTERS.Models;

namespace SCRIPTERS.DAL
{
    public class InventoryReportDal
    {
        ApplicationDbContext _db = new ApplicationDbContext();
        internal object GetOutlet()
        {
            var outlet = new SelectList(_db.Outlets, "Id", "Name");
            return outlet;
        }
        internal List<Expense> GetExpensesByReportVm(InventoryReportVm reportVm)
        {

            var expenses = _db.Expenses.AsQueryable();
            if (reportVm.OutletId != null)
            {
                expenses = expenses.Where(m => m.OutletId == reportVm.OutletId && m.IsDeleted == false).AsQueryable();
            }
            if (reportVm.Code != null)
            {
                expenses = expenses.Where(m => m.Id == reportVm.Code && m.IsDeleted == false).AsQueryable();
            }
            if (reportVm.FromDate != null)
            {
                expenses = expenses.Where(m => m.ExpenseDate >= reportVm.FromDate && m.IsDeleted == false).AsQueryable();
            }
            if (reportVm.ToDate != null)
            {
                expenses = expenses.Where(m => m.ExpenseDate <= reportVm.ToDate && m.IsDeleted == false).AsQueryable();
            }

            return expenses.ToList();
        }
        internal List<InventorySale> GetSalesByReportVm(InventoryReportVm reportVm)
        {
            var sales = _db.InventorySales.AsQueryable();
            if (reportVm.OutletId != null)
            {
                sales = sales.Where(m => m.OutletId == reportVm.OutletId && m.IsDeleted == false).AsQueryable();

            }
            if (reportVm.Code != null)
            {
                sales = sales.Where(m => m.Id == reportVm.Code && m.IsDeleted == false).AsQueryable();
            }
            if (reportVm.FromDate != null)
            {
                sales = sales.Where(m => m.SaleDate >= reportVm.FromDate && m.IsDeleted == false).AsQueryable();
            }
            if (reportVm.ToDate != null)
            {
                sales = sales.Where(m => m.SaleDate <= reportVm.ToDate && m.IsDeleted == false).AsQueryable();
            }
            return sales.ToList();

        }
        internal List<Order> GetOrdersByReportVm(InventoryReportVm reportVm)
        {
            var orders = _db.Orders.AsQueryable();
            if (reportVm.OutletId != null)
            {
                orders = orders.Where(m => m.OutletId == reportVm.OutletId && m.IsDeleted == false).AsQueryable();

            }
            if (reportVm.Code != null)
            {
                orders = orders.Where(m => m.Id == reportVm.Code && m.IsDeleted == false).AsQueryable();
            }
            if (reportVm.FromDate != null)
            {
                orders = orders.Where(m => m.OrderDate >= reportVm.FromDate && m.IsDeleted == false).AsQueryable();
            }
            if (reportVm.ToDate != null)
            {
                orders = orders.Where(m => m.OrderDate <= reportVm.ToDate && m.IsDeleted == false).AsQueryable();
            }
            return orders.ToList();
        }

      
    }
}