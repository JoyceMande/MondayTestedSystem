using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SCRIPTERS.Models;

namespace SCRIPTERS.Controllers
{
    public class AuditTrailController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Audit_Trail

  
        public ActionResult Index(string searchBy = "", string search = "")
        {
            var list = db.Audits.ToList();
            /*if (searchBy == "date")
            {
                return View(db.Audit_Trail.Where(x => x.Transaction_Date.Contains(search) || search == null).ToList());
            }
            else*/
            if (searchBy == "user")
            {
                return View(db.Audits.Where(x => x.User.Contains(search) || search == null).ToList());
            }
            else if (searchBy == "type")
            {
                return View(db.Audits.Where(x => x.TransactionType.StartsWith(search) || search == null).ToList());
            }
            else
            {
                return View(list);
            }
        }
    }
}