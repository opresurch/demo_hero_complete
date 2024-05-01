using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using demo_hero_complete.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//var app = builder.Build();

if (builder.Environment.IsDevelopment())
{
    //Use a constant for the connection string key to avoid typos


   builder.Services.AddDbContext<DataContext>(options =>
   {
       // Use constant for connection string key
       options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
   });
}
else if (builder.Environment.IsProduction())
{
    var keyVaultURL = builder.Configuration["KeyVault:KeyVaultURL"];

    var client = new SecretClient(new Uri(keyVaultURL), new DefaultAzureCredential());
    var prodConnectionString = client.GetSecret("prodConnectionString").Value.Value.ToString();

    // Register AppDbContext with the production connection string from Key Vault
    builder.Services.AddDbContext<DataContext>(options =>
    {
        options.UseSqlServer(prodConnectionString);
    });
}
var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();




app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

app.Run();
