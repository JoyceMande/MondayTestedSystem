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
    [Authorize(Roles = "Manager")]
    public class InventoryCategoriesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        InventoryCategoryBll _itemCategoryBll = new InventoryCategoryBll();
        Common _common = new Common();
        bool status;


        #region ScarfoldActionResults

        // GET: BookCategories
        //public ActionResult Index()
        //{
        //    return View(db.BookCategories.ToList());
        //}


        //// GET: BookCategories/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        // POST: BookCategories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "Id,Name,Code,Description")] InventoryCategory InventoryCategory)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.BookCategories.Add(InventoryCategory);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    return View(InventoryCategory);
        //}

        //// GET: BookCategories/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    InventoryCategory InventoryCategory = db.BookCategories.Find(id);
        //    if (InventoryCategory == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(InventoryCategory);
        //}

        //// POST: BookCategories/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "Id,Name,Code,Description")] InventoryCategory InventoryCategory)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(InventoryCategory).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    return View(InventoryCategory);
        //}

        //// GET: BookCategories/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    InventoryCategory InventoryCategory = db.BookCategories.Find(id);
        //    if (InventoryCategory == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(InventoryCategory);
        //}



        //// POST: BookCategories/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    InventoryCategory InventoryCategory = db.BookCategories.Find(id);
        //    db.BookCategories.Remove(InventoryCategory);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

        //// GET: BookCategories/Details/5
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    InventoryCategory InventoryCategory = db.BookCategories.Find(id);
        //    if (InventoryCategory == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(InventoryCategory);
        //}
        #endregion


        // GET: ItemCategories
        public ActionResult List()
        {
            List<InventoryCategory> itemCategories = _itemCategoryBll.List();
            return View(itemCategories);
        }

        // GET: ItemCategories/Create
        public ActionResult Create()
        {
            ViewBag.autoCode = _itemCategoryBll.GenerateAutoCode();
            //ViewBag.ParentId = new SelectList(db.ItemCategories, "Id", "Name");
            return View();
        }

        [HttpPost]
        public ActionResult Create(InventoryCategory itemCategory)
        {

            if (ModelState.IsValid)
            {
                status = _itemCategoryBll.Create(itemCategory);
                if (status == true)
                {
                    return RedirectToAction("List");
                }
                else
                {
                    ViewBag.Message = "Inventory Catagory added failed";
                }
            }
            //ViewBag.ParentId = new SelectList(db.ExpenseCatagories, "Id", "Name");
            return View(itemCategory);
        }


        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Error", "Home");
            }
            InventoryCategory itemCategory = _itemCategoryBll.GetById(id);
            if (itemCategory == null)
            {
                return RedirectToAction("Error", "Home");
            }
            // ViewBag.parentId = new SelectList(db.ItemCategories, "Id", "Name");
            return View(itemCategory);
        }

        // POST: ItemCategories/Edit/5
        [HttpPost]
        public ActionResult Edit(InventoryCategory itemCategory)
        {
            if (ModelState.IsValid)
            {
                status = _itemCategoryBll.Edit(itemCategory);
                if (status == true)
                {
                    return RedirectToAction("List", "InventoryCategories");
                }
                else
                {
                    ViewBag.Message = "Item category is not updated successfully";
                }
            }
            //  ViewBag.ParentId = new SelectList(db.ItemCategories, "Id", "Name");
            return View(itemCategory);

        }


        // GET: ItemCategories/Delete/5
        public JsonResult Delete(int id)
        {
            status = _itemCategoryBll.Delete(id);

            if (status == true)
            {
                return Json(1);
            }

            return Json(0);
        }

        // POST: ItemCategories/Delete/5
        //[HttpPost]
        //public ActionResult Delete(int id, FormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add delete logic here

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

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
