

using Microsoft.EntityFrameworkCore;
using TiendaServicios.Api.Libro.Persistencia;
using System.Configuration;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using MediatR;
//using TiendaServicios.Api.Libro.Aplicacion;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore.SqlServer;



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


//esto es de internet para netcore 6.0 en adelante ya que no se puede definir en el setup.cs
//https://stackoverflow.com/questions/39083372/how-to-read-connection-string-in-net-core


//builder.Services.AddControllers().AddFluentValidation(cfg => cfg.RegisterValidatorsFromAssemblyContaining<Nuevo>());

var connectionString = builder.Configuration.GetConnectionString("ConexionDB");
builder.Services.AddDbContext<ContextoLibreria>(options => options.UseSqlServer(connectionString));
//builder.Services.AddMediatR(typeof(Nuevo.Manejador).Assembly);

//builder.Services.AddAutoMapper(typeof(Consulta.ListaAutor.Manejador));


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
