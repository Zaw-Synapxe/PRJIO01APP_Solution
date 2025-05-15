using Application.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Contexts;
using Persistence.Repositories;

namespace Persistence.Extensions
{
    public static class IServiceCollectionExtensions
    {
        public static void AddPersistenceLayer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext(configuration);
            services.AddRepositories();
        }

        public static void AddDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            // https://muratsuzen.medium.com/using-multiple-dbcontext-on-net-6-web-api-with-repository-pattern-3d223874e625
            // https://github.com/muratsuzen/MultipleDbContext/blob/master/MultipleDbContext/Program.cs
            // ---------------------------------------
            // Multiple DB context

            // DB One
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            if (connectionString == null)
            {
                throw new ApplicationException("DefaultConnection is not set");
            }
            services.AddDbContext<ApplicationDbContext>(options =>
               options.UseSqlServer(connectionString,
                   builder => builder.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));
            //

            // ---------------------------------------
            // DB Two
            var connectionString2 = configuration.GetConnectionString("DefaultConnection2");
            if (connectionString2 == null)
            {
                throw new ApplicationException("DefaultConnection2 is not set");
            }

            services.AddDbContext<ApplicationDb2Context>(options =>
               options.UseSqlServer(connectionString2,
                   builder => builder.MigrationsAssembly(typeof(ApplicationDb2Context).Assembly.FullName)));

            //
        }

        private static void AddRepositories(this IServiceCollection services)
        {
            // ------------------------------
            #region Repositories
            services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddTransient<IDeveloperRepository, DeveloperRepository>();
            services.AddTransient<IProjectRepository, ProjectRepository>();
            #endregion
            services.AddTransient<IUnitOfWork, UnitOfWork>();

            //
        }

        //



    }
}
