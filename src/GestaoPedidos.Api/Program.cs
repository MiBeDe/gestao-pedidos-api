using GestaoPedidos.Api.Middlewares;
using GestaoPedidos.Api.Startups;
using GestaoPedidos.CrossCutting.IoC;
using Serilog;


var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, configuration) =>
{
    configuration.ReadFrom.Configuration(context.Configuration);
});

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddCustomizedSwagger();
builder.Services.AddCustomizedMvc();
builder.Services.RegisterNativeInjector();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddCustomizedCors();
builder.Services.AddCustomizedMapper();

var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();
app.MapOpenApi();
app.UseAuthentication();
app.UseCustomizedSwagger();
app.UseCustomizedCors();
app.UseCustomizedMvc();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}

app.MapControllers();

try
{
    Log.Information("API iniciada.");

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "A aplicação foi encerrada inesperadamente.");
}
finally
{
    Log.CloseAndFlush();
}