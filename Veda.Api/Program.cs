using System.Text.Json.Serialization;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Veda.Api.Configurations;
using Veda.Api.MiddleWares;
using Veda.Infrastructure;
using Veda.Infrastructure.DataAccess;

var builder = WebApplication.CreateBuilder(args);

builder.Host
    .UseSerilog((ctx, lc) => lc
        .ReadFrom.Configuration(ctx.Configuration)
        .Enrich.FromLogContext()
        .WriteTo.Console()
    );

builder.Services
    .AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

builder.Services.AddSwaggerConfiguration();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowNextJsApp",
        policyBuilder =>
        {
            policyBuilder
                .AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddInfrastructureDependencies();

builder.Services.AddScoped<ApiEndpointHitLoggerMiddleware>();
builder.Services.AddScoped<ExceptionMiddleware>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using (var serviceScope = app.Services.GetService<IServiceScopeFactory>()!.CreateScope())
{
    var context = serviceScope.ServiceProvider.GetRequiredService<VedaDbContext>();
    context.Database.Migrate();
}

app.UseHttpsRedirection();
app.UseCors();

app.UseMiddleware<ApiEndpointHitLoggerMiddleware>();
app.UseMiddleware<ExceptionMiddleware>();

app.MapControllers();

app.Run();