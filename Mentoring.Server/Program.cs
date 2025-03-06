using Mentoring.Server.DataAcces;
using Scalar.AspNetCore;

namespace Mentoring.Server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);



            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAngular",
                    policy => policy.WithOrigins("https://localhost:54813") // ADRES FRONTENDU!
                        .AllowAnyMethod()
                        .AllowAnyHeader());
            });




            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddOpenApi();
            builder.Services.RegisterDataAcces();
            builder.Services.AddControllers();

            
            var app = builder.Build();
            app.UseCors("AllowAngular");
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseDefaultFiles();
            app.MapStaticAssets();
            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();
            app.MapFallbackToFile("/index.html");

            app.Run();
        }
    }
}