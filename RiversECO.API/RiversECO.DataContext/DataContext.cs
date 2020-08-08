using Microsoft.EntityFrameworkCore;
using RiversECO.Models;

namespace RiversECO.DataContext
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
            : base(options) { }

        public DbSet<Criteria> Criterias { get; set; }
    }
}
