using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace cinema.Models
{
    public class cinemacontext : DbContext
    {
        public cinemacontext() : base("name=cinema")
        {
            Database.SetInitializer<cinemacontext>(null);
        }
        public DbSet<moviedetail> movieset { get; set; }
        
        public DbSet<Theatredetail> Theatreset { get; set; }

        public DbSet<showingtime> showingtimeset { get; set; }
        public DbSet<Booking> bookingset { get; set; }

        public DbSet<Temporarydata> Temporarydataset { get; set; }
        public DbSet<ReceiveMessage> ReceiveMessageset { get; set; }

        public DbSet<AdminUser> AdminUserset { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // modelBuilder.Ignore<ExtensionDataObject>();
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Entity<moviedetail>().HasKey<int>(e => e.movieid).Ignore(e => e.EntityId);

            modelBuilder.Entity<Theatredetail>().HasKey<int>(e => e.theatreid).Ignore(e => e.EntityId);
            modelBuilder.Entity<showingtime>().HasKey<int>(e => e.showid).Ignore(e => e.EntityId);
            modelBuilder.Entity<Booking>().HasKey<int>(e => e.tempid).Ignore(e => e.EntityId);
            modelBuilder.Entity<Temporarydata>().HasKey<int>(e => e.temporaryid).Ignore(e => e.EntityId);
            modelBuilder.Entity<ReceiveMessage>().HasKey<int>(e => e.Id).Ignore(e => e.EntityId);
            modelBuilder.Entity<AdminUser>().HasKey<int>(e => e.Id).Ignore(e => e.EntityId);
        }
    }
}