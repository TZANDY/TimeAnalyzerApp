using Microsoft.AspNetCore.Authentication.Cookies;
using TimeAnalyzerApp.Services.Implementation;
using TimeAnalyzerApp.Services.Interface;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services
    .AddAuthentication(
    CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(
        options =>
        {
            options.LoginPath = "/Access/SignIn";
            options.LogoutPath = "/Access/Logout";
            options.AccessDeniedPath = "/Access/AccessDenied";
            options.ExpireTimeSpan = TimeSpan.FromMinutes(120);
            options.SlidingExpiration = true;
            options.Cookie.HttpOnly = true; // Protección contra XSS
        });

// Configuración de HttpClient con manejo de errores
var apiUrl = builder.Configuration["AppSettings:ApiUrl"];
if (string.IsNullOrEmpty(apiUrl))
{
    builder.Logging.AddConsole();
    builder.Logging.AddDebug();
}

builder.Services.AddHttpClient<ILoginService, LoginService>(
    client =>
    {
        client.BaseAddress = new Uri(apiUrl!);
    });

builder.Services.AddHttpClient<IRecordsService, RecordService>(
    client =>
    {
        client.BaseAddress = new Uri(apiUrl!);
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.Use(async (context, next) =>
{
    context.Response.Headers.CacheControl = "no-cache, no-store, must-revalidate";
    context.Response.Headers.Pragma = "no-cache";
    context.Response.Headers.Expires = "0";
    await next();
});
app.UseStaticFiles();


app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Access}/{action=SignIn}/{id?}");

app.Run();
