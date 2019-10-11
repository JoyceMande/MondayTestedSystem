﻿using System;
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
    public class ExpenseCategoriesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        ExpenseCategoryBll _expenseCategoryBll = new ExpenseCategoryBll();
        bool status;

        #region AutoGenerated

        /*
         *   // GET: ExpenseCategories
        public ActionResult Index()
        {
            return View(db.ExpenseCategories.ToList());
        }

        // GET: ExpenseCategories/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ExpenseCategory expenseCategory = db.ExpenseCategories.Find(id);
            if (expenseCategory == null)
            {
                return HttpNotFound();
            }
            return View(expenseCategory);
        }

        // GET: ExpenseCategories/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ExpenseCategories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Code,Description")] ExpenseCategory expenseCategory)
        {
            if (ModelState.IsValid)
            {
                db.ExpenseCategories.Add(expenseCategory);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(expenseCategory);
        }

        // GET: ExpenseCategories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ExpenseCategory expenseCategory = db.ExpenseCategories.Find(id);
            if (expenseCategory == null)
            {
                return HttpNotFound();
            }
            return View(expenseCategory);
        }

        // POST: ExpenseCategories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Code,Description")] ExpenseCategory expenseCategory)
        {
            if (ModelState.IsValid)
            {
                db.Entry(expenseCategory).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(expenseCategory);
        }

        // GET: ExpenseCategories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ExpenseCategory expenseCategory = db.ExpenseCategories.Find(id);
            if (expenseCategory == null)
            {
                return HttpNotFound();
            }
            return View(expenseCategory);
        }

        // POST: ExpenseCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ExpenseCategory expenseCategory = db.ExpenseCategories.Find(id);
            db.ExpenseCategories.Remove(expenseCategory);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

         */

        #endregion

        // GET: ExpenseCategories
        public ActionResult List()
        {

            List<ExpenseCategory> listOfExpenseCategory = _expenseCategoryBll.List();
            return View(listOfExpenseCategory);

        }

        // GET: ExpenseCategories/Details/5
        //public ActionResult Details(int id)
        //{
        //    return View();
        //}

        // GET: ExpenseCategories/Create

        public ActionResult Create()
        {
            ViewBag.autoCode = _expenseCategoryBll.GenerateAutoCode();
            ViewBag.ParentId = new SelectList(db.ExpenseCategories, "Id", "Name");
            return View();
        }

        // POST: ExpenseCategories/Create
        [HttpPost]
        // [ValidateAntiForgeryToken]
        public ActionResult Create(ExpenseCategory ExpenseCategories)
        {

          

            if (ModelState.IsValid)
            {
                status = _expenseCategoryBll.Create(ExpenseCategories);
                if (status == true)
                {
                    return RedirectToAction("List");
                }
                else
                {
                    ViewBag.Message = "Expense Catagory added failed";
                }
            }
            // ViewBag.ParentId = new SelectList(db.ExpenseCategories, "Id", "Name");
            return View(ExpenseCategories);
        }

        //GET: ExpenseCategories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Error", "Home");
            }
            ExpenseCategory ExpenseCategory = _expenseCategoryBll.GetById(id);
            if (ExpenseCategory == null)
            {
                return RedirectToAction("Error", "Home");
            }
            ViewBag.parentId = new SelectList(db.ExpenseCategories, "Id", "Name");
            return View(ExpenseCategory);
        }

        //POST: ExpenseCategories/Edit/5

        [HttpPost]
        public ActionResult Edit(ExpenseCategory ExpenseCategories)
        {
            //if (ImageFile != null)
            //{
            //    bool IsValidFormat = _ExpenseCategoryBll.ImageValidation(ImageFile);
            //    if (IsValidFormat == false)
            //    {
            //        ModelState.AddModelError("Image", "Only jpg, png, jpeg formet is allowed");
            //    }
            //    byte[] convertedImage = _ExpenseCategoryBll.ConvertImage(ImageFile);
            //    ExpenseCategories.Image = convertedImage;
            //}

            if (ModelState.IsValid)
            {
                status = _expenseCategoryBll.Edit(ExpenseCategories);
                if (status == true)
                {
                    return RedirectToAction("List", "ExpenseCategories");
                }
                else
                {
                    ViewBag.Message = "Expense Catagory update failed";
                }
            }
            //ViewBag.parentId = new SelectList(db.ExpenseCategories, "Id", "Name");
            return View(ExpenseCategories);
        }

        //GET: ExpenseCategories/Delete/5
        public JsonResult Delete(int id)
        {
            status = _expenseCategoryBll.Delete(id);

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
