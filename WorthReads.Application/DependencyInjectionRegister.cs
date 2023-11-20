using FluentValidation;
using MapsterMapper;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using WorthReads.Application.Common.Behaviors;

namespace WorthReads.Application;

public static class DependencyInjectionRegister
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        //Add MediatR
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(DependencyInjectionRegister).Assembly));

        //Add validator
        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
        services.AddValidatorsFromAssembly(typeof(DependencyInjectionRegister).Assembly);


        services.AddSingleton<IMapper, ServiceMapper>();

        return services;
    }
}
