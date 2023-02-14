using LmmPlanner.Data;
using LmmPlanner.Data.Statistics;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace LmmPlanner.WebGUI;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddRazorPages();

        // builder.Services.Configure<AppSettings>();
        AppSettings appSettings = new()
        {
            LmmConnectionString = builder.Configuration.GetSection("AppSettings")["LmmConnectionString"]
        };
        builder.Services.AddSingleton<AppSettings>(sp => appSettings);
        builder.Services.AddScoped<MyContext>();
        builder.Services.AddScoped<DataRepo>();
        builder.Services.AddScoped<ScheduleRepo>();
        builder.Services.AddScoped<IChairStatisticsRepo, ChairStatisticsRepo>();
        builder.Services.AddScoped<IDataRepo, DataRepo>();
        builder.Services.AddScoped<ISettingsRepo, SettingsRepo>();
        builder.Services.AddScoped<IEditorRepo, EditorRepo>();
        builder.Services.AddScoped<IScheduleRepo, ScheduleRepo>();
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

        app.UseAuthorization();

        app.MapRazorPages();

        app.Run();
    }
}