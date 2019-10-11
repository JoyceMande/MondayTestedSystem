using System.Collections.Generic;
using System.Web.Mvc;
using Rotativa;
using SCRIPTERS.BLL.Operation;
using SCRIPTERS.Core.Models.Operation;

namespace SCRIPTERS.Controllers.Operation
{
    //[Authorize]
    public class OrdersController : Controller

    {

        OrderBll _orderBll = new OrderBll();
        bool status = false;
        int id;
        // GET: Orders
        public ActionResult List()
        {
            List<Order> orders = _orderBll.List();
            return View(orders);
        }
        //GET: Details/5
        public ActionResult Details(int id)
        {
            Order order = _orderBll.Details(id);
            return View(order);
        }
        public ActionResult DetailsPdf(int id)
        {
            Order order = _orderBll.Details(id);
            return View(order);
        }

        //GET: Create

        public ActionResult Create()
        {
            ViewBag.OutletId = _orderBll.GetOutlet();
            ViewBag.EmployeeId = _orderBll.GetEmployee(); //TO DO: change to get the currently logged in Employee
            ViewBag.ItemId = _orderBll.GetItem();
            ViewBag.Supplier = _orderBll.Supplier();
            ViewBag.OrderCode = _orderBll.GenerateAutoCode();
            return View();
        }

        // POST: Create
        [HttpPost]
        public ActionResult Create(Order Order)
        {
            Order.IsDeleted = false;
            if (ModelState.IsValid && Order.OrderDetail != null && Order.OrderDetail.Count > 0)
            {
                id = _orderBll.Create(Order);
                if (id != null)
                {
                    System.Diagnostics.Debug.WriteLine(Order.OrderDetail);
                    return RedirectToAction("Details", "Orders", new { id = id });
                }
                else
                {
                    ViewBag.Message = "Order added failed";
                }
            }
            ViewBag.ItemId = _orderBll.GetItem();
            ViewBag.OutletId = _orderBll.GetOutlet();
            ViewBag.EmployeeId = _orderBll.GetEmployee();
            ViewBag.Supplier = _orderBll.Supplier();
            return View(Order);
        }

        public ActionResult ExportPdf(int id)
        {
            return new ActionAsPdf("DetailsPdf", new { id = id });
        }

      

        public JsonResult Delete(int id)
        {
            status = _orderBll.Delete(id);
            if (status == true)
            {
                return Json(1);
            }
            return Json(0);
        }
    }
}