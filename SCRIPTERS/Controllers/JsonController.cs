using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SCRIPTERS.Models;
using System.IO; 
using Newtonsoft.Json;
using SCRIPTERS.Core.Models;

namespace SCRIPTERS.Controllers
{
    public class JsonController : Controller
    {
        // GET: Json
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult uploadJson(HttpPostedFileBase filejson)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            {
               if(filejson != null)
                {
                    if (!filejson.FileName.EndsWith("json"))
                    {
                        ViewBag.ErrorMessage = "Error: Only Json File types are allowed";
                    }
                    else
                    {
                        filejson.SaveAs(Server.MapPath("~/jsonFolder/" + Path.GetFileName(filejson.FileName))); //Copy json file to specific folder  
                        StreamReader reader = new StreamReader(Server.MapPath("~/jsonFolder/" + Path.GetFileName(filejson.FileName)));
                        string jsondata = reader.ReadToEnd();

                        List<Customer> custList = JsonConvert.DeserializeObject<List<Customer>>(jsondata);

                        foreach (var item in custList)
                        {
                            @item.Name.ToString();
                            @item.Code.ToString();
                            @item.ContactNo.ToString();
                            @item.Email.ToString();
                            @item.Address.ToString();
                            db.Customers.Add(item);
                            db.SaveChanges();

                        }
                        ViewBag.message = "Selected file " + Path.GetFileName(filejson.FileName) + " Successfully Imported";
                    }
                }
               else
                {
                    ViewBag.ErrorMessage = "Please Upload a file";
                }
            }
            return View("Index");
        }
    }
} 