using ClickaholicWebApp.BLL.ServiceLayer.Services.Interfaces;
using ClickaholicWebApp.BLL.ServiceLayer.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using ClickaholicWebApp.BLL.Repository;
using ClickaholicWebApp.DAL.Repository;
using ClickaholicWebApp.DAL.DataBase;

namespace ClickaholicWebApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var services = builder.Services;
            // Add services to the container.
            services.AddControllersWithViews();

            services.AddDbContext<DataContext>(o => { o.UseSqlServer(builder.Configuration.GetConnectionString("Clickaholic"),
                b => b.MigrationsAssembly("ClickaholicWebApp")); });


            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(o =>
                {
                    o.Cookie.Name = "Identity";
                    o.ExpireTimeSpan = TimeSpan.FromMinutes(50);
                    o.LoginPath = "/auth/authenticate";
                    o.AccessDeniedPath = "/admin/auth/login";
                });


            services.AddHttpContextAccessor();
            services.AddMvc();
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAboutService, AboutService>();
            services.AddScoped<ICommentService, CommentService>();
            services.AddScoped<IContactService, ContactService>();
            services.AddScoped<IPhotographService, PhotographService>();
            services.AddScoped<ISliderService, SliderService>();
            services.AddSingleton<IFileService, FileService>();
            services.AddScoped<IBlogService, BlogService>();
            services.AddScoped<IHomeService, HomeService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{area=exists}/{controller=home}/{action=index}");

            app.Run();
        }
    }
}