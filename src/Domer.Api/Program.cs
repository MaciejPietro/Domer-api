using Domer.Api.Common;
using Domer.Api.Configurations;
using Domer.Api.Endpoints;
using Domer.Application.Common.Interfaces;
using Domer.Application.Queries.User.GetCurrentUser;
using Domer.Domain.Interfaces.Projects;
using Domer.Infrastructure;
using Domer.Infrastructure.Repository;
using Domer.Infrastructure.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.IO;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

DotNetEnv.Env.Load();

// Controllers
var configurationBuilder = new ConfigurationBuilder();
configurationBuilder.SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.Development.json", optional: false, reloadOnChange: true).AddEnvironmentVariables().Build();

// builder.Services.AddSingleton<IApplicationConfiguration, ApplicationConfiguration>();

builder.Services.AddControllers();
builder.AddValidationSetup();

builder.Services.AddAuthSetup(builder.Configuration);

builder.Services.AddAutoMapperSetup(builder.Configuration);

// Mediatr
builder.Services.AddMediatR(ctg =>
{
    ctg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
});
builder.Services.AddScoped<IIdentityService, IdentityService>();
builder.Services.AddScoped<IProjectRepository, ProjectRepository>();


// Swagger
builder.Services.AddSwaggerSetup(builder.Configuration);

// Email
builder.Services.AddEmailSetup(builder.Configuration);

// Persistence
builder.Services.AddDbSetup(builder.Configuration);

// S3 File Storage
builder.Services.AddS3StorageSetup(builder.Configuration);

// Application layer setup
builder.Services.AddApplicationSetup();

// Request response compression
builder.Services.AddCompressionSetup();

// HttpContextAccessor
builder.Services.AddHttpContextAccessor();

// Mediator
builder.Services.AddMediatRSetup();

// Exception handler
builder.Services.AddExceptionHandler<ExceptionHandler>();
builder.Services.AddProblemDetails();

// Add CORS services
builder.Services.AddCorsSetup(builder.Configuration);

builder.Logging.ClearProviders();

// Add serilog
if (builder.Environment.EnvironmentName != "Testing")
{
    builder.Host.UseLoggingSetup(builder.Configuration);
    
    // Add opentelemetry
    builder.AddOpenTemeletrySetup();
}


var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseResponseCompression();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseCorsSetup();

app.UseRouting();

app.UseExceptionHandler(o => { });

app.UseSwaggerSetup();
app.UseHsts();

app.UseResponseCompression();
// app.UseHttpsRedirection();

// app.UseAuthentication();
// app.UseAuthorization();
app.UseAuthSetup();

app.MapControllers();

app.MapHeroEndpoints();
app.MapGroup("api/identity")
    .WithTags("Identity")
    .MapIdentityApi<ApplicationUser>();

await app.RunAsync();