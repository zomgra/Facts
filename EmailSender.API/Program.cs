using Common;
using EmailSender.API.Data;
using EmailSender.API.Listeners;
using EmailSender.UseCases.Tags;
using EmailSender.UseCases.Users;
using EmaiSender.Core.Inderfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using RabbitMQ.Client;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new()
    {
        Description = @"Enter 'Bearer' [space] and your token",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = JwtBearerDefaults.AuthenticationScheme,

    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = JwtBearerDefaults.AuthenticationScheme,
                },
                Scheme = "oauth2",
                Name="Bearer",
                In = ParameterLocation.Header,
            },
            new List<string>()
        }
    });
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
{
    options.Authority = builder.Configuration["Authentication:Authority"];
    options.TokenValidationParameters = new()
    {
        ValidateAudience = false,

    };
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("ApiScope", policy =>
    {
        policy.RequireAuthenticatedUser();
        policy.RequireClaim("scope", "fact");
    });
});
builder.Services.AddDbContextPool<EmailDbContext>(x => x.UseSqlite(builder.Configuration.GetConnectionString("DefaultString")));
builder.Services.AddTransient<IConnection>(c =>
{
    var factory = new ConnectionFactory()
    {
        Endpoint = new AmqpTcpEndpoint(),
        DispatchConsumersAsync = true

    };
    return factory.CreateConnection();
});


builder.Services.AddTransient<IEmailDbContext, EmailDbContext>();
builder.Services.AddCommonServices();

builder.Services.AddTransient<ISubscribeToTagUseCase, SubscribeToTagUseCase>();
builder.Services.AddTransient<IAddNewTagUseCase, AddNewTagUseCase>();
builder.Services.AddTransient<IGetSubscribedUserByNameTagUseCase, GetSubscribedUserByNameTagUseCase>();
builder.Services.AddTransient<IGetSubscribedUsersByTagIdUseCase, GetSubscribedUsersByTagIdUseCase>();

builder.Services.AddHostedService<TagEventListener>();
builder.Services.AddHostedService<FactEventListener>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
