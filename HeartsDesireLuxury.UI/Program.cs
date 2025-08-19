using Entities.DB;
using Entities.IdentityEntity;
using HeartsDesireLuxury.Core.Domain.RepositoryContracts;
using HeartsDesireLuxury.Core.ServiceContracts;
using HeartsDesireLuxury.Core.Services;
using HeartsDesireLuxury.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Repositories;
using RepositoryContracts;
using ServiceContracts;
using Services;

var builder = WebApplication.CreateBuilder(args);

// ---------------------------
// Add Services to the Container
// ---------------------------
builder.Services.AddControllersWithViews();

// Repository Dependencies
builder.Services.AddScoped<IProductCategoryRepository, ProductCategoryRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IInventoryRepository, InventoryRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();

// Services
builder.Services.AddScoped<IProductGetterServices, ProductGetterService>();
builder.Services.AddScoped<IProductCategoryAdderService, ProductCategoryAdderServices>();
builder.Services.AddScoped<IProductCategoryGetterService, ProductCategoryGetterServices>();
builder.Services.AddScoped<IProductDeleteServices, ProductDeleteService>();
builder.Services.AddScoped<IProductAdderServices, ProductAdderService>();
builder.Services.AddScoped<IProductUpdateServices, ProductUpdateService>();
builder.Services.AddScoped<IInventoryAdderService, InventoryAdderService>();
builder.Services.AddScoped<IOrderGetService, OrderAdderService>();

// EF DbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
});

// Identity
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


//builder.Services.AddAuthentication(options =>
//{
//    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
//})
//.AddCookie(options =>
//{
//    options.LoginPath = "/Account/Login";
//    options.AccessDeniedPath = "/Account/AccessDenied";
//})
//.AddGoogle(options =>
//{
//    options.ClientId = builder.Configuration["Authentication:Google:ClientId"];
//    options.ClientSecret = builder.Configuration["Authentication:Google:ClientSecret"];
//});


builder.Services.AddAuthorization(options =>
{
    options.FallbackPolicy = new AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser()
        .Build();
});


var app = builder.Build();


if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    
    endpoints.MapControllerRoute(
        name: "areas",
        pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

   
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
});

app.Run();
