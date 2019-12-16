using cinema.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace cinema.Models
{
    public class moviedetail : IIdentifiableEntity
    {
        public int movieid { get; set; }
        public string moviename { get; set; }
        public string moviedescription { get; set; }
        public string director { get; set; }
        public string movietype { get; set; }
        public string rating { get; set; }
        public string showingevent { get; set; }
        public string moviephotho { get; set; }
        public string trailer { get; set; }
        public string movie2D { get; set; }
        public string actor { get; set; }
        public TimeSpan? movietime { get; set; }
        public int EntityId
        { get { return movieid; }
            set { movieid = value; }
        }
    }
}