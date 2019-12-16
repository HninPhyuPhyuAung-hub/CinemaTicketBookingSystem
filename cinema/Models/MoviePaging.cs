using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace cinema.Models
{
    public class MoviePaging
    {
        public IPagedList<moviedetail> movieList { get; set; }
        public int TotalCount { get; set; }
        public int TotalPages { get; set; }
        public string nextLink { get; set; }
        public string prevLink { get; set; }
    }

    public class MovieList
    {
        public IEnumerable<moviedetail> movieList { get; set; }
        public int TotalCount { get; set; }
        public int TotalPages { get; set; }
        public string nextLink { get; set; }
        public string prevLink { get; set; }
    }
}