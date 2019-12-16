using cinema.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace cinema.Repository
{
    public class ReceiveMessageRepository : DataRepositoryBase<ReceiveMessage, cinemacontext>

    {
        protected override DbSet<ReceiveMessage> DbSet(cinemacontext entityContext)
        {
            return entityContext.ReceiveMessageset;
        }

        protected override Expression<Func<ReceiveMessage, bool>> IdentifierPredicate(cinemacontext entityContext, int id)
        {
            return (e => e.Id == id);
        }
    }
}
