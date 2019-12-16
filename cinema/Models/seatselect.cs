using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace cinema.Models
{
    public class seatselect
    {
        public string seatname { get; set; }
        public Boolean? ispaid { get; set; }
        public int? showid { get; set; }
        public Double price { get; set; }
    }
}