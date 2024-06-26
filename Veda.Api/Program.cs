using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using Veda.Api.Configurations;
using Veda.Api.Helpers;
using Veda.Api.MiddleWares;
using Veda.Application;
using Veda.Infrastructure;
using Veda.Infrastructure.DataAccess;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureOptions<JwtConfigurationSetup>();
builder.Services.AddSingleton<JwtProvider>();

JwtConfiguration jwtConfiguration = new();
builder.Configuration.GetSection("Jwt").Bind(jwtConfiguration);
builder.Services.AddAuthentication(x =>
    {
        x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(x =>
    {
        x.RequireHttpsMetadata = builder.Environment.IsDevelopment();
        x.SaveToken = true;
        x.TokenValidationParameters = new TokenValidationParameters
        {
            IssuerSigningKey = new SymmetricSecurityKey("secret_must_be_longer_at_least_256_bit"u8.ToArray()),
            ValidateIssuerSigningKey = true,
            ValidateIssuer = true,
            ValidIssuer = jwtConfiguration.Issuer,
            ValidateAudience = true,
            ValidAudience = jwtConfiguration.Audience
        };
    });

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
builder.Services.AddApplicationDependencies();

builder.Services.AddScoped<ApiEndpointHitLoggerMiddleware>();
builder.Services.AddScoped<ExceptionMiddleware>();

var app = builder.Build();

app.UseSerilogRequestLogging();

app.UseSwagger();
app.UseSwaggerUI();

// if (app.Environment.IsDevelopment())
// {
//     app.UseSwagger();
//     app.UseSwaggerUI();
// }

using (var serviceScope = app.Services.GetService<IServiceScopeFactory>()!.CreateScope())
{
    var context = serviceScope.ServiceProvider.GetRequiredService<VedaDbContext>();
    context.Database.EnsureCreated();
    DatabaseSeeder.Seed(context);
}

app.UseHttpsRedirection();
app.UseCors();

app.UseMiddleware<ApiEndpointHitLoggerMiddleware>();
app.UseMiddleware<ExceptionMiddleware>();

app.MapControllers();

app.Run();

public partial class Program
{
}