using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SCRIPTERS.BLL;

using SCRIPTERS.Core.Models;
using SCRIPTERS.Models;

namespace SCRIPTERS.Controllers
{
    [Authorize]
    public class CustomersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        Common common = new Common();
        CustomerBll _customerBll = new CustomerBll();
        bool status = false;

        #region ScarfoledActionEntities

        /*
         *   // GET: Customers
        public ActionResult Index()
        {
            return View(db.Customers.ToList());
        }

        // GET: Customers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // GET: Customers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Customers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Code,ContactNo,Email,Image,Address")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Customers.Add(customer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(customer);
        }

        // GET: Customers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Code,ContactNo,Email,Image,Address")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(customer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(customer);
        }

        // GET: Customers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Customer customer = db.Customers.Find(id);
            db.Customers.Remove(customer);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

         */

        #endregion

        // GET: Items
        public ActionResult List()
        {
            List<Customer> Parties = _customerBll.List();
            return View(Parties);
        }

        // GET: Items/Create
        public ActionResult Create()
        {
            ViewBag.autoCode = _customerBll.GenerateAutoCode();
            return View();
        }

        // POST: Items/Create
        [HttpPost]
        public ActionResult Create(Customer Customer, HttpPostedFileBase ImageFile)
        {
            if (ImageFile == null)
            {
                ModelState.AddModelError("Image", "Please upload an Image");
            }
            bool isValidFormate = common.ImageValidation(ImageFile);
            if (isValidFormate == false)
            {
                ModelState.AddModelError("Image", "only png,jpg,jpeg format is allowed");
            }

            byte[] ConvertedImage = common.ConvertImage(ImageFile);
            Customer.Image = ConvertedImage;
            if (ModelState.IsValid)
            {
                status = _customerBll.Create(Customer);
                if (status == true)
                {
                    return RedirectToAction("List", "Customers");
                }
                else
                {
                    ViewBag.Message = "Expense Catagory added failed";
                }
            }
            return View(Customer);
        }

        // GET: Items/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Error", "Home");
            }
            Customer Customer = _customerBll.GetById(id);
            if (Customer == null)
            {
                return RedirectToAction("Error", "Home");
            }
            return View(Customer);
        }

        // POST: Items/Edit/5
        [HttpPost]
        public ActionResult Edit(Customer Customer, HttpPostedFileBase ImageFile)
        {
            if (ImageFile != null)
            {
                bool isValidFormate = common.ImageValidation(ImageFile);
                if (isValidFormate == false)
                {
                    ModelState.AddModelError("Image", "only png,jpeg,jpg formates are allowed");
                }
                byte[] convertedImage = common.ConvertImage(ImageFile);
                Customer.Image = convertedImage;
            }

            if (ModelState.IsValid)
            {
                status = _customerBll.Edit(Customer);
                if (status == true)
                {
                    return RedirectToAction("List", "Customers");
                }
                else
                {
                    ViewBag.Message = "Item is not updated succesfully";
                }
            }
            return View(Customer);
        }

        // GET: Items/Delete/5
        public JsonResult Delete(int id)
        {
            status = _customerBll.Delete(id);
            if (status == true)
            {
                return Json(1);
            }
            return Json(0);
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
