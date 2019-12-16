using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace cinema.Models
{
    public class ListViewModel
    {
        public Theatredetail[] theatredata { set; get; }
        public Booking[] bookingdata { set; get; }
        public seatselect[] seatselection { get; set; }
    }
}