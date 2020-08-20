using Microsoft.EntityFrameworkCore;
using RiversECO.Models;

namespace RiversECO.DataContext
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
            : base(options) { }

        public DbSet<Criteria> Criterias { get; set; }
        public DbSet<WaterObject> WaterObjects { get; set; }
        public DbSet<Review> Reviews { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<WaterObject>()
                .HasMany(o => o.Reviews)
                .WithOne(r => r.WaterObject)
                .HasForeignKey(r => r.WaterObjectId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
