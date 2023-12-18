using Mapster;
using WorthReads.Application;
using WorthReads.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddApplication()
    .AddInfrastructure(builder.Configuration);

builder.Services.AddCors(options =>
{
    options.AddPolicy("MyPolicy", builder =>
    {
        builder.WithOrigins("https://localhost:7184") // Add your allowed origins here
        .AllowAnyMethod()
        .AllowAnyHeader();
    });
});

//Mapster
{
    var config = TypeAdapterConfig.GlobalSettings;
    config.Scan(typeof(Program).Assembly);
    config.Scan(typeof(WorthReads.Application.DependencyInjectionRegister).Assembly);

    builder.Services.AddSingleton(config);
}


var app = builder.Build();
app.UseCors("MyPolicy");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers().RequireAuthorization();

app.Run();
