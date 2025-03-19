using Microsoft.EntityFrameworkCore;
using TransportApp.Server.DataModels;

namespace TransportApp.Server.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<TruckMaster> TruckMaster { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TruckMaster>()
                .HasKey(t => t.Truck_No); // Explicitly define primary key

            base.OnModelCreating(modelBuilder);
        }

    }
}
