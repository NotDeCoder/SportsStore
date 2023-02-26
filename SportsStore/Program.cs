using SportsStore.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SportsStoreProducts")));

builder.Services.AddTransient<IProductRepository, EFProductRepository>();
builder.Services.AddMvc();

builder.WebHost.UseDefaultServiceProvider(options =>
    options.ValidateScopes = false);

var app = builder.Build();
app.UseDeveloperExceptionPage();
app.UseStatusCodePages();
app.UseStaticFiles();
app.UseRouting();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Product}/{action=List}/{id?}");

SeedData.EnsurePopulated(app);

app.Run();
