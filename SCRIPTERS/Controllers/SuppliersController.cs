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
    public class SuppliersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        Common common = new Common();
        SupplierBll _supplierBll = new SupplierBll();
        bool status = false;

        #region AutoGenratedCode
        /*
         *   // GET: Suppliers
        public ActionResult Index()
        {
            return View(db.Suppliers.ToList());
        }

        // GET: Suppliers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Supplier supplier = db.Suppliers.Find(id);
            if (supplier == null)
            {
                return HttpNotFound();
            }
            return View(supplier);
        }

        // GET: Suppliers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Suppliers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Code,ContactNo,Email,Image,Address,SupplierType")] Supplier supplier)
        {
            if (ModelState.IsValid)
            {
                db.Suppliers.Add(supplier);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(supplier);
        }

        // GET: Suppliers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Supplier supplier = db.Suppliers.Find(id);
            if (supplier == null)
            {
                return HttpNotFound();
            }
            return View(supplier);
        }

        // POST: Suppliers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Code,ContactNo,Email,Image,Address,SupplierType")] Supplier supplier)
        {
            if (ModelState.IsValid)
            {
                db.Entry(supplier).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(supplier);
        }

        // GET: Suppliers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Supplier supplier = db.Suppliers.Find(id);
            if (supplier == null)
            {
                return HttpNotFound();
            }
            return View(supplier);
        }

        // POST: Suppliers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Supplier supplier = db.Suppliers.Find(id);
            db.Suppliers.Remove(supplier);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
         */


        #endregion

        // GET: Items
        public ActionResult List()
        {
            List<Supplier> suppliers = _supplierBll.List();
            return View(suppliers);
        }
      

        // GET: Items/Create
        public ActionResult Create()
        {
            ViewBag.autoCode = _supplierBll.GenerateAutoCode();
            return View();
        }

        // POST: Items/Create
        [HttpPost]
        public ActionResult Create(Supplier Supplier, HttpPostedFileBase ImageFile)
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
            Supplier.Image = ConvertedImage;
            if (ModelState.IsValid)
            {
                status = _supplierBll.Create(Supplier);
                if (status == true)
                {
                    return RedirectToAction("List", "Suppliers");
                }
                else
                {
                    ViewBag.Message = "Supplier failed to add";
                }
            }
            return View(Supplier);
        }

        // GET: Items/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Error", "Home");
            }
            Supplier Supplier = _supplierBll.GetById(id);
            if (Supplier == null)
            {
                return RedirectToAction("Error", "Home");
            }
            return View(Supplier);
        }

        // POST: Items/Edit/5
        [HttpPost]
        public ActionResult Edit(Supplier Supplier, HttpPostedFileBase ImageFile)
        {
            if (ImageFile != null)
            {
                bool isValidFormate = common.ImageValidation(ImageFile);
                if (isValidFormate == false)
                {
                    ModelState.AddModelError("Image", "only png,jpeg,jpg formates are allowed");
                }
                byte[] convertedImage = common.ConvertImage(ImageFile);
                Supplier.Image = convertedImage;
            }

            if (ModelState.IsValid)
            {
                status = _supplierBll.Edit(Supplier);
                if (status == true)
                {
                    return RedirectToAction("List", "Suppliers");
                }
                else
                {
                    ViewBag.Message = "Item is not updated succesfully";
                }
            }
            return View(Supplier);
        }

        // GET: Items/Delete/5
        public JsonResult Delete(int id)
        {
            status = _supplierBll.Delete(id);
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
