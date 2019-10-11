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
    public class OutletDal
    {

        ApplicationDbContext db = new ApplicationDbContext();
        private Audit transaction;
        bool status = false;
        int start = 0;

        internal List<Outlet> List()
        {
            List<Outlet> Outlets = db.Outlets.ToList();
            return Outlets;
        }

        internal object GenerateAutoCode()
        {
            string autoCode = "";
            string lastCode = db.Outlets.Max(item => item.Code);

            if (lastCode != null)
            {
                string resultString = Regex.Match(lastCode, @"\d+").Value;
                start = Int32.Parse(resultString);

                autoCode = "I" + (start + 1).ToString("000");
            }
            autoCode = "I" + (start + 1).ToString("000");

            return autoCode;
        }


        internal Outlet GetById(int? id)
        {
            Outlet outlet = db.Outlets.FirstOrDefault(m => m.Id == id);
            return outlet;
        }

        internal bool Edit(Outlet outlet)
        {
            db.Outlets.Attach(outlet);
            db.Entry(outlet).State = EntityState.Modified;
            transaction = new Audit();
            transaction.TransactionDate = DateTime.Now.Date;
            transaction.TransactionTime = DateTime.Now;
            transaction.User = "User";
            transaction.TransactionType = "Modified outlet" + " " + outlet.Id;
            transaction.TransactionDetails = outlet.Name;
            db.Audits.Add(transaction);
            int AffectedRow = db.SaveChanges();
            if (AffectedRow > 0)
            {
                status = true;
            }
            return status;
        }

        internal bool Delete(int id)
        {
            var OutletById = db.Outlets.FirstOrDefault(m => m.Id == id);

            if (OutletById != null)
            {
                db.Entry(OutletById).State = EntityState.Deleted;
                transaction = new Audit();
                transaction.TransactionDate = DateTime.Now.Date;
                transaction.TransactionTime = DateTime.Now;
                transaction.User = "User";
                transaction.TransactionType = "Deleted outlet" + " " + OutletById.Id;
                transaction.TransactionDetails = OutletById.Name;
                db.Audits.Add(transaction);
                int affectedRow = db.SaveChanges();

                if (affectedRow > 0)
                {
                    status = true;
                }
            }
            return status;
        }

        internal bool Create(Outlet outlet)
        {
            transaction = new Audit();
            transaction.TransactionDate = DateTime.Now.Date;
            transaction.TransactionTime = DateTime.Now;
            transaction.User = "User";
            transaction.TransactionType = "Added outlet" + " " + outlet.Id;
            transaction.TransactionDetails = outlet.Name;
            db.Audits.Add(transaction);
            db.Outlets.Add(outlet);
            int RowAffected = db.SaveChanges();

            if (RowAffected > 0)
            {
                status = true;
            }
            return status;
        }
    }
}