using Mentoring.Server.DataAcces;
using System.Text.Encodings.Web;
using System.Text.Unicode;
using Mentoring.Application;
using Mentoring.Server.Middleware;
using NLog.Web;



namespace Mentoring.Server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            
            builder.Host.UseNLog();
            builder.WebHost.ConfigureKestrel(options =>
            {
                options.AllowSynchronousIO = true; 
            });
            
            builder.Services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(options =>
            {
                options.SerializerOptions.PropertyNamingPolicy = null;
                options.SerializerOptions.Encoder = JavaScriptEncoder.Create(UnicodeRanges.All);
            });
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll",
                    policy => policy.AllowAnyOrigin()
                                    .AllowAnyMethod()
                                    .AllowAnyHeader());
            });
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.RegisterDataAcces();
            builder.Services.AddControllers();
            builder.Services.AddApplication();
            var app = builder.Build();
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseMiddleware<ErrorHandlingMiddleware>();
            app.UseCors("AllowAll");
            app.UseDefaultFiles();
            app.UseStaticFiles();
            
            app.UseRouting();
            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();
            app.MapFallbackToFile("/index.html");
            app.Run();
        }
    }
}