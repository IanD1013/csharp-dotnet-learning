using Authentication_Project.CustomAuthHandler;
using Microsoft.AspNetCore.Mvc.Razor;

var builder = WebApplication.CreateBuilder(args);

//Support features folder structure
builder.Services.Configure<RazorViewEngineOptions>(rvo =>
{
    rvo.ViewLocationFormats.Add("~/Features/{1}/{0}.cshtml");
    rvo.ViewLocationFormats.Add("~/Views/Shared/{0}.cshtml");
});

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddAuthentication(o => // add authentication middleware
    {
        // Customize the middleware
        o.DefaultScheme = "handler1";
    })
    .AddCustomAuth(authenticationScheme: "handler1", displayName: "Google SignIn",
        configureOption: o =>
        {
            o.LoginPath = "/user/login";
            o.CookieName = "AuthCookie1";
        })
    .AddCustomAuth(authenticationScheme: "handler2", displayName: "Facebook SignIn",
        configureOption: o =>
        {
            o.LoginPath = "/user/login";
            o.CookieName = "AuthCookie2";
        });

var app = builder.Build();

// Configure the HTTP request pipeline.


if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

// Serve static files (CSS/JS) before routing so assets are resolved correctly
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication(); // add authentication middleware to the pipeline
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


app.Run();