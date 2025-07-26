using CabBookingDll.Models;
using CabDll.Models;
using CabDll.Services;
using HotelBookingDll.Models;
using HotelDll.Models;
using HotelDll.Services;
using Microsoft.EntityFrameworkCore;
using SearchDll.Services;
using SearchWebApi.Services;

namespace SearchWebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddTransient<ISearchDataService, SearchDataService>();
            builder.Services.AddTransient<ISearchService, SearchService>();
            //builder.Services.AddTransient<ISearchService, SearchService>();
            //builder.Services.AddTransient<IHotelService, HotelService>();
            //builder.Services.AddTransient<ICabService, CabService>();

            builder.Services.AddCors(setUp =>
            {
                setUp.AddPolicy("cors", setUp =>
                {
                    setUp.AllowAnyHeader();
                    setUp.AllowAnyMethod();
                    setUp.AllowAnyOrigin();
                });
            });
            builder.Services.AddDbContext<HotelDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("myConnection"));
            });
            builder.Services.AddDbContext<CabDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("myConnection"));
            });
            builder.Services.AddDbContext<CabBookingDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("myConnection"));
            });
            builder.Services.AddDbContext<HotelBookingDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("myConnection"));
            });
            // Add services to the container.

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
