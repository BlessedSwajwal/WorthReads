using Mapster;
using WorthReads.Application;
using WorthReads.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddApplication()
    .AddInfrastructure(builder.Configuration);

//Mapster
{
    var config = TypeAdapterConfig.GlobalSettings;
    config.Scan(typeof(Program).Assembly);
    config.Scan(typeof(WorthReads.Application.DependencyInjectionRegister).Assembly);

    builder.Services.AddSingleton(config);
}


var app = builder.Build();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers().RequireAuthorization();

app.Run();
