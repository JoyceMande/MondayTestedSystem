using Nexmo.Api;
using SCRIPTERS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SCRIPTERS.Core;

namespace SCRIPTERS.Controllers
{
    public class SMSController : Controller
    {
      private  ApplicationDbContext db = new ApplicationDbContext();
       
        public ActionResult Index()
        {
            List<SmsModel> sms = new List<SmsModel>();
            return View(sms);
        }

        public ActionResult Send()
        {

            return View();
        }

        [System.Web.Mvc.HttpPost]
        public ActionResult Send(SmsModel model)
        {
           // text = "Scripters Testing Sms" ----for automated text 
            var results = SMS.Send(new SMS.SMSRequest
            {
                from = Configuration.Instance.Settings["appsettings:NEXMO_FROM_NUMBER"],
                to = model.ToNo,
                text =model.SmsBody
            });
            return View("Index");
           
        }
        
    }
}