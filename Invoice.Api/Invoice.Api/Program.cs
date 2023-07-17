using Invoice.Api.Authorization;
using Invoice.Api.Data.SqlServer;
using Invoice.Api.Domains.Common.Mappers;
using Invoice.Api.Extensions.DependencyInjection;
using Invoice.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;

// swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// custom extensions
builder.Services.AddConfigurationReader();

// mvc
builder.Services.AddControllers(options =>
    {
        options.Filters.Add<ControllerExceptionFilterAttribute>();
    })
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
    });

// entityframework
builder.Services.AddDbContextFactory<SqlServerDbContext>(options =>
    {
        var connectionString = Environment.GetEnvironmentVariable("SqlServerConnectionString") ?? configuration.GetValue<string>("Configuration:SqlServerConnectionString");

        options.UseSqlServer(connectionString);
    });    

// cors
builder.Services.AddCors(options =>
    {
        options.AddDefaultPolicy(builder =>
        {
            builder.SetIsOriginAllowed(origin => new Uri(origin).Host == "localhost");
        });
    });

// authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.Authority = Environment.GetEnvironmentVariable("Auth0Authority") ?? configuration.GetValue<string>("Configuration:Auth0Authority");
        options.Audience = Environment.GetEnvironmentVariable("Auth0Audience") ?? configuration.GetValue<string>("Configuration:Auth0Audience");
        options.TokenValidationParameters = new TokenValidationParameters { NameClaimType = ClaimTypes.NameIdentifier };
    });

// authorization
builder.Services.AddAuthorization(options =>
    {
        var authority = Environment.GetEnvironmentVariable("Auth0Authority") ?? configuration.GetValue<string>("Configuration:Auth0Authority");
        var scopes = Environment.GetEnvironmentVariable("Auth0Scopes") ?? configuration.GetValue<string>("Configuration:Auth0Scopes");
        foreach (var scope in scopes.Split(' '))
        {
            options.AddPolicy(scope, policy => policy.Requirements.Add(new HasScopeRequirement(scope, authority)));
        }
    });

// implicit service registration
builder.Services.AddScopedDomain("Common");

// explicit service registration
builder.Services.AddSingleton<IAuthorizationHandler, HasScopeHandler>();
builder.Services.AddScoped<IDateTimeService, DateTimeService>();

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
