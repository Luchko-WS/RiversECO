using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace RiversECO.DataContext
{
    public class DataContextFactory : IDesignTimeDbContextFactory<DataContext>
    {
        public DataContextFactory()
        {
            ConnectionString = GetConfigurationString();
        }

        public DataContextFactory(string connectionString)
        {
            ConnectionString = connectionString;
        }

        public string ConnectionString { get; private set; }

        DataContext IDesignTimeDbContextFactory<DataContext>.CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<DataContext>();
            builder.UseSqlServer(ConnectionString);
            return new DataContext(builder.Options);
        }

        private string GetConfigurationString()
        {
            var path = Path.Combine(
               Directory.GetParent(Directory.GetCurrentDirectory()).FullName,
               "RiversECO.API");

            var configuration = new ConfigurationBuilder()
                .SetBasePath(path)
                .AddJsonFile("appsettings.json")
                .AddJsonFile("appsettings.Development.json")
                .Build();

            var connectionString = configuration.GetConnectionString("DefaultConnection");
            return connectionString;
        }
    }
}
