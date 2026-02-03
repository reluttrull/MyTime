using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace MyTime.Shared.Data
{
    public class MyTimeDbContextFactory : IDesignTimeDbContextFactory<MyTimeDbContext>
    {
        public MyTimeDbContext CreateDbContext(string[] args)
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true)
                .AddJsonFile("appsettings.Development.json", optional: true)
                .AddEnvironmentVariables()
                .Build();

            var connectionString =
                config.GetConnectionString("mytimedb")
                ?? "Host=localhost;Port=5432;Database=mytime;Username=postgres;Password=postgres";

            var options = new DbContextOptionsBuilder<MyTimeDbContext>()
                .UseNpgsql(connectionString)
                .Options;

            return new MyTimeDbContext(options);
        }
    }
}
