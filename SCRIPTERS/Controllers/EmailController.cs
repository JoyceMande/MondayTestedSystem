using SCRIPTERS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SCRIPTERS.Controllers
{
    public class EmailController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            //this opens view to create email
            return View();
        }


        [HttpPost]

        public ActionResult SendEmailView()
        {
            //call SendEmailView view to invoke webmail  --check this view to see the code that is called
            return View();
        }
    }
}