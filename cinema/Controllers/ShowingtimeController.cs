using cinema.Models;
using cinema.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace cinema.Controllers
{
    public class ShowingtimeController : Controller
    {
        // GET: Showingtime
        public ActionResult AddShowingTime()
        {
            return View();
        }
        public ActionResult getmoviename()
        {
            cinemacontext context = new cinemacontext();
            List<SelectListItem> droplist = new List<SelectListItem>();

            string[] mname = context.movieset
                                               .Select(e => e.moviename)

                                               .ToArray();
            foreach (var item in mname)
            {
                droplist.Add(new SelectListItem { Text = item, Value = item });
            }

            return Json(droplist, JsonRequestBehavior.AllowGet);
        }
        public ActionResult gettheatrename()
        {
            cinemacontext context = new cinemacontext();
            List<SelectListItem> droplist = new List<SelectListItem>();
            string[] tname = context.Theatreset.Select(a => a.therartname).Distinct().ToArray();
            foreach (var item in tname)
            {
                droplist.Add(new SelectListItem { Text = item, Value = item });

            }
            return Json(droplist, JsonRequestBehavior.AllowGet);
        }
        public ActionResult addshowtime(showingtime s )
        {
            showingtimeRepository srepo = new showingtimeRepository();
            if(s.showid==0)
            {
                srepo.Add(s);
            }
            else
            {
                srepo.Update(s);
            }
            return RedirectToAction("AddShowingTime", "Showingtime");
        }
        public ActionResult ShowingTimeList()
        {
            cinemacontext context = new cinemacontext();
            var result = context.showingtimeset.ToList().OrderByDescending(a => a.theatrename);
            return Json(new { data = result }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult EditShowingTime(int? showid)
        {
            cinemacontext context = new cinemacontext();
            showingtime[] result = null;
            if(showid!=null)
                {

                result = context.showingtimeset.Where(a => a.showid == showid).ToArray();
            }
            
          
            return Json(result, JsonRequestBehavior.AllowGet);
        }

    }
}
