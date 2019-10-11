using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Rotativa;
using SCRIPTERS.BLL;
using SCRIPTERS.BLL.Operation;
using SCRIPTERS.Core.Models;
using SCRIPTERS.Core.Models.Operation;
using SCRIPTERS.Models;

namespace SCRIPTERS.Controllers.Operation
{
    //[Authorize]
    public class SalesController : Controller
    {
        // GET: Sales
        SaleBll _saleBll = new SaleBll();
        BookBll itemBll = new BookBll();
        Common common = new Common();
        private ApplicationDbContext db = new ApplicationDbContext();
        bool status = false;
        int id;
        // GET: Items
        public ActionResult List()
        {
            List<Sale> Sales = _saleBll.List();
            return View(Sales);
        }
        // GET: Items/Details/5
        public ActionResult Details(int id)
        {
            Sale sale = _saleBll.GetById(id);
            return View(sale);
        }
        public ActionResult DetailsPdf(int id)
        {
            Sale sale = _saleBll.GetById(id);
            return View(sale);
        }

        public ActionResult ExportPdf(int id)
        {
            return new ActionAsPdf("DetailsPdf", new { id = id });
        }

        // GET: Items/Create
        public ActionResult Create()
        {
            ViewBag.OutletId = _saleBll.GetOutlet();
            ViewBag.EmployeeId = _saleBll.GetEmployee();
            ViewBag.ItemId = _saleBll.GetItem();
            var tax = db.BusinessRules.Select(b => b.VAT).FirstOrDefault();

            ViewBag.VAT = tax;  //TODO: SET IN CONFIGURATION CLASS
            ViewBag.SaleCode = _saleBll.GenerateAutoCode();
            return View();
        }
        // POST: Items/Create
        [HttpPost]
        public ActionResult Create(Sale sale)
        {
            //System.Diagnostics.Debug.WriteLine("Hello World");
            //System.Diagnostics.Debug.WriteLine(sale.DueAmount);
            //System.Diagnostics.Debug.WriteLine(sale.Total);
            
            //System.Diagnostics.Debug.WriteLine("Hello World");
            if (ModelState.IsValid && sale.SalesDetails!=null && sale.SalesDetails.Count>0)
            {
                sale.IsDeleted = false;
                id = _saleBll.Create(sale);
                if (id !=null)
                {
                    return RedirectToAction("Details", "Sales", new { id=id});
                }
                else
                {
                    ViewBag.Message = "Sale added failed";
                }
            }
            ViewBag.ItemId = _saleBll.GetItem();
            ViewBag.OutletId = _saleBll.GetOutlet();
            ViewBag.EmployeeId = _saleBll.GetEmployee();
            return View(sale);
        }

        // GET: Items/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Error", "Home");
            }
            Sale sale = _saleBll.GetById(id);
            if (sale == null)
            {
                return RedirectToAction("Error", "Home");
            }
            ViewBag.OutletId = _saleBll.GetOutlet();
            ViewBag.EmployeeId = _saleBll.GetEmployee();
            return View(sale);
        }

        // POST: Items/Edit/5
        [HttpPost]
        public ActionResult Edit(Sale sale)
        {
         

            if (ModelState.IsValid)
            {
                status = _saleBll.Edit(sale);
                if (status == true)
                {
                    return RedirectToAction("List", "Sales");
                }
                else
                {
                    ViewBag.Message = "Sale is not updated succesfully";
                }
            }

            ViewBag.OutletId = _saleBll.GetOutlet();
            ViewBag.EmployeeId = _saleBll.GetEmployee();
            return View(sale);

        }
        // GET: Items/Delete/5
        public JsonResult Delete(int id)
        {
            status = _saleBll.Delete(id);
            if (status == true)
            {
                return Json(1);
            }
            return Json(0);
        }
        public JsonResult GetItemSalesPrice(int id)
        {
            Book item = itemBll.GetById(id);
            var itemPrice = item.SalePrice;
            return Json(itemPrice);
        }
        public JsonResult GetItemStock(int id)
        {
            var ItemStock = common.GetItemStockById(id);
            return Json(ItemStock);
        }

    }
}
