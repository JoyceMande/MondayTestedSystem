using SCRIPTERS.Core.Models;
using SCRIPTERS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SCRIPTERS.Controllers
{
    //[Authorize]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Contact()
        {
            return View();
                 
        }
        public ActionResult About()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetEvents()
        {
            using (ApplicationDbContext dc = new ApplicationDbContext())
            {
                var events = dc.Events.ToList();
                return new JsonResult { Data = events, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
        }

        [HttpPost]
        public JsonResult SaveEvent(Event e)
        {
            var status = false;
            using (ApplicationDbContext dc = new ApplicationDbContext())
            {
                if (e.EventId > 0)
                {
                    //Update the event
                    var v = dc.Events.Where(a => a.EventId == e.EventId).FirstOrDefault();
                    if (v != null)
                    {
                        v.Subject = e.Subject;
                        v.Start = e.Start;
                        v.End = e.End;
                        v.Description = e.Description;
                        v.IsFullDay = e.IsFullDay;
                        v.ThemeColor = e.ThemeColor;
                    }
                }
                else
                {
                    dc.Events.Add(e);
                }

                dc.SaveChanges();
                status = true;
                

            }
            return new JsonResult { Data = new { status } };
        }

        [HttpPost]
        public JsonResult DeleteEvent(int eventID)
        {
            var status = false;
            using (ApplicationDbContext dc = new ApplicationDbContext())
            {
                var v = dc.Events.Where(a => a.EventId == eventID).FirstOrDefault();
                if (v != null)
                {
                    dc.Events.Remove(v);
                    dc.SaveChanges();
                    status = true;
                }
            }
            return new JsonResult { Data = new { status } };
        }
    }
}