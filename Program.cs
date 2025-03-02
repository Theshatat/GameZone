
namespace GameZone
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            var ConnectionString = builder.Configuration.GetConnectionString(name: "DefaultConnection")
                ?? throw new InvalidOperationException(message: "No Connection String Was Found");
            builder.Services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(ConnectionString);
            });
            builder.Services.AddScoped<IDeviceAppServices, DeviceAppServices>();
            builder.Services.AddScoped<ICategoriesAppServices, CategoriesAppServices>();
            builder.Services.AddScoped<IGameServices, GameServices>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseAuthorization();

            app.MapStaticAssets();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}")
                .WithStaticAssets();

            app.Run();
        }
    }
}
