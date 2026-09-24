var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddControllersWithViews();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

//app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthorization();

//// MVC routing
//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller=Home}/{action=Index}");   ye publick hai 

////app.MapControllerRoute(
////    name: "default",
////    pattern: "{controller=Admin}/{action=Dashboard}");
//// API controllers   ye admin hai 
///

app.Use(async (context, next) =>
{
    if (context.Request.Path == "/" &&
        context.Connection.LocalPort == 5017)
    {
        context.Response.Redirect("/Admin/Dashboard");
        return;
    }

    await next();
});

app.MapControllerRoute(
    name: "admin",
    pattern: "Admin/{action=Dashboard}/{id?}",
    defaults: new { controller = "Admin" });

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapControllers();

app.Run();


