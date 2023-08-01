using Microsoft.Extensions.DependencyInjection;

namespace Common
{
    public static class CommonExtension
    {
        public static IServiceCollection AddCommonServices(this IServiceCollection services)
        {
            services.AddTransient<IGuidFactory, GuidFactory>();
            services.AddTransient<IMomentProvider, MomentProvider>();
            return services;
        }
    }
}
