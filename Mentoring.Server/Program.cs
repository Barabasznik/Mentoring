using Mentoring.Server.DataAcces;
using Mentoring.Application;
using Mentoring.Server.Middleware;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using NLog.Web;
using System.Text.Encodings.Web;
using System.Text.Unicode;

namespace Mentoring.Server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Logger
            builder.Host.UseNLog();

            // Kestrel do obsługi synchronizacji
            builder.WebHost.ConfigureKestrel(options =>
            {
                options.AllowSynchronousIO = true;
            });

            // JSON + polskie znaki
            builder.Services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(options =>
            {
                options.SerializerOptions.PropertyNamingPolicy = null;
                options.SerializerOptions.Encoder = JavaScriptEncoder.Create(UnicodeRanges.All);
            });

            // CORS – pozwalamy na wszystko
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", policy =>
                    policy.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader());
            });

            // JWT Bearer – Azure AD
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.Authority = "https://login.microsoftonline.com/9188040d-6c67-4c5b-b112-36a304b66dad";
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateAudience = true,
                        ValidAudience = "api://d237dddb-5618-430f-b117-9d85c9bdd599"
                    };
                });

            // Serwisy
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddApplication();
            builder.Services.RegisterDataAcces();
            builder.Services.AddControllers();

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

            // 🔐 Autoryzacja i autentykacja
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.MapFallbackToFile("/index.html");

            app.Run();
        }
    }
}
