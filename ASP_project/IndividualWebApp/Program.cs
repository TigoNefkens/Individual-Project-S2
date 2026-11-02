using BLL.Interfaces;
using DAL.Data;
using DAL.Interfaces;
using Microsoft.AspNetCore.Authentication.Cookies;
using BLL.Services;
using BLL.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(90);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddAuthorization();

builder.Services.AddHttpContextAccessor();
// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddScoped<IUserService, UserServices>();
builder.Services.AddScoped<IUserRepo, UserRepo>();
builder.Services.AddScoped<ITabService, TabServices>();
builder.Services.AddScoped<ITabRepo, TabRepo>();
builder.Services.AddScoped<ICategoryRepo, CategoryRepo>();
builder.Services.AddScoped<ICategoryServices, CategoryServices>();
builder.Services.AddScoped<IRecommendationServices, RecommendationServices>();

var app = builder.Build();


app.UseSession();

app.UseAuthentication();

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
