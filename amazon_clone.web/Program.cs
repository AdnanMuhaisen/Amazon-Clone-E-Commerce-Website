using amazon_clone.DataAccess.Data;
using amazon_clone.DataAccess.Repositories;
using amazon_clone.Models.Models;
using amazon_clone.Models.Users.Managers;
using amazon_clone.Models.Users.Roles;
using amazon_clone.Services.Notification_Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Stripe;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
    //options.AddInterceptors(new SoftDeleteInterceptor(),
    //    new UpdateCreationDetailsInterceptor());
    options.LogTo(l =>
    {
        System.IO.File.WriteAllText(@"C:\amazon_clone\amazon_clone.DataAccess\Data\Logged-Queries.txt", l);
    }, LogLevel.Information);
});

builder.Services.AddIdentity<CustomerApplicationUser, CustomerRole>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();


builder.Services.TryAddScoped<IEmailSender, EmailNotificationService>();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

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

//customer1@gmail.com
//asdfA1!

//customer2
//asdfA2@


app.Run();
