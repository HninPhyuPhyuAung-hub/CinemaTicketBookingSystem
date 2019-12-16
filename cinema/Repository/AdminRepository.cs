using cinema.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace cinema.Repository
{
    public class AdminRepository : DataRepositoryBase<AdminUser, cinemacontext>

    {
        protected override DbSet<AdminUser> DbSet(cinemacontext entityContext)
        {
            return entityContext.AdminUserset;
        }

        protected override Expression<Func<AdminUser, bool>> IdentifierPredicate(cinemacontext entityContext, int id)
        {
            return (e => e.Id == id);
        }
    }
}
