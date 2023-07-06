using Invoice.Api.Data.SqlServer;
using Invoice.Api.Domains.Common.Mappers;
using Invoice.Api.Domains.Common.Repositories;
using Invoice.Api.Domains.Common.Services;
using Invoice.Api.Extensions.DependencyInjection;
using Invoice.System;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase;
    });
builder.Services.AddConfigurationReader();
builder.Services.AddDbContextFactory<SqlServerDbContext>(options =>
{
    var connectionString = Environment.GetEnvironmentVariable("ConnectionString");

    if (string.IsNullOrEmpty(connectionString))
    {
        connectionString = configuration.GetValue("Configuration:ConnectionString", "");
    }

    options.UseSqlServer(connectionString);
});    
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.SetIsOriginAllowed(origin => new Uri(origin).Host == "localhost");
    });
});
builder.Services.AddScoped<IMapper, JsonMapper>();
builder.Services.AddScoped<IDateTimeService, DateTimeService>();
//builder.Services.AddScoped<IAddressRepository, AddressRepository>();
//builder.Services.AddScoped<IAddressService, AddressService>();
builder.Services.AddDomain("Common");

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors();

app.UseAuthorization();

app.MapControllers();

app.Run();
