using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SCRIPTERS.Controllers
{
    [Authorize(Roles = "Manager")]
    public class ManageUsersController : Controller
    {
        // GET: ManageUsers
        public ActionResult Index()
        {
            return View();
        }
    }
}