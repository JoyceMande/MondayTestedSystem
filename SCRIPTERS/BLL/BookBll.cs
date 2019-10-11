using System.Collections.Generic;
using SCRIPTERS.Core.Models;
using SCRIPTERS.DAL;

namespace SCRIPTERS.BLL
{
    public class BookBll
    {
        BookDal _bookDal = new BookDal();
        bool status;

        internal List<Book> List()
        {
            List<Book> books = _bookDal.List();
            return books;
        }

        internal dynamic GenerateAutoCode()
        {
            var autoCode = _bookDal.GenerateAutoCode();
            return autoCode;
        }

        internal bool Create(Book book)
        {
            status = _bookDal.Create(book);
            return status;
        }

        internal bool Delete(int id)
        {
            status = _bookDal.Delete(id);
            return status;
        }

        internal dynamic GetItemCategory()
        {
            var bookCategory = _bookDal.GetBookCategory();
            return bookCategory;
        }

        internal Book GetById(int? id)
        {
            Book book = _bookDal.GetById(id);
            return book;
        }

        internal bool Edit(Book book)
        {
            status = _bookDal.Edit(book);
            return status;
        }
    }
}