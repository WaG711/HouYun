using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using HouYun3.Data;
using HouYun2.IRepositories;
using HouYun2.Repositories;
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
