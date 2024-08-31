using PSW24.Core.Services;
using PSW24.Infrastructure;
using Quartz;

namespace PSW24_BackEnd
{
    public static class ModulesConfiguration
    {
        public static IServiceCollection RegisterModules(this IServiceCollection services)
        {
            services.ConfigureModule();


            services.AddQuartz(options =>
            {
                options.UseMicrosoftDependencyInjectionJobFactory();

                var jobKey = JobKey.Create(nameof(ReportService));
                options
                    .AddJob<ReportService>(jobKey)
                    .AddTrigger(trigger => trigger
                                            .ForJob(jobKey)
                                            .WithSimpleSchedule(s => s.WithIntervalInHours(24).RepeatForever()));
            });

            services.AddQuartzHostedService(opitions =>
            {
                opitions.WaitForJobsToComplete = true;
            });

            return services;
        }


    }
}
