using CrayonCloudSale.Core;
using CrayonCloudSale.Infrastructure;
using CrayonCloudSale.Infrastructure.Data;
using CrayonCloudSale.Infrastructure.Services;
using CrayonCloudSale.Services;
using CrayonCloudSale.Services.Interfaces;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);


// add configuration
builder.Configuration.AddUserSecrets<Program>(true);

var azureConfiguration = builder.Configuration.GetSection(AzureConfiguration.Azure);
builder.Services.Configure<AzureConfiguration>(azureConfiguration);
builder.Services.AddSingleton(c => c.GetService<IOptions<AzureConfiguration>>()!.Value);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(); 
builder.Services.AddHttpClient();

builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<IPurchasedSoftwareService, PurchasedSoftwareService>();
builder.Services.AddScoped<ICcpService, CcpService>();
builder.Services.RegisterInfrastructure(azureConfiguration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    SeedDataHelper.SeedData(app.Services);
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();