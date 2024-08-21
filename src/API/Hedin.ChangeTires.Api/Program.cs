using Hedin.ChangeTires.Api.Settings;
using Microsoft.EntityFrameworkCore;
using Hedin.ChangeTires.Modules.Booking.Infrastructure.Configuration;
using Serilog;
using Hedin.ChangeTires.BuildingBlocks.Infrastructure.Configurations;
using Microsoft.OpenApi.Models;
using Hedin.ChangeTires.Modules.Booking.Infrastructure;
using System.Text.Json.Serialization;

// create the app builder
var builder = WebApplication.CreateBuilder(args);

// define the settings types
var settingTypes = new List<Type>
{
    typeof(SlotApiSettings),
    typeof(CommonSettings)
};

// initialize and validate the configuration settings
SettingsInitializer.Initialize(builder.Services, builder.Configuration, settingTypes);

// configure logging
Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .MinimumLevel.Debug()
                .CreateLogger();

// add controllers
builder.Services.AddControllers().AddJsonOptions(opts =>
{
    opts.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

// initialize global services
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

// add Swagger services
builder.Services.AddEndpointsApiExplorer(); 
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Change Tires API", Version = "v1" });
});

// get the connection string
string connectionString = builder.Configuration
    .GetValue<string>($"{nameof(CommonSettings)}:{nameof(CommonSettings.ConnectionString)}");

// initialize modules
BookingStartup.Initialize(connectionString, builder.Services, Log.Logger);

// build app
var app = builder.Build();

// use Swagger middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Change Tires API v1"));
}

// configure db
using (var scope = app.Services.CreateScope()) 
{
    var context = scope.ServiceProvider.GetRequiredService<BookingDBContext>();
    context.Database.EnsureCreated();
    context.Database.Migrate();
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}

app.UseHttpsRedirection();

app.MapControllers();

// this is commented out because the authentication and authorization middleware is not yet implemented
//app.UseAuthentication();
//app.UseAuthorization();

app.Run();
