using Microsoft.EntityFrameworkCore;
using APIBLOG.Models;
using APIBLOG.Services.Interfaces;
using APIBLOG.Controllers;
using APIBLOG.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSqlServer<ApiblogContext>(builder.Configuration.GetConnectionString("conexion"));


builder.Services.AddScoped<IUsuarioService,UsuarioService>();
builder.Services.AddScoped<ICategoriaService,CategoriaService>();

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
