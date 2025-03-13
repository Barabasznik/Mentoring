using Mentoring.Server.DataAcces.Context;
using Mentoring.Server.DataAcces.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Mentoring.Server.DataAcces
{
    public static class DataAccesRegistry
    {
        public static void RegisterDataAcces(this IServiceCollection services)
        {
            services.AddScoped<IBookRepository, BookRepository>();
            services.AddDbContext<BooksDbContext>(options =>

                options.UseSqlServer(
                    "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=BooksDb;Integrated Security=True;"));


        }
    }
}
