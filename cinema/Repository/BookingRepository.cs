using cinema.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace cinema.Repository
{
    public class BookingRepository : DataRepositoryBase<Booking, cinemacontext>

    {
        protected override DbSet<Booking> DbSet(cinemacontext entityContext)
        {
            return entityContext.bookingset;
        }

        protected override Expression<Func<Booking, bool>> IdentifierPredicate(cinemacontext entityContext, int id)
        {
            return (e => e.tempid == id);
        }
    }
}
