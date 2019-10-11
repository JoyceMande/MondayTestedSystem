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
    public class EmployeesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        Common common = new Common();
        EmployeeBll employeeBll = new EmployeeBll();
        bool status = false;

        #region AllGeneterated
        /*
         * 
        // GET: Employees
        public ActionResult Index()
        {
            var employees = db.Employees.Include(e => e.Outlet).Include(e => e.Reference);
            return View(employees.ToList());
        }

        // GET: Employees/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // GET: Employees/Create
        public ActionResult Create()
        {
            ViewBag.OutletId = new SelectList(db.Outlets, "Id", "Name");
            ViewBag.ReferenceId = new SelectList(db.Employees, "Id", "Name");
            return View();
        }

        // POST: Employees/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Code,OutletId,JoiningDate,Image,ContactNo,Email,ReferenceId,EmerContactNo,NationalId,FathersName,MothersName,PresentAddress,PermanentAddress")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                db.Employees.Add(employee);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.OutletId = new SelectList(db.Outlets, "Id", "Name", employee.OutletId);
            ViewBag.ReferenceId = new SelectList(db.Employees, "Id", "Name", employee.ReferenceId);
            return View(employee);
        }

        // GET: Employees/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            ViewBag.OutletId = new SelectList(db.Outlets, "Id", "Name", employee.OutletId);
            ViewBag.ReferenceId = new SelectList(db.Employees, "Id", "Name", employee.ReferenceId);
            return View(employee);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Code,OutletId,JoiningDate,Image,ContactNo,Email,ReferenceId,EmerContactNo,NationalId,FathersName,MothersName,PresentAddress,PermanentAddress")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                db.Entry(employee).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.OutletId = new SelectList(db.Outlets, "Id", "Name", employee.OutletId);
            ViewBag.ReferenceId = new SelectList(db.Employees, "Id", "Name", employee.ReferenceId);
            return View(employee);
        }

        // GET: Employees/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Employee employee = db.Employees.Find(id);
            db.Employees.Remove(employee);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
         */


        #endregion


        // GET: Items
        public ActionResult List()
        {
            List<Employee> Employees = employeeBll.List();
            return View(Employees);
        }

        //GET: Items/Details/5
        public ActionResult Details(int id)
        {
            Employee employee = employeeBll.GetById(id);
            return View(employee);
        }

        // GET: Items/Create
        public ActionResult Create()
        {
            ViewBag.autoCode = employeeBll.GenerateAutoCode();
            ViewBag.OutletId = employeeBll.GetOutlet();
            ViewBag.ReferenceId = employeeBll.GetReference();
            return View();
        }

        // POST: Items/Create
        [HttpPost]
        public ActionResult Create(Employee employee, HttpPostedFileBase ImageFile)
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
            employee.Image = ConvertedImage;
            if (ModelState.IsValid)
            {
                status = employeeBll.Create(employee);
                if (status == true)
                {
                    return RedirectToAction("List", "Employees");
                }
                else
                {
                    ViewBag.Message = "Expense Catagory added failed";
                }
            }
            ViewBag.OutletId = employeeBll.GetOutlet();
            ViewBag.ReferenceId = employeeBll.GetReference();
            return View(employee);
        }

        // GET: Items/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Error", "Home");
            }
            Employee employee = employeeBll.GetById(id);
            if (employee == null)
            {
                return RedirectToAction("Error", "Home");
            }
            ViewBag.OutletId = employeeBll.GetOutlet();
            ViewBag.ReferenceId = employeeBll.GetReference();
            return View(employee);
        }

        // POST: Items/Edit/5
        [HttpPost]
        public ActionResult Edit(Employee employee, HttpPostedFileBase ImageFile)
        {
            if (ImageFile != null)
            {
                bool isValidFormate = common.ImageValidation(ImageFile);
                if (isValidFormate == false)
                {
                    ModelState.AddModelError("Image", "only png,jpeg,jpg formates are allowed");
                }
                byte[] convertedImage = common.ConvertImage(ImageFile);
                employee.Image = convertedImage;
            }

            if (ModelState.IsValid)
            {
                status = employeeBll.Edit(employee);
                if (status == true)
                {
                    return RedirectToAction("List", "Employees");
                }
                else
                {
                    ViewBag.Message = "Item is not updated succesfully";
                }
            }
            ViewBag.OutletId = employeeBll.GetOutlet();
            ViewBag.ReferenceId = employeeBll.GetReference();
            return View(employee);
        }

        // GET: Items/Delete/5
        public JsonResult Delete(int id)
        {
            status = employeeBll.Delete(id);
            if (status == true)
            {
                return Json(1);
            }
            return Json(0);
        }


    }
}
