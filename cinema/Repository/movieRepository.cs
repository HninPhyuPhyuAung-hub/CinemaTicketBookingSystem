using cinema.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace cinema.Repository
{
    public class movieRepository : DataRepositoryBase<moviedetail, cinemacontext>
     
    {
        protected override DbSet<moviedetail> DbSet(cinemacontext entityContext)
        {
            return entityContext.movieset;
        }

        protected override Expression<Func<moviedetail, bool>> IdentifierPredicate(cinemacontext entityContext, int id)
        {
            return (e => e.movieid == id);
        }
    }
}