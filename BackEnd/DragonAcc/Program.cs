using DragonAcc.Infrastructure;
using DragonAcc.Service;
using DragonAcc.Service.Services;
using Microsoft.OpenApi.Models;
using Microsoft.Extensions.FileProviders;
using System.IO;

namespace DragonAcc
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();
            builder.Services.AddHostedService<ExpiredGiftCleanupService>();

            // Configure Swagger/OpenAPI
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "DRAGONACC 2024 WEB API",
                    Version = "v1",
                });

                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Enter 'Bearer' [space] and then your valid token in the text input below.\r\n\r\nExample: \"Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9\"",
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] {}
                    }
                });

                options.CustomSchemaIds(type => type.ToString());
            });

            // Configure CORS
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAnyCorsPolicy",
                    builder => builder
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
            });

            // Add application and infrastructure services
            builder.Services.AddApplicationServices();
            builder.Services.AddInfrastructureServices(builder.Configuration);
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            // Phục vụ các tệp tĩnh từ wwwroot
            app.UseStaticFiles();

            // Phục vụ các tệp tĩnh từ thư mục 'C:\Host\public' với RequestPath '/public'
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(@"C:\Host\public"),
                RequestPath = "/public"
            });

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseCors("AllowAnyCorsPolicy");
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
