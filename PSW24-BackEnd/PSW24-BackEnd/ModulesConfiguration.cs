using PSW24.Infrastructure;

namespace PSW24_BackEnd
{
    public static class ModulesConfiguration
    {
        public static IServiceCollection RegisterModules(this IServiceCollection services)
        {
            services.ConfigureModule();


            return services;
        }

    }
}
