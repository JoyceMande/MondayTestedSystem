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
    public class BooksController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        #region ScarfoldingActionResults

        /*
         *  // GET: Books
        public ActionResult Index()
        {
            var books = db.Books.Include(b => b.ItemCategory);
            return View(books.ToList());
        }

        // GET: Books/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        // GET: Books/Create
        public ActionResult Create()
        {
            ViewBag.ItemCategoryId = new SelectList(db.BookCategories, "Id", "Name");
            return View();
        }

        // POST: Books/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,CostPrice,SalePrice,Code,Description,ItemCategoryId,Image")] Book book)
        {
            if (ModelState.IsValid)
            {
                db.Books.Add(book);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ItemCategoryId = new SelectList(db.BookCategories, "Id", "Name", book.ItemCategoryId);
            return View(book);
        }

        // GET: Books/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            ViewBag.ItemCategoryId = new SelectList(db.BookCategories, "Id", "Name", book.ItemCategoryId);
            return View(book);
        }

        // POST: Books/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,CostPrice,SalePrice,Code,Description,ItemCategoryId,Image")] Book book)
        {
            if (ModelState.IsValid)
            {
                db.Entry(book).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ItemCategoryId = new SelectList(db.BookCategories, "Id", "Name", book.ItemCategoryId);
            return View(book);
        }

        // GET: Books/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Book book = db.Books.Find(id);
            db.Books.Remove(book);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
         */

        #endregion


        Common _common = new Common();
        BookBll _itemBll = new BookBll();
        bool status = false;
        // GET: Items
        public ActionResult List()
        {
            List<Book> items = _itemBll.List();
            return View(items);
        }

        // GET: Items/Create
        public ActionResult Create()
        {
            ViewBag.autoCode = _itemBll.GenerateAutoCode();
            ViewBag.ItemCategoryId = _itemBll.GetItemCategory();
            ViewBag.CostPrice = "0";
            return View();
        }

        // POST: Items/Create
        [HttpPost]
        public ActionResult Create(Book item, HttpPostedFileBase ImageFile)
        {
            //System.Diagnostics.Debug.WriteLine("Hello World 1");
            //System.Diagnostics.Debug.WriteLine(ImageFile.InputStream.ToString());
            //System.Diagnostics.Debug.WriteLine("Hello World 1");
           
            if (ImageFile == null)
            {
                ModelState.AddModelError("Image", "Please upload an Image");
            }
            bool isValidFormate = _common.ImageValidation(ImageFile);
            if (isValidFormate == false)
            {
                ModelState.AddModelError("Image", "only png,jpg,jpeg format is allowed");
            }

            byte[] ConvertedImage = _common.ConvertImage(ImageFile);
            item.Image = ConvertedImage;
            if (ModelState.IsValid)
            {
                status = _itemBll.Create(item);
                if (status == true)
                {
                    return RedirectToAction("List", "Books");
                }
                else
                {
                    ViewBag.Message = "Expense Catagory added failed";
                }
            }
            ViewBag.ParentId = _itemBll.GetItemCategory();
            return View(item);

        }

        // GET: Items/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Error", "Home");
            }
            Book item = _itemBll.GetById(id);
            if (item == null)
            {
                return RedirectToAction("Error", "Home");
            }
            ViewBag.ItemCategoryId = _itemBll.GetItemCategory();
            return View(item);
        }

        // POST: Items/Edit/5
        [HttpPost]
        public ActionResult Edit(Book item, HttpPostedFileBase ImageFile)
        {
            if (ImageFile != null)
            {
                bool isValidFormate = _common.ImageValidation(ImageFile);
                if (isValidFormate == false)
                {
                    ModelState.AddModelError("Image", "only png,jpeg,jpg formates are allowed");
                }
                byte[] convertedImage = _common.ConvertImage(ImageFile);
                item.Image = convertedImage;
            }

            if (ModelState.IsValid)
            {
                status = _itemBll.Edit(item);
                if (status == true)
                {
                    return RedirectToAction("List", "Books");
                }
                else
                {
                    ViewBag.Message = "Item is not updated succesfully";
                }
            }

            ViewBag.ItemCategoryId = _itemBll.GetItemCategory();
            return View(item);

        }

        // GET: Items/Delete/5
        public JsonResult Delete(int id)
        {
            status = _itemBll.Delete(id);
            if (status == true)
            {
                return Json(1);
            }
            return Json(0);
        }

        // POST: Items/Delete/5
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
