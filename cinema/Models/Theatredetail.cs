using cinema.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace cinema.Models
{
    public class Theatredetail : IIdentifiableEntity
    {
        public int theatreid { get; set; }
        public string seatname { get; set; }
        public string therartname { get; set; }
        public string seattype { get; set; }

        public Double seatprice { get; set; }

        public int EntityId
        {
            get { return theatreid; }
            set { theatreid = value; }
        }
    }
}