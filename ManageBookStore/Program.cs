using ManageBookStore.IRepositories;
using ManageBookStore.Repositories;
using ManageBookStore.Models;
using ManageBookStore.Hubs;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace ManageBookStore
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
        .AddCookie(options =>
        {
            options.Cookie.Name = "CookieName";
            options.LoginPath = "/login";
            options.AccessDeniedPath = "/admin";
        });

            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("RequireAdminRole", policy =>
                {
                    policy.RequireRole("Admin");
                });
            });

            // Add services to the container.
            builder.Services.AddRazorPages();
            builder.Services.AddSignalR();

            builder.Services.AddDbContext<Prn221ManageStoreContext>(
    opt => builder.Configuration.GetConnectionString("NorthwindConStr")
    );
            builder.Services.AddScoped<IBook, BookRepository>();
            builder.Services.AddScoped<IAccount, AccountRepository>();
            builder.Services.AddScoped<ICategories, Categories>();
            builder.Services.AddScoped<IRole, Roles>();
            builder.Services.AddScoped<IOrderCartImport, OrderCartImports>();
            builder.Services.AddScoped<IOrderDetails, OrderDetailsRepository>();
            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseSession();

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();
            app.UseAuthentication();

            app.MapRazorPages();
            app.MapHub<ProductHub>("/productHub");


            app.Run();
        }
    }
}