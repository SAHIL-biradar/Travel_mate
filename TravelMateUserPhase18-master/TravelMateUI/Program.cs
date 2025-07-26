using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;

using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using TravelMate.Services;
using TravelMate2.Services;
using TravelMateUI.Services;
using TravelMateUI.Services.Ratings;


namespace TravelMateUI
{
    public class Program
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<LocalStorageService>();
            services.AddScoped<AuthService>();
            services.AddScoped<UserInfoService>();
            services.AddTransient<IUserUIService, UserUIService>();
            services.AddTransient<IHotelUIService, HotelUIService>();
            services.AddTransient<ICabUIService, CabUIService>();
            services.AddTransient<IAdminUIService, AdminUIService>();
            services.AddAuthorizationCore();

            services.AddAuthorizationCore(options =>
            {
                options.AddPolicy("RegularUser", policy => policy.RequireRole("Regular"));
                options.AddPolicy("HotelOwner", policy => policy.RequireRole("hotel_owner"));
                options.AddPolicy("DriverClient", policy => policy.RequireRole("driver_client"));
            });
            services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();
            services.AddScoped<ICustomAuthenticationStateProvider, CustomAuthenticationStateProvider>();
            services.AddTransient<ICabBookingUIService, CabBookingUIService>();
            services.AddTransient<ISearchUIService, SearchUIService>();
            services.AddTransient<IRoomUIService, RoomUIService>();
            services.AddTransient<IHotelBookingUIService, HotelBookingUIService>();
            services.AddTransient<IWishListUIService, WishListUIService>();
            services.AddTransient<IPackageProUIService, PackageProUIService>();
            services.AddTransient<IHotelRatingUIService, HotelRatingUIService>();
            services.AddTransient<ICabRatingUIService, CabRatingUIService>();

            
        }
        public static IConfigurationRoot Configuration { get; set; }
		public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            builder.RootComponents.Add<HeadOutlet>("head::after");
			Configuration = builder.Configuration;

            builder.Services.AddTransient(sp => new HttpClient {});       

            ConfigureServices(builder.Services);

            await builder.Build().RunAsync();
        }
    }
}
