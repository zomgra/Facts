using Common;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using RabbitMQ.Client;
using Storage.API.Data;
using Storage.API.Publisher;
using Storage.Core.Interfaces;
using Storage.Core.Mapper;
using Storage.UseCases;
using Storage.UseCases.Facts.GetListByTagId;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
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

builder.Services.AddDbContextPool<FactsDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultString")));

builder.Services.AddAutoMapper(typeof(ApplicationMapper).Assembly);
builder.Services.AddTransient<IFactDbContext, FactsDbContext>();

builder.Services.AddTransient<IConnection>(c =>
{
    var factory = new ConnectionFactory()
    {
        Endpoint = new AmqpTcpEndpoint(),
        DispatchConsumersAsync = true,
    };
    return factory.CreateConnection();
});

builder.Services.AddTransient<INewFactPublisher, NewFactPublisher>();
builder.Services.AddTransient<INewTagPublisher, NewTagPublisher>();
builder.Services.AddTransient<IGetListByTagIdUseCase, GetListByTagIdUseCase>();

builder.Services.AddUseCases();
builder.Services.AddCommonServices();

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
