using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MyTime.Shared.Data;
using MyTime.Shared.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyTime.Shared.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddMyTimeDbContext(
            this IServiceCollection services,
            string connectionString,
            bool isDevelopment)
        {
            services.AddDbContext<MyTimeDbContext>(options =>
            {
                options.UseNpgsql(connectionString, npgsql =>
                {
                    npgsql.MigrationsAssembly(typeof(MyTimeDbContext).Assembly.FullName);
                });

                if (isDevelopment)
                {
                    options.UseSeeding((context, _) =>
                    {
                        var existingPunch1 = context.Find(typeof(Punch), 1);
                        var existingPunch2 = context.Find(typeof(Punch), 2);
                        Punch punch1 = new() { Id = 1, PunchedUserId = 1, PunchedTime = DateTime.UtcNow.AddHours(-5), PunchType = Enums.PunchType.In };
                        Punch punch2 = new() { Id = 2, PunchedUserId = 1, PunchedTime = DateTime.UtcNow.AddHours(-1), PunchType = Enums.PunchType.Out };
                        if (existingPunch1 is null) context.Add(punch1);
                        if (existingPunch2 is null) context.Add(punch2);
                        context.SaveChanges();
                    });
                }
            });

            return services;
        }
    }
}
