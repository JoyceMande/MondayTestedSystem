using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text.RegularExpressions;

using SCRIPTERS.Core.Models;
using SCRIPTERS.Models;

namespace SCRIPTERS.DAL
{
    public class BookCategoryDal
    {
        ApplicationDbContext db = new ApplicationDbContext();

       
        private Audit transaction;
        bool _status=false;
        int _start=0;

        internal List<BookCategory> List()
        {
            List<BookCategory> bookCategories = db.BookCategories.ToList();

            return bookCategories;
        }

        internal object GenerateAutoCode()
        {
            string autoCode = "";
            string lastCode = db.BookCategories.Max(item => item.Code);
          
            if(lastCode!=null)
            {
                string resultString = Regex.Match(lastCode, @"\d+").Value;
                 _start = Int32.Parse(resultString);

                autoCode = "BC" + (_start + 1).ToString("000");
            }
            autoCode = "BC" + (_start + 1).ToString("000");

            return autoCode;
        }

        internal BookCategory GetById(int? id)
        {
            BookCategory bookCategory = db.BookCategories.FirstOrDefault(m => m.Id == id);
            return bookCategory;
        }

        internal bool Edit(BookCategory bookCategory)
        {
            ApplicationUser applicationUser = new ApplicationUser();
            db.BookCategories.Attach(bookCategory);
            db.Entry(bookCategory).State = EntityState.Modified;
            transaction = new Audit();
            transaction.TransactionDate = DateTime.Now.Date;
            transaction.TransactionTime = DateTime.Now;
            transaction.User = applicationUser.UserName;
            transaction.TransactionType = "Updated Book Category";
            transaction.TransactionDetails = bookCategory.Name;
            db.Audits.Add(transaction);
            int affectedRow= db.SaveChanges();
            if(affectedRow>0)
            {
                _status = true;
            }
            return _status;
        }

        internal bool Delete(int id)
        {
           var bookCategoryById= db.BookCategories.FirstOrDefault(m => m.Id == id);

            if(bookCategoryById!=null)
            {
                db.Entry(bookCategoryById).State = EntityState.Deleted;
                transaction = new Audit();
                transaction.TransactionDate = DateTime.Now.Date;
                transaction.TransactionTime = DateTime.Now;
                transaction.User = "User";
                transaction.TransactionType = "Deleted Book Category";
                transaction.TransactionDetails = bookCategoryById.Name;
                db.Audits.Add(transaction);
                int affectedRow = db.SaveChanges();

                if (affectedRow > 0)
                {
                    _status = true;
                }
            }
            return _status;
        }

        internal bool Create(BookCategory bookCategory)
        {
            transaction = new Audit();
            transaction.TransactionDate = DateTime.Now.Date;
            transaction.TransactionTime = DateTime.Now;
            transaction.User = "User";
            transaction.TransactionType = "Added Book Cate";
            transaction.TransactionDetails = bookCategory.Name;
            db.Audits.Add(transaction);
            db.BookCategories.Add(bookCategory);
           int rowAffected= db.SaveChanges();

            if(rowAffected>0)
            {
                _status = true;
            }
            return _status;
        }
    }
}