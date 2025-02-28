using Mentoring.Server.DataAcces;
using Scalar.AspNetCore;

namespace Mentoring.Server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddOpenApi();
            // Add services to the container.
            builder.Services.RegisterDataAcces();

            builder.Services.AddControllers();

            var app = builder.Build();
            if (app.Environment.IsDevelopment())
            { app.MapOpenApi(); app.MapScalarApiReference(); }
            app.UseDefaultFiles();
            app.MapStaticAssets();

            // Configure the HTTP request pipeline.

            app.UseHttpsRedirection();

            app.UseAuthorization();

            //TEST?//
            app.MapControllers();

            app.MapFallbackToFile("/index.html");

            app.Run();
        }
    }
}
