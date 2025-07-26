using AdminMvcUi.Services;

namespace AdminMvcUi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            
            builder.Services.AddHttpClient<IAdminService,AdminService>();
            builder.Services.AddHttpClient<IPackageProUIService,PackageProUIService>();
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=AdminUi}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
