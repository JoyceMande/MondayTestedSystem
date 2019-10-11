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
    public class BookDal
    {
        ApplicationDbContext db = new ApplicationDbContext();
        private Audit transaction;
        bool _status = false;
        int _start = 0;

        internal List<Book> List()
        {
            List<Book> books = db.Books.ToList();

            return books;
        }

        internal object GenerateAutoCode()
        {
            string autoCode = "";
            string lastCode = db.Books.Max(book => book.Code);

            if (lastCode != null)
            {
                string resultString = Regex.Match(lastCode, @"\d+").Value;
                _start = Int32.Parse(resultString);

                autoCode = "BI" + (_start + 1).ToString("000");
            }
            autoCode = "BI" + (_start + 1).ToString("000");

            return autoCode;
        }

        internal object GetBookCategory()
        {
            var bookCategory = new SelectList(db.BookCategories, "Id", "Name");
            return bookCategory;
        }

        internal Book GetById(int? id)
        {
            Book book = db.Books.FirstOrDefault(m => m.Id == id);
            return book;
        }
        
        internal bool Edit(Book book)
        {
           
            db.Books.Attach(book);
            db.Entry(book).State = EntityState.Modified;
            transaction = new Audit();
            transaction.TransactionDate = DateTime.Now.Date;
            transaction.TransactionTime = DateTime.Now;
            transaction.User = "User";
            transaction.TransactionType = "Modified Book";
            transaction.TransactionDetails = book.Name;
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
            var bookById = db.Books.FirstOrDefault(m => m.Id == id);

            if (bookById != null)
            {
                db.Entry(bookById).State = EntityState.Deleted;
                transaction = new Audit();
                transaction.TransactionDate = DateTime.Now.Date;
                transaction.TransactionTime = DateTime.Now;
                transaction.User = "User";
                transaction.TransactionType = "Deleted Book";
                transaction.TransactionDetails = bookById.Name;
                db.Audits.Add(transaction);
                int affectedRow = db.SaveChanges();

                if (affectedRow > 0)
                {
                    _status = true;
                }
            }
            return _status;
        }

        internal bool Create(Book book)
        {
            transaction = new Audit();
            transaction.TransactionDate = DateTime.Now.Date;
            transaction.TransactionTime = DateTime.Now;
            transaction.User = "User";
            transaction.TransactionType = "Added Book";
            transaction.TransactionDetails = book.Name;
            db.Audits.Add(transaction);
            db.Books.Add(book);
            int rowAffected = db.SaveChanges();

            if (rowAffected > 0)
            {
                _status = true;
            }
            return _status;
        }
    }
}