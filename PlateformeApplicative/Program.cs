using Domain.Entities;
using Domain.Repositories;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Shareds.Interfaces;
using Swashbuckle.AspNetCore.Filters;
using System;
using System.Text;
using Shareds.Hubs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen(); // Ajouter plus bas avec les configations pour securiser swagger

// Starting ****************
builder.Services.AddDbContext<DataContext>(); //Injection de dependance pour le context de données DataContext

// Ajout Ientity et JWT authentication

// JWT
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateIssuerSigningKey = true,
        ValidateLifetime = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!))
    };
});

//builder.Services.AddAuthorization();

// Identity
builder.Services.AddIdentity<Utilisateur, TypeE>()
    .AddEntityFrameworkStores<DataContext>()
    .AddRoles<TypeE>();

// Sucurisation de swagger ui
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });

    options.OperationFilter<SecurityRequirementsOperationFilter>();
});

builder.Services.AddScoped<UserRepository>();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<UEService>();
builder.Services.AddScoped<UERepository>();
builder.Services.AddScoped<TypeRepository>();
builder.Services.AddScoped<TypeService>();
builder.Services.AddScoped<PosteService>();
builder.Services.AddScoped<SalleService>();
builder.Services.AddScoped<SalleRepository>();
builder.Services.AddScoped<EtablissementService>();
builder.Services.AddScoped<EtablissementRepository>();
builder.Services.AddScoped<PosteRepository>();

// Ending *********************

// Hub SignalR
builder.Services.AddSignalR(options =>
{
    options.MaximumReceiveMessageSize = 1024 * 1024;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication(); // Pour utiliser l'authentification cree

app.UseAuthorization();

app.MapControllers();
app.MapHub<InstructionsHub>("/instructionshub");

app.Run();
