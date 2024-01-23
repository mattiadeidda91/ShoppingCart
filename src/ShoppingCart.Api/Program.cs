using Hangfire;
using Serilog;
using ShoppingCart.Api.Configurations;
using ShoppingCart.Dependencies.Configurations.Hangfire;
using System.Diagnostics;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.RegisterDataServices();

builder.Services.AddControllers();

//Hangfire
builder.AddHangfire();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Automapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

//Serilog
builder.Host.UseSerilog((hostingContext, loggerConfiguration) =>
{
    //Get configuration from appsettings.json
    loggerConfiguration.ReadFrom.Configuration(hostingContext.Configuration);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    //Debug Serilog
    Serilog.Debugging.SelfLog.Enable(msg =>
    {
        Debug.Print(msg);
        //Debugger.Break();
    });
}

//Serilog log all requests
app.UseSerilogRequestLogging();

app.UseHttpsRedirection();

app.UseAuthorization();

//Hangfire Dashboard
app.MapAndUseHangfireDashboard(requiredScope: "shoppingCart", allowAnonymousInDevelopment: true);

//Create job
HangfireBuilderExtensions.ConfigureJob(app.Services);

app.MapControllers();

app.Run();
