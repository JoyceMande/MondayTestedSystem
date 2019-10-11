using System.IO;
using System.Linq;
using System.Web;
using SCRIPTERS.BLL;
using SCRIPTERS.Models;

namespace SCRIPTERS.Core.Models
{
    public class InventoryCommon
    {
        bool _status;
        ApplicationDbContext db = new ApplicationDbContext();
        InventoryCommonBll _commonBll = new InventoryCommonBll();

        internal byte[] ConvertImage(HttpPostedFileBase imageFile)
        {
            byte[] Image = new byte[imageFile.ContentLength];
            imageFile.InputStream.Read(Image, 0, imageFile.ContentLength);
            return Image;
        }

        public bool ImageValidation(HttpPostedFileBase imageFile)
        {
            if (imageFile != null)
            {
                var extension = Path.GetExtension(imageFile.FileName)?.ToLower();
                var fileName = Path.GetFileName(imageFile.FileName);

                var allowExtension = new[]
                {
                    ".jpg",
                    ".png",
                    ".jpeg"
                };
                if (allowExtension.Contains(extension))
                {
                    _status = true;
                }
            }
            return _status;
        }

       internal dynamic GetItemStockById(int id)
        {
            var itemStock = _commonBll.GetItemStockById(id);
            return itemStock;
        }

       

    }
}