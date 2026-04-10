using Kompass.Api.Common;
using Kompass.Api.Configurations;
using Kompass.Infrastructure.Configurations;
using Kompass.Application.Common.Interfaces;
using Kompass.Application.Queries.User.GetCurrentUser;
using Kompass.Application.Services.Devices;
using Kompass.Application.Services.Devices.Factories;
using Kompass.Domain.Interfaces.Devices;
using Kompass.Domain.Interfaces.Documents;
using Kompass.Domain.Interfaces.Folders;
using Kompass.Domain.Interfaces.Projects;
using Kompass.Infrastructure;
using Kompass.Infrastructure.Repository;
using Kompass.Infrastructure.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.IO;
using System.Reflection;

// Load environment variables from .env file at the very start
EnvService.Load();

var builder = WebApplication.CreateBuilder(args);

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
builder.Services.AddScoped<IFolderRepository, FolderRepository>();
builder.Services.AddScoped<IDocumentRepository, DocumentRepository>();
builder.Services.AddScoped<CameraDeviceFactory>();
builder.Services.AddScoped<DeviceFactoryRegistry>();
builder.Services.AddScoped<IDeviceRepository, DeviceRepository>();
builder.Services.AddScoped<IDeviceService, DeviceService>();


// Swagger
builder.Services.AddSwaggerSetup(builder.Configuration);

// Email
builder.Services.AddEmailSetup(builder.Configuration);

// Persistence
builder.Services.AddDbSetup();

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

app.MapGroup("api/identity")
    .WithTags("Identity")
    .MapIdentityApi<ApplicationUser>();

await app.RunAsync();