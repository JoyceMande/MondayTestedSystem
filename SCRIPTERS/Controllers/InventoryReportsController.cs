using System.Collections.Generic;
using System.Web.Mvc;
using Rotativa;
using SCRIPTERS.BLL;
using SCRIPTERS.Core.Models.Operation;
using SCRIPTERS.Core.Models.ViewModel;

namespace SCRIPTERS.Controllers
{
    //[Authorize(Roles = "Manager")]
    public class InventoryReportsController : Controller
    {
        InventoryReportBll _reportBll = new InventoryReportBll();

        //>>>>Expense reports starts from here<<<<<

        public ActionResult Expense()
        {
            ViewBag.OutletId = _reportBll.GetOutlet();
            return View();
        }
        static List<Expense> ExpensesPdf = new List<Expense>();
        [HttpPost]
        public ActionResult Expense(InventoryReportVm reportVm )
        {
            List<Expense> Expenses = _reportBll.GetExpensesByReportVm(reportVm);
            ExpensesPdf = Expenses;
            ViewBag.OutletId = _reportBll.GetOutlet();
            return View(Expenses);
        }
        public ActionResult ExpenseReportPdf()
        {
            if(ExpensesPdf==null)
            {
                return RedirectToAction("Error", "Home");
            }
            return View(ExpensesPdf);
        }
        public ActionResult ExportPdf()
        {
            return new ActionAsPdf("ExpenseReportPdf");
        }

        //>>>>Purchaase Reports starts from here<<<<<

        public ActionResult OrderReport()
        {
            ViewBag.OutletId = _reportBll.GetOutlet();
            return View();
        }
        static List<Order> OrdersPdf = new List<Order>();
        [HttpPost]
        public ActionResult OrderReport(InventoryReportVm reportVm)
        {
            List<Order> orders = _reportBll.GetOrdersByReportVm(reportVm);
            OrdersPdf = orders;
            ViewBag.OutletId = _reportBll.GetOutlet();
            return View(orders);
        }
        public ActionResult OrderReportPdf()
        {
            if (OrdersPdf == null)
            {
                return RedirectToAction("Error", "Home");
            }
            return View(OrdersPdf);
        }
        public ActionResult ExportOrderPdf()
        {
            return new ActionAsPdf("OrderReportPdf");
        }

        //>>>InventorySales reports starts from here<<<

        public ActionResult SalesReport()
        {
            ViewBag.OutletId = _reportBll.GetOutlet();
            return View();
        }
        static List<InventorySale> _salesPdf = new List<InventorySale>();
        [HttpPost]
        public ActionResult SalesReport(InventoryReportVm reportVm)
        {
            List<InventorySale> inventorySales = _reportBll.GetSalesByReportVm(reportVm);
            _salesPdf = inventorySales;
            ViewBag.OutletId = _reportBll.GetOutlet();
            return View(inventorySales);
        }
        public ActionResult SalesReportPdf()
        {
            if (_salesPdf == null)
            {
                return RedirectToAction("Error", "Home");
            }
            return View(_salesPdf);
        }
        public ActionResult ExportSalesPdf()
        {
            return new ActionAsPdf("SalesReportPdf");
        }

        //>>>Income reports starts from here<<<

        public ActionResult IncomeReport()
        {
            ViewBag.OutletId = _reportBll.GetOutlet();
            return View();
        }
        static InventoryIncomeVm _incomeVmForPdf = new InventoryIncomeVm(); 
        [HttpPost]
        public ActionResult IncomeReport(InventoryReportVm reportVm)
        {
            InventoryIncomeVm incomeVm = _reportBll.GetIncomeVmByReportVm(reportVm);
            _incomeVmForPdf = incomeVm;
            ViewBag.OutletId = _reportBll.GetOutlet();
            return View(incomeVm);
        }
        public ActionResult IncomeReportPdf()
        {
            if (_incomeVmForPdf == null)
            {
                return RedirectToAction("Error", "Home");
            }
            return View(_incomeVmForPdf);
        }
        public ActionResult ExportIncomePdf(InventoryIncomeVm incomeVm)
        {
            return new ActionAsPdf("IncomeReportPdf");
        }

        //>>>Stock report starts from here<<<

        public ActionResult StockReport()
        {
            ViewBag.OutletId = _reportBll.GetOutlet();
            return View();
        }
        static List<InventoryStockVm> _stockReportListPdf = new List<InventoryStockVm>();
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult StockReport(InventoryReportVm reportVm)
        {
            List<InventoryStockVm> stockReportList = _reportBll.GetStockReportList(reportVm);
            _stockReportListPdf = stockReportList;
            ViewBag.OutletId = _reportBll.GetOutlet();
            return View(stockReportList);
        }
        public ActionResult StockReportPdf()
        {
            if(_stockReportListPdf==null)
            {
                return RedirectToAction("Error", "Home");
            }
            return View(_stockReportListPdf);
        }

        public ActionResult ExportStockReportPdf()
        {
            return new ActionAsPdf("StockReportPdf");
        }
    }
}