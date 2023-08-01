using Common;
using Microsoft.EntityFrameworkCore;
using Storage.API.Data;
using Storage.Core.Mapper;
using Storage.UseCases;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContextPool<FactsDbContext>(options => 
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultString")));

builder.Services.AddAutoMapper(typeof(ApplicationMapper).Assembly);
builder.Services.AddTransient<IFactDbContext, FactsDbContext>();

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

app.UseAuthorization();

app.MapControllers();

app.Run();
