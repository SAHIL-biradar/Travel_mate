
using CabRatingDll.Models;
using CabRatingDll.Services;
using CabRatingWebApi.DataServices;
using Microsoft.EntityFrameworkCore;

namespace CabRatingWebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddScoped<ICabRatingService, CabRatingService>();
            builder.Services.AddScoped<CabRatingDataServices>();
            // Add services to the container.
            builder.Services.AddDbContext<CabRatingDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("myConnection"));
            });

            builder.Services.AddCors(setUp =>
            {
                setUp.AddPolicy("cors", setUp =>
                {
                    setUp.AllowAnyHeader();
                    setUp.AllowAnyMethod();
                    setUp.AllowAnyOrigin();
                });
            });
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseCors("cors");
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
