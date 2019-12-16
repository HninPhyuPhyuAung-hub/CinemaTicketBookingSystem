using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace cinema.Models
{
    public class Temporarydata
    {
        public int temporaryid { get; set; }
        public int showid { get; set; }
        public string seatname { get; set; }
        public Boolean ispaid { get; set; }
        public int EntityId
        {
            get { return temporaryid; }
            set { value = temporaryid; }
        }
    }
}