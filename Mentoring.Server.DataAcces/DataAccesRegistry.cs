using Mentoring.Server.DataAcces.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Mentoring.Server.DataAcces
{
    public static class DataAccesRegistry
    {
        public static void RegisterDataAcces(this IServiceCollection services)
        {
            services.AddScoped<IBookRepository, BookRepository>();
        }
    }
}
