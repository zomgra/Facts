using Microsoft.AspNetCore.Authentication.Cookies;

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


builder.Services.AddHttpClient("Storage", client =>
{
    client.BaseAddress = new Uri(builder.Configuration["ServicesUrl:StorageApi:Base"]);
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

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
