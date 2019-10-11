using System.Collections.Generic;
using System.Linq;
using SCRIPTERS.Core.Models;
using SCRIPTERS.Core.Models.Operation;
using SCRIPTERS.Core.Models.ViewModel;
using SCRIPTERS.DAL;

namespace SCRIPTERS.BLL
{
    public class InventoryReportBll
    {
        InventoryReportDal _reportDal = new InventoryReportDal();
        InventoryDal _itemDal = new InventoryDal();
        InventoryCommon _common = new InventoryCommon();
        InventoryCategoryDal _itemCategoryDal = new InventoryCategoryDal();
        InventoryCategory itemCategory = new InventoryCategory();
        internal dynamic GetOutlet()
        {
            var outlet = _reportDal.GetOutlet();
            return outlet;
        }

        internal List<Expense> GetExpensesByReportVm(InventoryReportVm reportVm)
        {
            List<Expense> expenses = _reportDal.GetExpensesByReportVm(reportVm);
            return expenses;
        }

        internal List<Order> GetOrdersByReportVm(InventoryReportVm reportVm)
        {
            List<Order> orders = _reportDal.GetOrdersByReportVm(reportVm);
            return orders;
        }

        internal List<InventorySale> GetSalesByReportVm(InventoryReportVm reportVm)
        {
            List<InventorySale> sales = _reportDal.GetSalesByReportVm(reportVm);
            return sales;
        }

        internal InventoryIncomeVm GetIncomeVmByReportVm(InventoryReportVm reportVm)
        {
            decimal totalIncome = 0;
            decimal totalSales = 0;
            decimal totalOrder = 0;
            List<InventorySale> sales = _reportDal.GetSalesByReportVm(reportVm);
            List<Order> orders = _reportDal.GetOrdersByReportVm(reportVm);
            if (sales != null)
            {
                foreach (var item in sales)
                {
                    totalSales = totalSales + item.Total;
                }
            }
            if (orders != null)
            {
                foreach (var item in orders)
                {
                    totalOrder = totalOrder + item.Total;
                }
            }

            totalIncome = totalSales - totalOrder;
            InventoryIncomeVm incomeVm = new InventoryIncomeVm();

            incomeVm.Sales = sales;
            incomeVm.Orders = orders;
            incomeVm.SalesTotal = totalSales;
            incomeVm.OrdersTotal = totalOrder;
            incomeVm.TotalIncome = totalIncome;
            return incomeVm;
        }


        //>>>Stock Report starts from here<<<

        List<InventoryStockVm> StockReportList = new List<InventoryStockVm>();
        internal List<InventoryStockVm> GetStockReportList(InventoryReportVm reportVm)
        {

            List<InventorySale> sales = _reportDal.GetSalesByReportVm(reportVm);
            List<Order> orders = _reportDal.GetOrdersByReportVm(reportVm);

            if (orders != null)
            {
                foreach (var item in orders)
                {
                    foreach (var itemDes in item.OrderDetail)
                    {
                        InventoryStockVm stockVm = new InventoryStockVm();
                        if (StockReportList.Any(m => m.ItemName == itemDes.Item.Name) == false)
                        {
                            stockVm.ItemName = itemDes.Item.Name;
                            stockVm.StockQuantity = _common.GetItemStockById(itemDes.Item.Id);
                            stockVm.CategoryFullPath = GetCategoryFullPathById(itemDes.Item.Id);
                            stockVm.Price = itemDes.Item.CostPrice;
                            StockReportList.Add(stockVm);
                        }
                    }
                }
            }
            return StockReportList.ToList();
        }

        //>>>for Category full path<<<

        internal string GetCategoryFullPathById(int id)
        {
            string categoryFullPath = "";
            Inventory item = _itemDal.GetById(id);
            InventoryCategory itemCategory = _itemCategoryDal.GetById(item.ItemCategoryId);
            categoryFullPath = itemCategory.Name;
            //if (itemCategory.ParentId != null)
            //{
            //    categoryFullPath = categoryFullPath + "," + GetCategoryParent(itemCategory.ParentId);
            //}
            return categoryFullPath;
        }

        //internal string GetCategoryParent(int? id)
        //{
        //    itemCategory = itemCategoryDal.GetById(id);
        //    string ParentName = itemCategory.Name;
        //    if (itemCategory.ParentId != null)
        //    {
        //        ParentName = ParentName + "," + (GetCategoryParent(itemCategory.ParentId));
        //    }
        //    return ParentName;
        //}
    }
}