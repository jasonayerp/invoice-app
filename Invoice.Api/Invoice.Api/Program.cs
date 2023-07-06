using Invoice.Api.Data.SqlServer;
using Invoice.Api.Domains.Common.Mappers;
using Invoice.Api.Extensions.DependencyInjection;
using Invoice.Api.Mvc.Filters;
using Invoice.Services;
using Invoice.System;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;

builder.Services.AddControllers(options =>
{
    options.Filters.Add<ControllerExceptionFilterAttribute>();
})
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

    options.AddPolicy("Policy", builder =>
    {
        var allowedOrigins = Environment.GetEnvironmentVariable("AllowedOrigins");

        if (string.IsNullOrEmpty(allowedOrigins))
        {
            allowedOrigins = configuration.GetValue("Configuration:AllowedOrigins", "");
        }

        string[] allowed = allowedOrigins.Split(',').ToArray();

        builder.SetIsOriginAllowed(origin => allowedOrigins.Contains(new Uri(origin).Host));
    });
});
builder.Services.AddScoped<IMapper, JsonMapper>();
builder.Services.AddScoped<IDateTimeService, DateTimeService>();
builder.Services.AddScoped<IModelService, ModelService>();
builder.Services.AddScopedDomain("Common");

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("Policy");

app.UseAuthorization();

app.MapControllers();

app.Run();
