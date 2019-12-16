using cinema.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace cinema.Models
{
    public class showingtime : IIdentifiableEntity
    {
        public int showid { get; set; }
        public DateTime? time { get; set; }
        public string moviename { get; set; }
        public string theatrename { get; set; }

        
        public int EntityId
        { get { return showid; }
        set { value = showid; }
        }
    }
}