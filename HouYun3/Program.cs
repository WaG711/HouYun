using Microsoft.EntityFrameworkCore;
using HouYun3.Data;
using HouYun3.IRepositories;
using HouYun3.Repositories;
using HouYun3.Models;
using Microsoft.AspNetCore.Identity;
using System.Data;
using System;
using static HouYun3.Models.User;

namespace HouYun3
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddDbContext<HouYun3Context>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("HouYun3Context") ?? throw new InvalidOperationException("Connection string 'HouYun3Context' not found.")));

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddScoped<IVideoRepository, VideoRepository>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
            builder.Services.AddScoped<ICommentRepository, CommentRepository>();
            builder.Services.AddScoped<ILikeRepository, LikeRepository>();
            builder.Services.AddScoped<INotificationRepository, NotificationRepository>();
            builder.Services.AddScoped<ISearchHistoryRepository, SearchHistoryRepository>();
            builder.Services.AddScoped<IViewRepository, ViewRepository>();
            builder.Services.AddScoped<IWatchLaterRepository, WatchLaterRepository>();
            builder.Services.AddScoped<IWatchHistoryRepository, WatchHistoryRepository>();

            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            builder.Services.AddIdentity<User, IdentityRole>(opts=>
            {
                opts.Password.RequiredLength = 6;   // минимальная длина
                opts.Password.RequireNonAlphanumeric = false;   // требуются ли не алфавитно-цифровые символы
                opts.Password.RequireLowercase = false; // требуются ли символы в нижнем регистре
                opts.Password.RequireUppercase = true; // требуются ли символы в верхнем регистре
                opts.Password.RequireDigit = true; // требуются ли цифры
                opts.User.RequireUniqueEmail = true;    // уникальный email
            }).AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();
            builder.Services.AddControllersWithViews();
            builder.Services.AddRazorPages();

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
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
