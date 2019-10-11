using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web.Mvc;

using SCRIPTERS.Core.Models;
using SCRIPTERS.Models;

namespace SCRIPTERS.DAL
{
    public class EmployeeDal
    {
        ApplicationDbContext db = new ApplicationDbContext();
        private Audit transaction;
        bool _status = false;
        int _start = 0;

        internal List<Employee> List()
        {
            List<Employee> employees = db.Employees.ToList();
            return employees;
        }

        internal object GenerateAutoCode()
        {
            string autoCode = "";
            string lastCode = db.Employees.Max(item => item.Code);

            if (lastCode != null)
            {
                string resultString = Regex.Match(lastCode, @"\d+").Value;
                _start = Int32.Parse(resultString);

                autoCode = "E" + (_start + 1).ToString("000");
            }
            autoCode = "E" + (_start + 1).ToString("000");
            return autoCode;
        }

        internal object GetOutlet()
        {
            var Outlet = new SelectList(db.Outlets, "Id", "Name");
            return Outlet;
        }

        internal object GetReference()
        {
            var reference = new SelectList(db.Employees, "Id", "Name");
            return reference;
        }

        internal Employee GetById(int? id)
        {
            Employee employee = db.Employees.FirstOrDefault(m => m.Id == id);
            return employee;
        }

        internal bool Edit(Employee employee)
        {
            db.Employees.Attach(employee);
            db.Entry(employee).State = EntityState.Modified;
            transaction = new Audit();
            transaction.TransactionDate = DateTime.Now.Date;
            transaction.TransactionTime = DateTime.Now;
            transaction.User = "User";
            transaction.TransactionType = "Modified employee " + " " + employee.Id;
            transaction.TransactionDetails = employee.Name;
            db.Audits.Add(transaction);
            int affectedRow = db.SaveChanges();
            if (affectedRow > 0)
            {
                _status = true;
            }
            return _status;
        }

        internal bool Delete(int id)
        {
            var employeeById = db.Employees.FirstOrDefault(m => m.Id == id);

            if (employeeById != null)
            {
                db.Entry(employeeById).State = EntityState.Deleted;
                transaction = new Audit();
                transaction.TransactionDate = DateTime.Now.Date;
                transaction.TransactionTime = DateTime.Now;
                transaction.User = "User";
                transaction.TransactionType = "Deleted employee " + " " + employeeById.Id;
                transaction.TransactionDetails = employeeById.Name;
                db.Audits.Add(transaction);
                int affectedRow = db.SaveChanges();

                if (affectedRow > 0)
                {
                    _status = true;
                }
            }
            return _status;
        }

        internal bool Create(Employee employee)
        {
            transaction = new Audit();
            transaction.TransactionDate = DateTime.Now.Date;
            transaction.TransactionTime = DateTime.Now;
            transaction.User = "User";
            transaction.TransactionType = "Added employee " + " " + employee.Id;
            transaction.TransactionDetails = employee.Name;
            db.Audits.Add(transaction);
            db.Employees.Add(employee);
            int rowAffected = db.SaveChanges();

            if (rowAffected > 0)
            {
                _status = true;
            }
            return _status;
        }
    }
}