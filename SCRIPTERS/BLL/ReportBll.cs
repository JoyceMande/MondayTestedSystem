using System.Collections.Generic;
using System.Linq;
using SCRIPTERS.Core.Models;
using SCRIPTERS.Core.Models.Operation;
using SCRIPTERS.Core.Models.ViewModel;
using SCRIPTERS.DAL;

namespace SCRIPTERS.BLL
{
    public class ReportBll
    {
        ReportDal _reportDal = new ReportDal();
        BookDal _itemDal = new BookDal();
        Common _common = new Common();
        BookCategoryDal _itemCategoryDal = new BookCategoryDal();
        BookCategory itemCategory = new BookCategory();
        internal dynamic GetOutlet()
        {
            var outlet = _reportDal.GetOutlet();
            return outlet;
        }

        internal List<Expense> GetExpensesByReportVm(ReportVm reportVm)
        {
            List<Expense> expenses = _reportDal.GetExpensesByReportVm(reportVm);
            return expenses;
        }

        internal List<Purchase> GetPurchasesByReportVm(ReportVm reportVm)
        {
            List<Purchase> purchases = _reportDal.GetPurchasesByReportVm(reportVm);
            return purchases;
        }

        internal List<Sale> GetSalesByReportVm(ReportVm reportVm)
        {
            List<Sale> sales = _reportDal.GetSalesByReportVm(reportVm);
            return sales;
        }

        internal IncomeVm GetIncomeVmByReportVm(ReportVm reportVm)
        {
            decimal totalIncome = 0;
            decimal totalSales = 0;
            decimal totalPurchase = 0;
            List<Sale> sales = _reportDal.GetSalesByReportVm(reportVm);
            List<Purchase> purchase = _reportDal.GetPurchasesByReportVm(reportVm);
            if (sales != null)
            {
                foreach (var item in sales)
                {
                    totalSales = totalSales + item.Total;
                }
            }
            if (purchase != null)
            {
                foreach (var item in purchase)
                {
                    totalPurchase = totalPurchase + item.Total;
                }
            }

            totalIncome = totalSales - totalPurchase;
            IncomeVm incomeVm = new IncomeVm();

            incomeVm.Sales = sales;
            incomeVm.Purchases = purchase;
            incomeVm.SalesTotal = totalSales;
            incomeVm.PurchasesTotal = totalPurchase;
            incomeVm.TotalIncome = totalIncome;
            return incomeVm;
        }


        //>>>Stock Report starts from here<<<

        List<StockVm> StockReportList = new List<StockVm>();
        internal List<StockVm> GetStockReportList(ReportVm reportVm)
        {

            List<Sale> sales = _reportDal.GetSalesByReportVm(reportVm);
            List<Purchase> purchases = _reportDal.GetPurchasesByReportVm(reportVm);

            if (purchases != null)
            {
                foreach (var item in purchases)
                {
                    foreach (var itemDes in item.PurchaseDetail)
                    {
                        StockVm stockVm = new StockVm();
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
            Book item = _itemDal.GetById(id);
            BookCategory itemCategory = _itemCategoryDal.GetById(item.ItemCategoryId);
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