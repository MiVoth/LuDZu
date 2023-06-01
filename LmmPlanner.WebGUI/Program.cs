using AutoMapper;
using LmmPlanner.Business.Services;
using LmmPlanner.Data;
using LmmPlanner.Data.Statistics;
using LmmPlanner.Entities.Interfaces;
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
        string s89Path = builder.Configuration.GetSection("AppSettings")["S89-Path"];
        string exportPath = builder.Configuration.GetSection("AppSettings")["Export-Path"];
        builder.Services.AddSingleton<AppSettings>(sp => appSettings);
        builder.Services.AddScoped<MyContext>();
        builder.Services.AddScoped<MyWriterContext>();
        builder.Services.AddScoped<DataRepo>();
        builder.Services.AddScoped<ScheduleRepo>();
        builder.Services.AddScoped<IChairStatisticsRepo, ChairStatisticsRepo>();
        builder.Services.AddScoped<IDataRepo, DataRepo>();
        builder.Services.AddScoped<ISettingsRepo, SettingsRepo>();
        builder.Services.AddScoped<IEditorRepo, EditorRepo>();
        builder.Services.AddScoped<IScheduleRepo, ScheduleRepo>();
        builder.Services.AddScoped<IExportService, ExportService>();
        builder.Services.AddScoped<IFormFillerService>(a =>
        new LmmPlanner.LmmFormFiller.FormFillerService(appSettings.LmmConnectionString, s89Path, exportPath));
        var mapperConfig = new MapperConfiguration(mc =>
             {
                 mc.AddProfile(new MappingProfile());
             });

        IMapper mapper = mapperConfig.CreateMapper();
        builder.Services.AddSingleton(mapper);
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