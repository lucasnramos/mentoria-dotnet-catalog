using Microsoft.Extensions.Options;
using Marraia.Notifications.Configurations;
using Microsoft.Extensions.Caching.StackExchangeRedis;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApi();
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.InstanceName = builder.Configuration.GetSection("Redis:InstanceName").Value;
    options.Configuration = builder.Configuration.GetSection("Redis:Configuration").Value;
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy",
            builder =>
            builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
});

builder.Services.AddSmartNotification();

// new RootBootstrapper().BootstrapperRegisterServices(builder.Services, builder.Configuration);
var app = builder.Build();

app.MapOpenApi();
app.UseHttpsRedirection();
app.UseCors("CorsPolicy");
app.MapControllers();
app.UseAuthentication();
app.UseAuthorization();

app.Run();