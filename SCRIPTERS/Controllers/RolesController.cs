using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.EntityFramework;
using SCRIPTERS.Models;

namespace SCRIPTERS.Controllers
{
    //[Authorize(Roles = "Manager")]
    public class RolesController : Controller
    {
        
         ApplicationDbContext _context;
        public RolesController()
        {
            _context = new ApplicationDbContext();
        }


        // GET: Roles
        public ActionResult Index()
        {
            var roles = _context.Roles.ToList();
            return View(roles);
        }

        public ActionResult Create()
        {
            var role = new IdentityRole();
            return View(role);
        }

        [HttpPost]
        public ActionResult Create(IdentityRole role)
        {
            _context.Roles.Add(role);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}