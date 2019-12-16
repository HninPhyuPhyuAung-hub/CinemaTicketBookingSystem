using cinema.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace cinema.Repository
{
    public class theatreRepository : DataRepositoryBase<Theatredetail, cinemacontext>

    {
        protected override DbSet<Theatredetail> DbSet(cinemacontext entityContext)
        {
            return entityContext.Theatreset;
        }

        protected override Expression<Func<Theatredetail, bool>> IdentifierPredicate(cinemacontext entityContext, int id)
        {
            return (e => e.theatreid == id);
        }
    }
}
    