﻿using Microsoft.EntityFrameworkCore;
using HouYun.Data;
using HouYun.IRepositories;
using HouYun.Repositories;
using Microsoft.AspNetCore.Identity;
using HouYun.Models;
using Microsoft.Data.SqlClient;
using HouYun.Controllers;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.AspNetCore.Http.Features;


namespace HouYun
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            builder.Services.AddIdentity<User, IdentityRole>(opts =>
            {
                opts.Password.RequiredLength = 6;
                opts.Password.RequireNonAlphanumeric = false;
                opts.Password.RequireLowercase = false;
                opts.Password.RequireUppercase = true;
                opts.Password.RequireDigit = true;
                opts.User.RequireUniqueEmail = true;
            }).AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();
   
            builder.Services.AddControllersWithViews();
            builder.Services.AddRazorPages();

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
            builder.Services.AddScoped<IChannelRepository, ChannelRepository>();
            builder.Services.AddScoped<ISubscriptionRepository, SubscriptionRepository>();
            builder.Services.AddControllersWithViews(options =>
            {
                options.Filters.Add(typeof(CustomAuthorizationFilter));
            });

            builder.Services.ConfigureApplicationCookie(options =>
            {
                options.AccessDeniedPath = "/Login";
                options.LoginPath = "/Login";
            });

            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(options =>
            {
                options.Cookie.Name = "HouYunCookie";
                options.Cookie.HttpOnly = true;
            });

            builder.Services.Configure<FormOptions>(options =>
            {
                options.MultipartBodyLengthLimit = 51L * 1024 * 1024 * 1024;
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Video}/{action=Index}/{id?}");

            app.MapControllerRoute(
                name: "Notifications",
                pattern: "Notifications",
                defaults: new { controller = "Notifications", action = "Index" });

            app.MapControllerRoute(
                name: "AddView",
                pattern: "View/AddView/{videoId}",
                defaults: new { controller = "View", action = "AddView" });

            app.MapControllerRoute(
                name: "AddToWatchHistory",
                pattern: "WatchHistory/AddToWatchHistory/{videoId}",
                defaults: new { controller = "WatchHistory", action = "AddToWatchHistory" });

            app.MapControllerRoute(
                name: "addComment",
                pattern: "Comment/AddComment",
                defaults: new { controller = "Comment", action = "AddComment" });

            app.MapControllerRoute(
                name: "search",
                pattern: "Video/Search",
                defaults: new { controller = "Search", action = "Search" });

            app.MapControllerRoute(
                 name: "channel",
                 pattern: "Channel/{channelName}",
                 defaults: new { controller = "Channel", action = "Index" });

            app.MapControllerRoute(
                name: "search",
                pattern: "Search/SearchResult/{searchTerm?}",
                defaults: new { controller = "Search", action = "SearchResult" }
            );

            app.MapControllerRoute(
                name: "login",
                pattern: "login",
                defaults: new { controller = "Login" }
            );

            app.MapControllerRoute(
                name: "registration",
                pattern: "registration",
                defaults: new { controller = "Registration"}
            );

            app.Run();
        }
    }
}
