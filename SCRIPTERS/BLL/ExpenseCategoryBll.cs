using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using SCRIPTERS.Core.Models;
using SCRIPTERS.DAL;

namespace SCRIPTERS.BLL
{
    public class ExpenseCategoryBll
    {
        ExpenseCategoryDal expenseCategoryDal = new ExpenseCategoryDal();
        bool status = false;
        public bool Create (ExpenseCategory expenseCategory )
        {
            
            status = expenseCategoryDal.Create(expenseCategory);

            return status;
        }

        public bool ImageValidation (HttpPostedFileBase ImageFile)
        {
            if(ImageFile!= null)
            {
                var extension = Path.GetExtension(ImageFile.FileName)?.ToLower();
                var fileName = Path.GetFileName(ImageFile.FileName);

                var allowExtension = new[]
                {
                    ".jpg",
                    ".png",
                    ".jpeg"
                };
                if(allowExtension.Contains(extension))
                {
                    status = true;
                }
            }
            return status;
        }

        internal object GenerateAutoCode()
        {
            var autoCode = expenseCategoryDal.GenerateAutoCode();
            return autoCode;
        }

        internal List<ExpenseCategory> List()
        {
            List<ExpenseCategory> listOfExpenseCategory = expenseCategoryDal.List();

            return listOfExpenseCategory;
        }

        internal byte[] ConvertImage(HttpPostedFileBase imageFile)
        {
           byte[] Image = new byte[imageFile.ContentLength];
            imageFile.InputStream.Read(Image, 0, imageFile.ContentLength);
            return Image;           
        }

        internal ExpenseCategory GetById(int? id)
        {
            ExpenseCategory expenseCategory = expenseCategoryDal.GetById(id);
            
            return expenseCategory;
        }

        internal bool Edit(ExpenseCategory expenseCategories)
        {
            status = expenseCategoryDal.Edit(expenseCategories);
            return status;
        }

        internal bool Delete(int id)
        {
            status = expenseCategoryDal.Delete(id);
            return status;
        }

        internal List<ExpenseCategory> GetParentCategories()
        {
            List<ExpenseCategory> expenseCategories = expenseCategoryDal.GetParentCategories();

            return expenseCategories;
        }
    }
}