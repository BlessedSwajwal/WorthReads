using Infrastructure.Services;
using Microsoft.Extensions.Hosting;

namespace Infrastructure.BackgroundTasks;

public class GetReadsBackgroundTask : BackgroundService
{
    private readonly ReadsService _readsService;
    private readonly TimeSpan _period = TimeSpan.FromMinutes(1);

    public GetReadsBackgroundTask(ReadsService readsService)
    {
        _readsService = readsService;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using PeriodicTimer timer = new PeriodicTimer(_period);

        while (!stoppingToken.IsCancellationRequested && await timer.WaitForNextTickAsync(stoppingToken))
        {
            var result = await _readsService.GetReads("technology", "popularity");
            await Console.Out.WriteLineAsync("Here.");
            await Console.Out.WriteLineAsync(result.FirstOrDefault()!.Content);
            //   await Console.Out.WriteLineAsync("Hello");

        }

    }
}
