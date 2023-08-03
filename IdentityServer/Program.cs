using IdentityServer;
using IdentityServer.Data;
using IdentityServer.Data.Interfaces;
using IdentityServer.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<ApplicationDbContext>(x => 
    x.UseSqlite(builder.Configuration.GetConnectionString("DefaultString")));

builder.Services.AddIdentity<ApplicationUser, ApplicationRole>(c =>
{
    c.SignIn.RequireConfirmedPhoneNumber = false;
    c.Password.RequireNonAlphanumeric = false;
    c.Password.RequiredLength = 5;
    c.Password.RequireUppercase = false;
})
    .AddRoles<ApplicationRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddScoped<IDbInitilizer, DbInitiliazer>();

builder.Services.AddIdentityServer(options =>
{
    options.Events.RaiseFailureEvents = true;
    options.Events.RaiseInformationEvents = true;
    options.Events.RaiseErrorEvents = true;
    options.Events.RaiseSuccessEvents = true;

    options.EmitStaticAudienceClaim = true;
})
    .AddAspNetIdentity<ApplicationUser>()
    .AddInMemoryApiScopes(IdentityConfiguration.GetApiScopes())
    .AddInMemoryIdentityResources(IdentityConfiguration.GetIdentityResources())
    .AddInMemoryClients(IdentityConfiguration.GetClients())
    .AddDeveloperSigningCredential();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");

    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

//app.UseAuthentication();
app.UseIdentityServer();
app.UseAuthorization();
var scope = app.Services.CreateScope();
var initializer = scope.ServiceProvider.GetRequiredService<IDbInitilizer>();
await initializer.Initialize();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
