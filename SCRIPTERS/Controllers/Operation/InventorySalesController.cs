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
    public class InventorySalesController : Controller
    {
        // GET: Sales
        InventorySaleBll _inventorySaleBll = new InventorySaleBll();
        InventoryBll itemBll = new InventoryBll();
        InventoryCommon common = new InventoryCommon();
        private ApplicationDbContext db = new ApplicationDbContext();
        bool status = false;
        int id;
        // GET: Items
        public ActionResult List()
        {
            List<InventorySale> InventorySales = _inventorySaleBll.List();
            return View(InventorySales);
        }
        // GET: Items/Details/5
        public ActionResult Details(int id)
        {
            InventorySale InventorySale = _inventorySaleBll.GetById(id);
            return View(InventorySale);
        }
        public ActionResult DetailsPdf(int id)
        {
            InventorySale InventorySale = _inventorySaleBll.GetById(id);
            return View(InventorySale);
        }

        public ActionResult ExportPdf(int id)
        {
            return new ActionAsPdf("DetailsPdf", new { id = id });
        }

        // GET: Items/Create
        public ActionResult Create()
        {
            ViewBag.OutletId = _inventorySaleBll.GetOutlet();
            ViewBag.EmployeeId = _inventorySaleBll.GetEmployee();
            ViewBag.ItemId = _inventorySaleBll.GetItem();
            var tax = db.BusinessRules.Select(b => b.VAT).FirstOrDefault();

            ViewBag.VAT = tax;  //TODO: SET IN CONFIGURATION CLASS
            ViewBag.InventorySaleCode = _inventorySaleBll.GenerateAutoCode();
            return View();
        }
        // POST: Items/Create
        [HttpPost]
        public ActionResult Create(InventorySale InventorySale)
        {
            //System.Diagnostics.Debug.WriteLine("Hello World");
            //System.Diagnostics.Debug.WriteLine(InventorySale.DueAmount);
            //System.Diagnostics.Debug.WriteLine(InventorySale.Total);

            //System.Diagnostics.Debug.WriteLine("Hello World");
            if (ModelState.IsValid && InventorySale.InventorySalesDetails != null && InventorySale.InventorySalesDetails.Count > 0)
            {
                InventorySale.IsDeleted = false;
                id = _inventorySaleBll.Create(InventorySale);
                if (id != null)
                {
                    return RedirectToAction("Details", "InventorySales", new { id = id });
                }
                else
                {
                    ViewBag.Message = "InventorySale added failed";
                }
            }
            ViewBag.ItemId = _inventorySaleBll.GetItem();
            ViewBag.OutletId = _inventorySaleBll.GetOutlet();
            ViewBag.EmployeeId = _inventorySaleBll.GetEmployee();
            return View(InventorySale);
        }

        // GET: Items/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Error", "Home");
            }
            InventorySale InventorySale = _inventorySaleBll.GetById(id);
            if (InventorySale == null)
            {
                return RedirectToAction("Error", "Home");
            }
            ViewBag.OutletId = _inventorySaleBll.GetOutlet();
            ViewBag.EmployeeId = _inventorySaleBll.GetEmployee();
            return View(InventorySale);
        }

        // POST: Items/Edit/5
        [HttpPost]
        public ActionResult Edit(InventorySale InventorySale)
        {


            if (ModelState.IsValid)
            {
                status = _inventorySaleBll.Edit(InventorySale);
                if (status == true)
                {
                    return RedirectToAction("List", "InventorySales");
                }
                else
                {
                    ViewBag.Message = "InventorySale is not updated succesfully";
                }
            }

            ViewBag.OutletId = _inventorySaleBll.GetOutlet();
            ViewBag.EmployeeId = _inventorySaleBll.GetEmployee();
            return View(InventorySale);

        }
        // GET: Items/Delete/5
        public JsonResult Delete(int id)
        {
            status = _inventorySaleBll.Delete(id);
            if (status == true)
            {
                return Json(1);
            }
            return Json(0);
        }
        public JsonResult GetItemSalesPrice(int id)
        {
            Inventory item = itemBll.GetById(id);
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
