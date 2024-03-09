using EvDb.Sample.Auctions.Abstractions;
using EvDb.Sample.Auctions.Abstractions.Views.AuctionStatus;
using EvDb.Sample.Auctions.CommandsHandlers.CloseAuction;
using System.Collections.Immutable;
using System.Threading.Channels;
using EvDb.Sample.Auctions.Abstractions.Commands;

namespace EvDb.Sample.Auctions.Processors.AuctionCloser;

internal class HostedService : BackgroundService
{
    private readonly Task<IEvDbOpenAuctionsStream> _streamTask;
    private readonly IHandler _handler;

    public HostedService(
        [FromKeyedServices(Constants.OpenAuctionsProjectionKey)]
        Task<IEvDbOpenAuctionsStream> streamTask,
        IHandler handler)
    {
        _streamTask = streamTask;
        _handler = handler;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var stream = await _streamTask;
        while (!stoppingToken.IsCancellationRequested)
        {
            await Task.Delay(1000);
            var now = DateTimeOffset.UtcNow;
            var openAuctions = stream.Views.Status;
            var tasks = openAuctions.Select(TryCloseAuctionAsync);
            await Task.WhenAll(tasks);

            async Task TryCloseAuctionAsync(State auction)
            {
                if (auction.PlacedAt == null ||
                   auction.BidderId == null ||
                   auction.CurrentBid == null)
                {
                    return;
                }

                if (now - auction.PlacedAt < TimeSpan.FromSeconds(10))
                    return;
                var command = new CloseAuctionCommand(auction.AuctionId, auction.BidderId.Value, auction.CurrentBid.Value, auction.PlacedAt.Value, now);
                await _handler.HandleAsync(command);
            }
        }
    }
}
