using SportsStore.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing.Template;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SportsStoreProducts")));

builder.Services.AddTransient<IProductRepository, EFProductRepository>();
builder.Services.AddScoped<Cart>(sp => SessionCart.GetCart(sp));
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddMvc();

builder.WebHost.UseDefaultServiceProvider(options =>
    options.ValidateScopes = false);

builder.Services.AddMemoryCache();
builder.Services.AddSession();

var app = builder.Build();
app.UseDeveloperExceptionPage();
app.UseStatusCodePages();
app.UseStaticFiles();
app.UseRouting();
app.UseSession();

app.MapControllerRoute(
    name: null,
    pattern: "{category}/Page{productPage:int}",
    defaults: new {controller = "Product", action = "List" }
    );

app.MapControllerRoute(
    name: null,
    pattern: "Page{productPage:int}",
    defaults: new { controller = "Product", action = "List", productPage = 1 }
    );

app.MapControllerRoute(
    name: null,
    pattern: "{category}",
    defaults: new { controller = "Product", action = "List", productPage = 1 }
    );

app.MapControllerRoute(
    name: null,
    pattern: "",
    defaults: new { controller = "Product", action = "List", productPage = 1 }
    );

app.MapControllerRoute(
    name: null,
    pattern: "{controller}/{action}/{id?}"
    );

SeedData.EnsurePopulated(app);

app.Run();
