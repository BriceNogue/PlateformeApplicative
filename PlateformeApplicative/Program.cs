using Domain.Entities;
using Domain.Repositories;
using Microsoft.AspNetCore.Identity;
using System;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Starting ****************
builder.Services.AddDbContext<DataContext>();
// Ad Ientity and JWT authentication

// Identity
builder.Services.AddIdentity<Utilisateur, TypeE>()
    .AddEntityFrameworkStores<DataContext>()
    .AddSignInManager()
    .AddRoles<TypeE>();


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
