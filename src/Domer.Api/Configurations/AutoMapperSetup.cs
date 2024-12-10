using Domer.Application.Common.Mappings;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Domer.Api.Configurations;

public static class AutoMapperSetup
{
    public static IServiceCollection AddAutoMapperSetup(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAutoMapper(typeof(UserMappingProfile));

        return services;
    }
    

}