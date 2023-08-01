
using Microsoft.Extensions.DependencyInjection;
using Storage.UseCases.Facts.CreateFact;
using Storage.UseCases.Facts.GetFacts;
using Storage.UseCases.Tags.CreateTag;

namespace Storage.UseCases
{
    public static class StorageUseCasesExtension
    {
        public static IServiceCollection AddUseCases(this IServiceCollection services)
        {
            services.AddTransient<ICreateTagUseCase, CreateTagUseCase>();
            services.AddTransient<IGetFactsUseCase, GetFactsUseCase>();
            services.AddTransient<ICreateFactUseCase, CreateFactsUseCase>();
            
            return services;
        }
    }
}
