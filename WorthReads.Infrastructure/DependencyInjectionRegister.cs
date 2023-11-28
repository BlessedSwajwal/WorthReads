using Application.Common.Services;
using Infrastructure.Authentication;
using Infrastructure.Pocket;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using WorthReads.Application.Common.Interfaces.Repositories;
using WorthReads.Infrastructure.Repositories;

namespace WorthReads.Infrastructure;

public static class DependencyInjectionRegister
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IUserRepository, UserRepository>();

        services.AddSingleton<IJwtGenerator, JwtGenerator>();

        AddAuth(services, configuration);

        //services.AddHostedService<GetReadsBackgroundTask>();

        //Pocket API Stuffs
        //TODO - Properly separate codes.
        services.AddHttpClient<IPocketAPIService, PocketAPIService>(op =>
        {
            op.BaseAddress = new Uri("https://getpocket.com");
        });

        var pocketSettings = new PocketAPISettings();
        configuration.GetSection(PocketAPISettings.SectionName).Bind(pocketSettings);
        services.AddSingleton(Options.Create<PocketAPISettings>(pocketSettings));

        return services;
    }

    private static void AddAuth(IServiceCollection services, IConfiguration configuration)
    {
        //Options Configuration
        var jwtSettings = new JwtSettings();
        configuration.GetSection(JwtSettings.SectionName).Bind(jwtSettings);
        services.AddSingleton(jwtSettings);
        services.AddSingleton(Options.Create<JwtSettings>(jwtSettings));

        //Add Authentication
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtSettings.Issuer,
            ValidAudience = jwtSettings.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SecretKey))
        });
    }
}
