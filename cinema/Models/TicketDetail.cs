using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace cinema.Models
{
    public class TicketDetail
    {
        public string username { get; set; }
        public string phonenumber { get; set; }
        public string seatname { get; set; }
        public double price { get; set; }

        public string moviename { get; set; }
        public DateTime? movietime { get; set; } 
        public string moviephoto { get; set; }
    }
}