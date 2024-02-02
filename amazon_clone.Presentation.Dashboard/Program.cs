using amazon_clone.Application.Interfaces;
using amazon_clone.Domain.Models;
using amazon_clone.Domain.Users.Managers;
using amazon_clone.Domain.Users.Roles;
using amazon_clone.Infrastructure.DataAccess.Data.Contexts;
using amazon_clone.Infrastructure.DataAccess.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<DashboardDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DashboardDb"));
});

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("AppDb"));
});

builder.Services.AddIdentity<Administrator, AdministratorRole>()
    .AddEntityFrameworkStores<DashboardDbContext>()
    .AddSignInManager<AdministratorSignInManager>()
    .AddDefaultTokenProviders();


var assembly = typeof(IDashboardHomePageInformationManager).Assembly;

builder.Services.Scan(s => s.
        FromAssemblies(assembly)
        .AddClasses(c => c.AssignableTo<ITransientService>())
        .AsImplementedInterfaces()
        .WithTransientLifetime()
                    );

builder.Services.Scan(s => s.
        FromAssemblies(assembly)
        .AddClasses(c => c.AssignableTo<IScopedService>())
        .AsImplementedInterfaces()
        .WithScopedLifetime()
                    );

builder.Services.Scan(s => s
        .FromAssemblies(assembly)
        .AddClasses(c => c.AssignableTo<ISingletonService>())
        .AsImplementedInterfaces()
        .WithSingletonLifetime()
                    );


//builder.Services.TryAddSingleton<IEmailSender, EmailNotificationService>();

builder.Services.AddRazorPages();

builder.Services.TryAddScoped<DbContext, DbContext>();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.MapRazorPages();

app.UseAuthorization();
app.UseAuthentication();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=RegisterAndLogin}/{action=Index}/{id?}");

app.Run();
