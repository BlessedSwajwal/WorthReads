using Application.Common.Interfaces.Repositories;
using Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;
using WorthReads.Application.Common.Interfaces.Repositories;
using WorthReads.Infrastructure.Repositories;

namespace WorthReads.Infrastructure;

public static class DependencyInjectionRegister
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IReadsRepository, ReadsRepository>();
        return services;
    }
}
