using Microsoft.Extensions.DependencyInjection;
using System.Reflection;


namespace Mentoring.Application
{
    public static class ApplicationRegistry
    {

        public static void AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        }

    }
}
