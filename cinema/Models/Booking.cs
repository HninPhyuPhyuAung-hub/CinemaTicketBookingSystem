using cinema.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace cinema.Models
{
    public class Booking: IIdentifiableEntity
    {
        public int tempid { get; set; }
        public string username { get; set; }
        public string phonenumber { get; set; }
        
        public string seat { get; set; }
        public bool IsPaid { get; set; }

        public Double Amount { get; set; }
       
        public int? showid { get; set; }

        public DateTime? bookingtime { get; set; }
        public int EntityId
        {
            get { return tempid; }
            set { value = tempid; }
        }

    }
}