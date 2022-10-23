using Microsoft.EntityFrameworkCore;
using MMGTS.Infra.EF.Context;


namespace MMGTS.Server.Config
{
    public static class Database
    {
        public static IServiceCollection ConfigureDatabase(this IServiceCollection services, string connectionString)
        {

#if (DEBUG)
            services.AddDbContext<DataContext>(options =>
                    options
                    .UseNpgsql(connectionString)
                    .UseLoggerFactory(LoggerFactory.Create(builder => builder.AddConsole()))
                    // ⚠️ !! Ativado somente em modo debug !! ⚠️ 
                    .EnableSensitiveDataLogging()
            );

#elif (RELEASE)
            services.AddDbContextPool<DataContext>(options =>
                               options
                               .UseNpgsql(connectionString)
                               .UseLoggerFactory(LoggerFactory.Create(builder => builder.AddConsole()))
                       );
#endif
            services.AddScoped<DataContext>();
            return services;
        }

        public static IApplicationBuilder ConfigureMigration(this IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>()?.CreateScope();
            var context = serviceScope?.ServiceProvider.GetRequiredService<DataContext>();

            context?.Database.Migrate();

            return app;
        }
    }
}
