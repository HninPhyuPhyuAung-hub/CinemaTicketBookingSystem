using cinema.Models;
using cinema.Repository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace cinema.Controllers
{
    public class BookingController : Controller
    {
        // GET: Booking
        
        public ActionResult BookMovie(int? movieid)
        {
            cinemacontext context = new cinemacontext();
            var moviename = context.movieset.Where(a => a.movieid == movieid).Select(a => a.moviename).FirstOrDefault();
            ViewBag.moviename = moviename;
            var vm = new ListViewModel();     
            List<seatselect> result = new List<seatselect>();
            vm.seatselection = (from prsn in context.Theatreset
                                from co in context.Temporarydataset.Where(co => co.seatname == prsn.seatname && co.showid == 2).DefaultIfEmpty()
                                where prsn.therartname == "Theatre1"
                                select new seatselect
                                {
                                    seatname = prsn.seatname,
                                    ispaid = co.ispaid,
                                    showid = co.showid,
                                    price = prsn.seatprice
                                }).ToArray();
            return View(vm);
        }

        public ActionResult ChooseTheatre(string moviename)
        {
            cinemacontext context = new cinemacontext();
            var result = context.showingtimeset.Where(a => a.moviename == moviename).GroupBy(a => new { a.theatrename }).
                Select(g => new
                {
                    theatrename = g.Select(b => b.theatrename).FirstOrDefault()
                }
                ).ToList();
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ChooseDate(string theartre,string moviename)
        {
            cinemacontext context = new cinemacontext();
            var result = context.showingtimeset.Where(a => a.theatrename == theartre && a.moviename == moviename).ToList();
            movietimejson data = new movietimejson();
            data.movietime = result.Select(a => a.time.Value.ToString("MMMM dd/ yyyy")).Distinct().ToArray(); 
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ChooseTime(string theartre, string moviename)
        {
            cinemacontext context = new cinemacontext();
            var result = context.showingtimeset.Where(a => a.theatrename == theartre && a.moviename == moviename).ToList();
            movietimejson data = new movietimejson();
            data.movietime = result.Select(a => a.time.Value.ToString("HH:MM")).Distinct().ToArray();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SeatSelect(string moviename,string theartre,string date,string time)
        {
            cinemacontext context = new cinemacontext();
            DateTime d1 = Convert.ToDateTime(date);
            DateTime d2 = Convert.ToDateTime(time);
            var showid = context.showingtimeset.Where(a => DbFunctions.TruncateTime(a.time) == DbFunctions.TruncateTime(d1) && a.time.Value.Hour == d2.Hour && a.time.Value.Minute == d2.Minute && a.theatrename==theartre &&a.moviename==moviename).Select(e => e.showid).FirstOrDefault();
            var vm = new ListViewModel();
            vm.seatselection = (from prsn in context.Theatreset
                                from co in context.bookingset.Where(co => co.seat == prsn.seatname && co.showid == showid).DefaultIfEmpty()
                                where prsn.therartname == theartre
                                select new seatselect
                                {
                                    seatname = prsn.seatname,
                                    ispaid = co.IsPaid,
                                    showid=co.showid,
                                    price=prsn.seatprice

                                }).ToArray();
            return View("BookMovie",vm);
        }

        public ActionResult UserBooking(string name, string phone, string seat, int? showid,string amount,int totalprice)
        {
            BookingRepository bb = new BookingRepository();
            cinemacontext context = new cinemacontext();
            Booking book = new Booking();
            if (seat.Contains(","))
            {
                List<string> seatname = seat.Split(',').ToList();
                List<string> seatprice = amount.Split(',').ToList();
                for (int i = 0; i < seatname.Count; i++)
                {
                    book.IsPaid = false;
                    book.username = name;
                    book.phonenumber = phone;
                    book.showid = showid;
                    book.seat = seatname[i];
                    book.Amount = Convert.ToDouble(seatprice[i]);
                    book.bookingtime = DateTime.UtcNow;
                    bb.Add(book);
                }
            }
            else
            {
                book.IsPaid = false;
                book.username = name;
                book.phonenumber = phone;
                book.seat = seat;
                book.showid = showid;
                book.bookingtime = DateTime.UtcNow;
                bb.Add(book);
            }
            var result = (from c in context.showingtimeset
                               where c.showid == showid
                               select new TicketDetail
                               {
                                   moviename = c.moviename,
                                   movietime = c.time,
                                   moviephoto=context.movieset.Where(a=>a.moviename==c.moviename).Select(a=>a.moviephotho).FirstOrDefault(),
                                   username = name,
                                   phonenumber=phone,
                                   seatname=seat,
                                   price= totalprice
                                
                               }).ToList() ;
            return View(result);
        }
        public ActionResult BookingResult()
        {
            return View();
        }
    }
}