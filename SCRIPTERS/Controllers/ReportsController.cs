using System.Collections.Generic;
using System.Web.Mvc;
using Rotativa;
using SCRIPTERS.BLL;
using SCRIPTERS.Core.Models.Operation;
using SCRIPTERS.Core.Models.ViewModel;

namespace SCRIPTERS.Controllers
{
    //[Authorize(Roles = "Manager")]
    public class ReportsController : Controller
    {
        ReportBll _reportBll = new ReportBll();

        //>>>>Expense reports starts from here<<<<<

        public ActionResult Expense()
        {
            ViewBag.OutletId = _reportBll.GetOutlet();
            return View();
        }
        static List<Expense> ExpensesPdf = new List<Expense>();
        [HttpPost]
        public ActionResult Expense(ReportVm reportVm )
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

        public ActionResult PurchaseReport()
        {
            ViewBag.OutletId = _reportBll.GetOutlet();
            return View();
        }
        static List<Purchase> PurchasesPdf = new List<Purchase>();
        [HttpPost]
        public ActionResult PurchaseReport(ReportVm reportVm)
        {
            List<Purchase> Purchases = _reportBll.GetPurchasesByReportVm(reportVm);
            PurchasesPdf = Purchases;
            ViewBag.OutletId = _reportBll.GetOutlet();
            return View(Purchases);
        }
        public ActionResult PurchaseReportPdf()
        {
            if (PurchasesPdf == null)
            {
                return RedirectToAction("Error", "Home");
            }
            return View(PurchasesPdf);
        }
        public ActionResult ExportPurchasePdf()
        {
            return new ActionAsPdf("PurchaseReportPdf");
        }

        //>>>Sales reports starts from here<<<

        public ActionResult SalesReport()
        {
            ViewBag.OutletId = _reportBll.GetOutlet();
            return View();
        }
        static List<Sale> SalesPdf = new List<Sale>();
        [HttpPost]
        public ActionResult SalesReport(ReportVm reportVm)
        {
            List<Sale> Sales = _reportBll.GetSalesByReportVm(reportVm);
            SalesPdf = Sales;
            ViewBag.OutletId = _reportBll.GetOutlet();
            return View(Sales);
        }
        public ActionResult SalesReportPdf()
        {
            if (SalesPdf == null)
            {
                return RedirectToAction("Error", "Home");
            }
            return View(SalesPdf);
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
        static IncomeVm incomeVmForPdf = new IncomeVm(); 
        [HttpPost]
        public ActionResult IncomeReport(ReportVm reportVm)
        {
            IncomeVm incomeVm = _reportBll.GetIncomeVmByReportVm(reportVm);
            incomeVmForPdf = incomeVm;
            ViewBag.OutletId = _reportBll.GetOutlet();
            return View(incomeVm);
        }
        public ActionResult IncomeReportPdf()
        {
            if (incomeVmForPdf == null)
            {
                return RedirectToAction("Error", "Home");
            }
            return View(incomeVmForPdf);
        }
        public ActionResult ExportIncomePdf(IncomeVm incomeVm)
        {
            return new ActionAsPdf("IncomeReportPdf");
        }

        //>>>Stock report starts from here<<<

        public ActionResult StockReport()
        {
            ViewBag.OutletId = _reportBll.GetOutlet();
            return View();
        }
        static List<StockVm> StockReportListPdf = new List<StockVm>();
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult StockReport(ReportVm reportVm)
        {
            List<StockVm> StockReportList = _reportBll.GetStockReportList(reportVm);
            StockReportListPdf = StockReportList;
            ViewBag.OutletId = _reportBll.GetOutlet();
            return View(StockReportList);
        }
        public ActionResult StockReportPdf()
        {
            if(StockReportListPdf==null)
            {
                return RedirectToAction("Error", "Home");
            }
            return View(StockReportListPdf);
        }

        public ActionResult ExportStockReportPdf()
        {
            return new ActionAsPdf("StockReportPdf");
        }
    }
}