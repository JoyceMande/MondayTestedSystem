using SCRIPTERS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;

namespace SCRIPTERS.Controllers
{
    public class ExportController : Controller
    {

        private ApplicationDbContext db = new ApplicationDbContext(); 

        public ActionResult ExportToXML()
        {

            db.Configuration.ProxyCreationEnabled = false;
            db.Configuration.LazyLoadingEnabled = false;
            var data = db.Customers.ToList();

            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment; filename = CustomerList.xml");
            Response.ContentType = "text/xml";

            
            var serializer = new System.Xml.Serialization.XmlSerializer(data.GetType());
            serializer.Serialize(Response.OutputStream, data);

            RedirectToAction("List","Customers");
            return View();
        }
    }
}