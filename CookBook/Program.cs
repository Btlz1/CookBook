using CookBook.Models;
using CookBook.Settings;
using FluentValidation.AspNetCore;
using FluentValidation;
var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddFluentValidationAutoValidation() 
    .AddFluentValidationClientsideAdapters();

builder.Services
    .AddCustomServices(builder.Configuration);
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.MapGet("/", context =>
    {
        context.Response.Redirect("/swagger/index.html");
        return Task.CompletedTask;
    });
}

app.UseAuthentication(); 
app.UseAuthorization(); 
app.MapControllers(); 

app.Run();