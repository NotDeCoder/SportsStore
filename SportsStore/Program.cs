using SportsStore.Models;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddMvc();
builder.Services.AddTransient<IProductRepository, FakeProductRepository>();

var app = builder.Build();
app.UseDeveloperExceptionPage();
app.UseStatusCodePages();
app.UseStaticFiles();
app.UseRouting();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Product}/{action=List}/{id?}");

app.Run();
