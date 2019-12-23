using cinema.Models;
using cinema.Repository;
using PagedList;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace cinema.Controllers
{
    public class CinemaController : Controller
    {
        // GET: Cinema
        public ActionResult Index(string showingtime = "NowShowing")
        {
            cinemacontext context = new cinemacontext();
            moviedetail[] movielist = context.movieset.Where(a => a.showingevent == showingtime).ToArray();
            return View(movielist);
        }

        public ActionResult Comingdata(string showingtime = "")
        {
            cinemacontext context = new cinemacontext();
            moviedetail[] movielist = context.movieset.Where(a => a.showingevent == showingtime).ToArray();
            ViewBag.Message = showingtime;
            return View("Index", movielist);
        }

        public ActionResult Addmoive(int? movieid)
        {
            cinemacontext context = new cinemacontext();
            moviedetail result = context.movieset.Where(a => a.movieid == movieid).FirstOrDefault();
            if (movieid != null)
            {
                return View(result);
            }
            else
            {
                return View();
            }
        }

        [HttpPost]
        public ActionResult Addmoive(moviedetail m, HttpPostedFileBase image, HttpPostedFileBase movie)
        {
            cinemacontext context = new cinemacontext();
            if (movie != null)
            {
                movie.SaveAs(Path.Combine(Server.MapPath("~/Video"), movie.FileName));
                m.trailer = "~/Video/" + movie.FileName;
            }
            if (image != null)
            {
                image.SaveAs(HttpContext.Server.MapPath("~/Image/")+ image.FileName);
                m.moviephotho = "~/Image/" + image.FileName;
            }
            m.moviephotho = m.moviephotho.Replace("~", string.Empty);
            m.trailer = m.trailer.Replace("~", string.Empty);
            if (m.movieid != 0)
            {
                movieRepository movrepo = new movieRepository();
                movrepo.Update(m);
            }
            else
            {
                context.movieset.Add(m);
            }
            context.SaveChanges();
            ModelState.Clear();
            return RedirectToAction("MovieList", "Cinema");
        }

        public ActionResult MovieList(int page = 1, int pagesize = 10)
        {
            cinemacontext context = new cinemacontext();
            var model = new MoviePaging();
            moviedetail[] movielist = context.movieset.ToArray();
            var totalcount = movielist.Count();
            var totalpage = (int)Math.Ceiling((double)totalcount / pagesize);
            var pagedList = new StaticPagedList<moviedetail>(movielist, page, pagesize, totalcount);
            model.movieList = pagedList;
            model.TotalCount = totalcount;
            model.TotalPages = totalpage;
            return View(model);
        }

        public ActionResult Deletemovie(int movieid)
        {
            cinemacontext context = new cinemacontext();
            var sqlquery = String.Format("Delete moviedetail WHERE movieid={0}", movieid);
            context.Database.ExecuteSqlCommand(sqlquery);
            return Json("Success", JsonRequestBehavior.AllowGet);
        }

        public ActionResult UpdateSeatPrice(FormCollection fc)
        {
            cinemacontext context = new cinemacontext();
            string seatname = fc["seatname"];
            string seatprice = fc["seatprice"];
            string theatrename = fc["theatrename"];
            List<string> stringList = seatname.Split(',').ToList();
            //List<int> result = new List<int>();
            string seat = "";
            for (int i = 0; i < stringList.Count; i++)
            {
                seat = stringList[i];
                int result = context.Theatreset.Where(a => a.seatname == seat && a.therartname == theatrename).Select(a => a.theatreid).FirstOrDefault();
                var sqlquery = String.Format("Update Theatredetail SET seatprice={0} WHERE theatreid={1}", seatprice, result);
                context.Database.ExecuteSqlCommand(sqlquery);
            }
            ViewBag.Message = "Successfully Updated Price!";
            return RedirectToAction("EditTheartre", "Cinema");
        }

        public ActionResult Addseat(FormCollection fc)
        {
            cinemacontext context = new cinemacontext();
            string theatrename = fc["therartname"];
            int gcount = 60;
            string goldprice = fc["goldprice"];
            int pcount = 60;
            string platprice = fc["platprice"];
            int scount = 60;
            string silverprice = fc["silverprice"];
            Double sprice = Convert.ToDouble(silverprice);
            Double pprice = Convert.ToDouble(platprice);
            Double gprice = Convert.ToDouble(goldprice);
            Theatredetail m = new Theatredetail();
            for (int i = 1; i <= scount; i++)
            {
                var result = "";
                if (i <= 20)
                {
                    result = "A" + Convert.ToString(i);
                }
                else if (i > 20 && i <= 40)
                {

                    result = "B" + Convert.ToString(i - 20);
                }
                else if (i > 40 && i <= 60)
                {
                    result = "C" + Convert.ToString(i - 40);
                }
                m.therartname = theatrename;
                m.seattype = "Silver";
                m.seatprice = sprice;
                m.seatname = result;
                context.Theatreset.Add(m);
                context.SaveChanges();
            }
            for (int j = 1; j <= pcount; j++)
            {
                var result = "";
                if (j <= 20)
                {
                    result = "D" + Convert.ToString(j);
                }
                else if (j > 20 && j <= 40)
                {
                    result = "D" + Convert.ToString(j - 20);
                }
                else if (j > 40 && j <= 60)
                {
                    result = "E" + Convert.ToString(j - 40);
                }
                m.therartname = theatrename;
                m.seattype = "Platinum";
                m.seatprice = pprice;
                m.seatname = result;
                context.Theatreset.Add(m);
                context.SaveChanges();
            }
            for (int k = 1; k <= gcount; k++)
            {
                var result = "";
                if (k <= 20)
                {
                    result = "F" + Convert.ToString(k);
                }
                else if (k > 20 && k <= 40)
                {
                    result = "G" + Convert.ToString(k - 20);
                }
                else if (k > 40 && k <= 60)
                {
                    result = "H" + Convert.ToString(k - 40);
                }
                m.therartname = theatrename;
                m.seattype = "Gold";
                m.seatprice = gprice;
                m.seatname = result;
                context.Theatreset.Add(m);
                context.SaveChanges();
            }
            return RedirectToAction("AddTheatre", "Cinema");
        }

        public ActionResult SeatList(string theatrename)
        {
            cinemacontext context = new cinemacontext();
            Theatredetail[] result = context.Theatreset.Where(a => a.therartname == theatrename).ToArray();
            ViewBag.data = "a";
            return View("EdtiTheartre", result);
        }

        public ActionResult AddTheatre()
        {
            return View();
        }

        public ActionResult TheartreList()
        {
            cinemacontext context = new cinemacontext();
            var result = context.Theatreset.GroupBy(a => new { a.therartname, a.seattype })
                       .Select(g => new
                       {
                           therartname = g.Select(a => a.therartname).FirstOrDefault(),
                           seatname = g.Select(a => a.seattype).FirstOrDefault(),
                           total = g.Select(a => a.seatname).Count(),
                           theatreid = g.Select(a => a.theatreid).FirstOrDefault(),
                           price = g.Select(a => a.seatprice).FirstOrDefault()
                       }).ToList();
            return Json(new { data = result }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult EditTheartre(string theatrename = "Theatre1")
        {
            cinemacontext context = new cinemacontext();
            Theatredetail[] result = context.Theatreset.Where(a => a.therartname == theatrename).ToArray();
            return View(result);
        }

        public ActionResult getTheartre()
        {
            cinemacontext context = new cinemacontext();
            var result = context.Theatreset.Select(a => a.therartname).Distinct().ToList();
            List<SelectListItem> droplist = new List<SelectListItem>();
            foreach (var item in result)
            {
                droplist.Add(new SelectListItem { Text = item, Value = item });
            }
            return Json(droplist, JsonRequestBehavior.AllowGet);
        }
       
        public ActionResult ViewDetail(int? movieid)
        {
            cinemacontext context = new cinemacontext();
            moviedetail result = new moviedetail();
            result = context.movieset.Where(c => c.movieid == movieid).FirstOrDefault();
            return View(result);
        }
        public ActionResult Browsemovie(string showingtime,int page=1,int pagesize=10)
        {
            cinemacontext context = new cinemacontext();
            var model = new MoviePaging();
            moviedetail[] movielist = new moviedetail[0];
            if (showingtime==null)
            {
               movielist = context.movieset.ToArray();
            }
            else
            {
                movielist = context.movieset.Where(a => a.showingevent == showingtime).ToArray();
            }
            var totalcount = movielist.Count();
            var totalpage = (int)Math.Ceiling((double)totalcount / pagesize);
            var pagedList = new StaticPagedList<moviedetail>(movielist, page, pagesize, totalcount);
            model.movieList = pagedList;
            model.TotalCount = totalcount;
            model.TotalPages = totalpage;
            return View(model);
        }

        public ActionResult ReceiveMessage(string Name,string Email,string Message)
        {
            ReceiveMessage rm = new ReceiveMessage();
            ReceiveMessageRepository rmrepo = new ReceiveMessageRepository();
            rm.Name = Name;
            rm.Email = Email;
            rm.Message = Message;
            rmrepo.Add(rm);
            return Json("Success", JsonRequestBehavior.AllowGet);
        }
    }
}
