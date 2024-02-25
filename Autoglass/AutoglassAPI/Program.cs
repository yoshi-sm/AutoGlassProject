using Autoglass.Aplicacao.Interfaces;
using Autoglass.Infra.Context;
using AutoglassAPI.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddServices();
builder.Services.AddDbContext<AutoglassContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("AutoglassConnectionString")));


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
