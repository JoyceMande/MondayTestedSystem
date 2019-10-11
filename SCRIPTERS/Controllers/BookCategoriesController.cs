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
    public class BookCategoriesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private Audit transaction;
        BookCategoryBll itemCategoryBll = new BookCategoryBll();
        Common common = new Common();
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
        //public ActionResult Create([Bind(Include = "Id,Name,Code,Description")] BookCategory bookCategory)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.BookCategories.Add(bookCategory);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    return View(bookCategory);
        //}

        //// GET: BookCategories/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    BookCategory bookCategory = db.BookCategories.Find(id);
        //    if (bookCategory == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(bookCategory);
        //}

        //// POST: BookCategories/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "Id,Name,Code,Description")] BookCategory bookCategory)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(bookCategory).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    return View(bookCategory);
        //}

        //// GET: BookCategories/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    BookCategory bookCategory = db.BookCategories.Find(id);
        //    if (bookCategory == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(bookCategory);
        //}



        //// POST: BookCategories/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    BookCategory bookCategory = db.BookCategories.Find(id);
        //    db.BookCategories.Remove(bookCategory);
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
        //    BookCategory bookCategory = db.BookCategories.Find(id);
        //    if (bookCategory == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(bookCategory);
        //}
        #endregion


        // GET: ItemCategories
        public ActionResult List()
        {
            List<BookCategory> itemCategories = itemCategoryBll.List();
            return View(itemCategories);
        }

        // GET: ItemCategories/Create
        public ActionResult Create()
        {
            ViewBag.autoCode = itemCategoryBll.GenerateAutoCode();
            //ViewBag.ParentId = new SelectList(db.ItemCategories, "Id", "Name");
            return View();
        }

        [HttpPost]
        public ActionResult Create(BookCategory itemCategory)
        {

            if (ModelState.IsValid)
            {
                status = itemCategoryBll.Create(itemCategory);
                if (status == true)
                {
                    transaction = new Audit();
                    transaction.TransactionDate = DateTime.Now.Date;
                    transaction.TransactionTime = DateTime.Now;
                    transaction.User = "User";
                    transaction.TransactionType = "Updated Book Category";
                    transaction.TransactionDetails = itemCategory.Name;
                    db.Audits.Add(transaction);
                    return RedirectToAction("List");
                }
                else
                {
                    ViewBag.Message = "Book Catagory added failed";
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
            BookCategory itemCategory = itemCategoryBll.GetById(id);
            if (itemCategory == null)
            {
                return RedirectToAction("Error", "Home");
            }
            // ViewBag.parentId = new SelectList(db.ItemCategories, "Id", "Name");
            return View(itemCategory);
        }

        // POST: ItemCategories/Edit/5
        [HttpPost]
        public ActionResult Edit(BookCategory itemCategory)
        {
            if (ModelState.IsValid)
            {
               
                status = itemCategoryBll.Edit(itemCategory);
                if (status == true)
                {
                    transaction = new Audit();
                    transaction.TransactionDate = DateTime.Now.Date;
                    transaction.TransactionTime = DateTime.Now;
                    transaction.User = "User";
                    transaction.TransactionType = "Updated Book Category";
                    transaction.TransactionDetails = itemCategory.Name;
                    db.Audits.Add(transaction);
                    return RedirectToAction("List", "BookCategories");
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
            BookCategory category = new BookCategory();
            status = itemCategoryBll.Delete(id);

            if (status == true)
            {
                transaction = new Audit();
                transaction.TransactionDate = DateTime.Now.Date;
                transaction.TransactionTime = DateTime.Now;
                transaction.User = "User";
                transaction.TransactionType = "Deleted Book Category";
                transaction.TransactionDetails = category.Name;
                db.Audits.Add(transaction);
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
