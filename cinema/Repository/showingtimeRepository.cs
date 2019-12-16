using cinema.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace cinema.Repository
{
    public class showingtimeRepository : DataRepositoryBase<showingtime, cinemacontext>

    {
        protected override DbSet<showingtime> DbSet(cinemacontext entityContext)
        {
            return entityContext.showingtimeset;
        }

        protected override Expression<Func<showingtime, bool>> IdentifierPredicate(cinemacontext entityContext, int id)
        {
            return (e => e.showid == id);
        }
    }
}