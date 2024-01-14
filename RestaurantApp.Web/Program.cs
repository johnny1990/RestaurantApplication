using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RestaurantApp.Web.Data;
using RestaurantApp.Web.Helpers;
using RestaurantApp.Web.Models;
using RestaurantApp.Web.Models.IP;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;



    var builder = WebApplication.CreateBuilder(args);

    // Add services to the container.
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
    builder.Services.AddDatabaseDeveloperPageExceptionFilter();

    builder.Services.AddIdentity<IdentityUser, IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

    builder.Services.AddDbContext<RestaurantDbContext>(
               options => options.UseSqlServer(builder.Configuration.GetConnectionString("RestaurantDbConnection")));

    builder.Services.AddControllersWithViews();
    builder.Services.AddRazorPages();
    builder.Services.AddSession();

    builder.Services.AddTransient<ExceptionMiddlewareAPI>();

builder.Services.Configure<CookiePolicyOptions>(options =>
    {
    // This lambda determines whether user consent for non-essential 
    // cookies is needed for a given request.
    options.CheckConsentNeeded = context => true;
    // requires using Microsoft.AspNetCore.Http;
    options.MinimumSameSitePolicy = SameSiteMode.None;
    });

    builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");
    builder.Services.AddMvc()
    .AddViewLocalization(Microsoft.AspNetCore.Mvc.Razor.LanguageViewLocationExpanderFormat.Suffix)
    .AddDataAnnotationsLocalization();

builder.Services.Configure<RequestLocalizationOptions>(options =>
    {
    var cultures = new List<CultureInfo> {
                new CultureInfo("en"),
                new CultureInfo("ro")
    };
    options.DefaultRequestCulture = new Microsoft.AspNetCore.Localization.RequestCulture("en");
    options.SupportedCultures = cultures;
    options.SupportedUICultures = cultures;
    });

    builder.Services.AddScoped<TraceIP>();


    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
    app.UseMigrationsEndPoint();
    }
    else
    {
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
    }

    app.UseHttpsRedirection();
    app.UseStaticFiles();

    app.UseRouting();

    app.UseAuthentication();
    app.UseAuthorization();
    app.UseSession();
    app.UseCookiePolicy();

    app.UseRequestLocalization(app.Services.GetRequiredService<IOptions<RequestLocalizationOptions>>().Value);

    app.UseDeveloperExceptionPage();
    app.UseMiddleware<ExceptionMiddlewareAPI>();

app.UseEndpoints(endpoints =>
    {
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
    endpoints.MapRazorPages();
    });

    app.Run();
