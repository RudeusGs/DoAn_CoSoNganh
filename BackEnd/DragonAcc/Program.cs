using DragonAcc.Infrastructure;
using DragonAcc.Service;
using DragonAcc.Service.Services;
using Microsoft.OpenApi.Models;

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
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options =>
            {
                //var commitHash = Configuration["LastedCommitHash"];
                //var commitMsg = Configuration["LastedCommitMsg"];
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "DRAGONACC 2024 WEB API",
                    Version = "v1",
                    // Description = $"Commit hash: {commitHash} <br /> Commit message: {commitMsg}"
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

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAnyCorsPolicy",
                    builder => builder
                    .WithOrigins("*")
                    .AllowAnyMethod()
                    .AllowAnyHeader());
            });
            builder.Services.AddApplicationServices();
            builder.Services.AddInfrastructureServices(builder.Configuration);
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseCors("AllowAnyCorsPolicy");
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
