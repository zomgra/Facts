using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http.Headers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddAuthentication(opt =>
{
    opt.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    opt.DefaultChallengeScheme = "oidc";

})
    .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, conf =>
    {
        conf.ExpireTimeSpan = TimeSpan.FromMinutes(10);
    })
    .AddOpenIdConnect("oidc", options =>
    {
        options.Authority = builder.Configuration["ServicesUrl:IdentityApi"];
        options.GetClaimsFromUserInfoEndpoint = true;
        options.ClientId = "fact";
        options.ClientSecret = "secret";// Change in production;
        options.ResponseType = "code";
        options.TokenValidationParameters.NameClaimType = "Name";
        options.TokenValidationParameters.RoleClaimType = "Role";
        options.Scope.Add("fact");
        options.SaveTokens = true;
    });

builder.Services.AddHttpContextAccessor();

builder.Services.AddHttpClient("Storage", c =>
{
    c.BaseAddress = new Uri(builder.Configuration["ServicesUrl:StorageApi:Base"]);
    var serviceProvider = builder.Services.BuildServiceProvider();
    var httpContextAccessor = serviceProvider.GetService<IHttpContextAccessor>();
    var bearerToken = httpContextAccessor.HttpContext.Request
                         .Headers["Authorization"]
                         .FirstOrDefault(h => h.StartsWith("bearer ", StringComparison.InvariantCultureIgnoreCase));

    if (bearerToken != null)
        c.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", bearerToken);
});
builder.Services.AddHttpClient("Subscribe", c =>
{
    c.BaseAddress = new Uri(builder.Configuration["ServicesUrl:SubscribeApi:Base"]);

    var serviceProvider = builder.Services.BuildServiceProvider();
    var httpContextAccessor = serviceProvider.GetService<IHttpContextAccessor>();
    var bearerToken = httpContextAccessor.HttpContext.Request
                         .Headers["Authorization"]
                         .FirstOrDefault(h => h.StartsWith("bearer ", StringComparison.InvariantCultureIgnoreCase));
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
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(x =>
{
    x.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
    
    x.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{search:regex(a-ZА-Я)?}");
    
    x.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{search:regex(a-ZА-Я)?}/{page:int?}");

});

app.Run();
