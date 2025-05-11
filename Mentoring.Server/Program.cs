using Mentoring.Server.DataAcces;
using Mentoring.Application;
using Mentoring.Server.Middleware;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Identity.Web;
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

            // Kestrel – obsługa synchronizacji
            builder.WebHost.ConfigureKestrel(options =>
            {
                options.AllowSynchronousIO = true;
            });

            // JSON – polskie znaki
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

            // 🔐 Konfiguracja Azure AD B2C
            var azureAdSection = builder.Configuration.GetSection("AzureAdB2C");

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddMicrosoftIdentityWebApi(options =>
                    {
                        azureAdSection.Bind(options);

                        options.TokenValidationParameters.NameClaimType = "emails";

                        // Authority i ValidIssuer z konfiguracji
                        var tenantId = azureAdSection["TenantId"];
                        options.Authority = $"https://login.microsoftonline.com/{tenantId}";
                        options.TokenValidationParameters.ValidIssuer = options.Authority;

                        // 👇 ValidAudience z appsettings.json
                        options.TokenValidationParameters.ValidAudience = azureAdSection["ValidAudience"];
                    },
                    options => azureAdSection.Bind(options));



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

            app.UseAuthentication(); // 🔐
            app.UseAuthorization();  // 🔐

            app.MapControllers();
            app.MapFallbackToFile("/index.html");

            app.Run();
        }
    }
}
