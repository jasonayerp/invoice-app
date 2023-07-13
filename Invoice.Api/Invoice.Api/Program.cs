using Invoice.Api.Authorization;
using Invoice.Api.Data.SqlServer;
using Invoice.Api.Domains.Common.Mappers;
using Invoice.Api.Extensions.DependencyInjection;
using Invoice.Api.Mvc.Filters;
using Invoice.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;
var domain = $"https://{configuration["Configuration:Auth0Domain"]}/";

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
});
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
.AddJwtBearer(options =>
{
    options.Authority = domain;
    options.Audience = configuration["Configuration:Auth0Audience"];
    options.TokenValidationParameters = new TokenValidationParameters
    {
        NameClaimType = ClaimTypes.NameIdentifier
    };
});
builder.Services.AddAuthorization(options =>
{
    var scopes = configuration["Configuration:Auth0Scopes"].Split(' ');

    foreach (var scope in scopes)
    {
        options.AddPolicy(scope, policy => policy.Requirements.Add(new HasScopeRequirement(scope, domain)));
    }
});
builder.Services.AddSingleton<IAuthorizationHandler, HasScopeHandler>();
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

app.UseCors();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
