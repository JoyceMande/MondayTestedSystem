using System.Collections.Generic;
using SCRIPTERS.Core.Models;
using SCRIPTERS.DAL;

namespace SCRIPTERS.BLL
{
    public class BookCategoryBll
    {
        BookCategoryDal _bookCategoryDal = new BookCategoryDal();
        bool status;

        internal List<BookCategory> List()
        {
            List<BookCategory> bookCategories = _bookCategoryDal.List();

            return bookCategories;
        }

        internal dynamic GenerateAutoCode()
        {
            var autoCode = _bookCategoryDal.GenerateAutoCode();
            return autoCode;
        }

        internal bool Create(BookCategory itemCategory)
        {
            status = _bookCategoryDal.Create(itemCategory);

            return status;
        }

        internal bool Delete(int id)
        {
            status = _bookCategoryDal.Delete(id);

            return status;
        }

        internal BookCategory GetById(int? id)
        {
            BookCategory bookCategory = _bookCategoryDal.GetById(id);
            return bookCategory;
        }

        internal bool Edit(BookCategory bookCategory)
        {
            status = _bookCategoryDal.Edit(bookCategory);
            return status;
        }
    }
}