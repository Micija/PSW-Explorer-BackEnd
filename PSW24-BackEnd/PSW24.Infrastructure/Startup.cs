using AutoMapper;
using PSW24.API.Public;
using PSW24.BuildingBlocks.Core.UseCases;
using PSW24.BuildingBlocks.Infrastructure.Database;
using PSW24.Core.Domain;
using PSW24.Core.Domain.RepositoryInterfaces;
using PSW24.Core.Mappers;
using PSW24.Core.Services;
using PSW24.Infrastructure.Auth;
using PSW24.Infrastructure.Database;
using PSW24.Infrastructure.Database.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace PSW24.Infrastructure
{
    public static class Startup
    {

        public static IServiceCollection ConfigureModule(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(UserProfile).Assembly);
            SetupCore(services);
            SetupInfrastructure(services);
            return services;
        }

        private static void SetupCore(IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<ITokenGenerator,JwtGenerator>();
            services.AddScoped<IInterestService, InterestService>();
            services.AddScoped<IUserInterestService, UserInterestService>();

        }

        private static void SetupInfrastructure(IServiceCollection services)
        {
            services.AddScoped(typeof(ICrudRepository<User>), typeof(CrudDatabaseRepository<User, Context>));
            services.AddScoped(typeof(ICrudRepository<Interest>), typeof(CrudDatabaseRepository<Interest, Context>));
            services.AddScoped(typeof(ICrudRepository<UserInterest>), typeof(CrudDatabaseRepository<UserInterest, Context>));


            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IInterestRepository, InterestRepository>();
            services.AddScoped<IUserInterestRepository, UserInterestRepository>();


            services.AddDbContext<Context>(opt =>
                opt.UseNpgsql(DbConnectionStringBuilder.Build("PSW24Schema"),
                    x => x.MigrationsHistoryTable("__EFMigrationsHistory", "PSW24Schema")));
        }

    }
}
