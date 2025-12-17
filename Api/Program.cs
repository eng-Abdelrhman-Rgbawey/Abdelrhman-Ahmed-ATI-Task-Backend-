using Api.Custom_Middlewares;
using Api.Data_Seeding;
using Api.Models;
using Api.Profiles;
using Api.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddCors(options =>
            {
                options.AddDefaultPolicy(policy =>
                    policy.AllowAnyOrigin()
                          .AllowAnyHeader()
                          .AllowAnyMethod());
            });

            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            // Database Context
            builder.Services.AddDbContext<AppDbContext>(op => op.UseLazyLoadingProxies()
                  .UseSqlServer(builder.Configuration.  GetConnectionString("TestTaskDb")));
            // Auto Mapper
            builder.Services.AddAutoMapper(cfg => { }, typeof(MappingProfile));

            // unit of work
            builder.Services.AddScoped<IUnitOfWork,Api.UnitOfWork.UnitOfWork>();

            var app = builder.Build();


            // Data Seeding
            using (var scope = app.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                DataSeeder.Seed(context);
            }



            app.UseMiddleware<ExceptionMiddleware>();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.UseSwaggerUI(op => op.SwaggerEndpoint("/openapi/v1.json", "v1"));

            }

            app.UseHttpsRedirection();
            app.UseCors();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
