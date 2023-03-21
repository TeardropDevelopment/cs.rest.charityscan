using cs.api.charityscan.Entities;
using System.Configuration;
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.CodeAnalysis.CSharp.Syntax;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add DbContext
builder.Services.AddDbContext<CharityscanDevContext>(options =>
        options.UseMySQL(builder.Configuration.GetConnectionString("WebApiDatabase") ?? ""));

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c => {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "CharityScan API v1");
        c.DocumentTitle = "CharityScanAPI v1";
        c.RoutePrefix = "api";
    }
);

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
