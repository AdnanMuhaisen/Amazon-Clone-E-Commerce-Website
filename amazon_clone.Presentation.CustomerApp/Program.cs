using amazon_clone.Application.Interfaces;
using amazon_clone.Domain.Models;
using amazon_clone.Domain.Users.Roles;
using amazon_clone.Infrastructure.DataAccess.Data.Contexts;
using amazon_clone.Infrastructure.DataAccess.Interceptors;
using amazon_clone.Infrastructure.DataAccess.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Stripe;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


//builder.Services.AddAuthentication()
//    .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("Basic", null);

//builder.Services
//    .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
//    .AddCookie(options =>
//    {
//        options.LoginPath = "/LoginAndRegister/Index";
//        options.ExpireTimeSpan = TimeSpan.FromMinutes(20);
//        options.AccessDeniedPath = "/";
//    });

//builder.Services.AddAuthorization(options =>
//{
//    options.AddPolicy("Cookies", policy =>
//    {
//        policy.RequireAuthenticatedUser();
//    });
//});

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
    options.AddInterceptors(new SoftDeleteInterceptor());
});

//builder.Services.AddDbContext<DashboardDbContext>(options =>
//{
//    options.UseSqlServer(builder.Configuration.GetConnectionString("DashboardDb"));
//});

builder.Services.AddScoped<DbContext, AppDbContext>();

builder.Services.AddIdentity<CustomerApplicationUser, CustomerRole>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

//builder.Services.AddSingleton<IEmailSender, EmailNotificationService>();

var assembly = typeof(IOrderProcessingService).Assembly;

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

builder.Services.AddRazorPages();

// stripe configuration - include api settings
StripeConfiguration.ApiKey = builder.Configuration.GetSection("Stripe:SecretKey").Get<string>();

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

app.UseAuthorization();
app.UseAuthentication();

app.MapRazorPages();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=LoginAndRegister}/{action=Index}/{id?}");

app.Run();
