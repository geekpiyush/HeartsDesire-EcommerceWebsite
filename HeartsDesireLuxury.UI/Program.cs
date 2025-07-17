using Entities.DB;
using Entities.IdentityEntity;
using HeartsDesireLuxury.Core.Domain.RepositoryContracts;
using HeartsDesireLuxury.Core.ServiceContracts;
using HeartsDesireLuxury.Core.Services;
using HeartsDesireLuxury.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Repositories;
using RepositoryContracts;
using ServiceContracts;
using Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//IOC Container
builder.Services.AddScoped<IProductCategoryRepository, ProductCategoryRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IInventoryRepository,InventoryRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();

builder.Services.AddScoped<IProductGetterServices, ProductGetterService>();
builder.Services.AddScoped<IProductCategoryAdderService, ProductCategoryAdderServices>();
builder.Services.AddScoped<IProductCategoryGetterService, ProductCategoryGetterServices>();
builder.Services.AddScoped<IProductDeleteServices, ProductDeleteService>();
builder.Services.AddScoped<IProductAdderServices, ProductAdderService>();
builder.Services.AddScoped<IProductUpdateServices, ProductUpdateService>();
builder.Services.AddScoped<IInventoryAdderService,InventoryAdderService>();
builder.Services.AddScoped<IOrderGetService, OrderAdderService>();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
});

builder.Services.AddIdentity<ApplicationUser, ApplicationUserRole>(options =>
{
    options.Password.RequiredLength = 5;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireLowercase = true;
    options.Password.RequireDigit = false;
    options.Password.RequiredUniqueChars = 2;

})
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders()
    .AddUserStore<UserStore<ApplicationUser, ApplicationUserRole, ApplicationDbContext, Guid>>()
    .AddRoleStore<RoleStore<ApplicationUserRole, ApplicationDbContext, Guid>>();

builder.Services.AddAuthorization(options =>
{
    options.FallbackPolicy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
});
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Account/Login";
});

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login";
        options.AccessDeniedPath = "/Account/AccessDenied";
    });

builder.Services.AddAuthorization();
var app = builder.Build();

//builder.Services.AddDbContext<ApplicationDbContext>(optins=>
//{
//    optins.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
//});


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
app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute
    (
        name: "areas",
        pattern: "{area:exists}/{controller=home}/{action=Index}"

        );

    endpoints.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
});


app.Run();
