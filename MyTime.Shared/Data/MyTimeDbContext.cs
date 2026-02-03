using Microsoft.EntityFrameworkCore;
using MyTime.Shared.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyTime.Shared.Data
{
    public class MyTimeDbContext(DbContextOptions<MyTimeDbContext> options) : DbContext(options)
    {
        public DbSet<Punch> Punches { get; set; }

        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            // save enums as strings
            configurationBuilder.Properties<Enum>().HaveConversion<string>();
        }
    }
}
