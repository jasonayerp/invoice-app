using Invoice.Configuration;
using Invoice.Web.Data;
using Invoice.Web.Domains.Common.Repositories;
using Invoice.Web.Domains.Common.Services;
using Invoice.Web.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddHttpClient();
builder.Services.AddInvoiceHttpClient();
builder.Services.AddConfigurationReader();
builder.Services.AddTokenProvider();
builder.Services.AddMemoryCache();
builder.Services.AddSingleton<WeatherForecastService>();

// custom extensions
builder.Services.AddCacheProvider();

// builder.Services.AddScopedDomain("Common");
builder.Services.AddScoped<IAddressRepository, AddressRepository>();
builder.Services.AddScoped<IAddressService, AddressService>();
builder.Services.AddScoped<IConfigurationReader, NetCoreConfigurationReader>();
builder.Services.AddScoped<IAddressVerificationService, SmartyAddressVerificationService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
