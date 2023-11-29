using Application.Common.Services;
using Microsoft.Extensions.Hosting;

namespace Infrastructure.BackgroundTasks;

public class GetReadsBackgroundTask : BackgroundService
{
    private readonly IPocketAPIService _pocketAPIService;
    private readonly TimeSpan _period = TimeSpan.FromMinutes(1);

    public GetReadsBackgroundTask(IPocketAPIService pocketAPIService)
    {
        _pocketAPIService = pocketAPIService;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using PeriodicTimer timer = new PeriodicTimer(_period);
        await Console.Out.WriteLineAsync("BG task1 --> Get Pocket executed.");

        while (!stoppingToken.IsCancellationRequested && await timer.WaitForNextTickAsync(stoppingToken))
        {
            //   var result = await _pocketAPIService.GetPocketList();
            await Console.Out.WriteLineAsync("BG task --> Get Pocket executed.");
            //   await Console.Out.WriteLineAsync("Hello");

        };

    }
}
