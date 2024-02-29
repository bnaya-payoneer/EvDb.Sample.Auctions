using EvDb.Sample.Auctions.Abstractions;
using EvDb.Sample.Auctions.Abstractions.Views.AuctionStatus;
using System.Collections.Immutable;
using System.Threading.Channels;

namespace EvDb.Sample.Auctions.Projectors.OpenAuctions;

internal class HostedService : BackgroundService
{
    private readonly IListener _listener;
    private readonly Channel<PublishedEvent> _channel;

    public HostedService(
                   IListener listener,
                   [FromKeyedServices(Constants.OpenAuctionsChannelKey)]
                   Channel<PublishedEvent> channel)
    {
        _listener = listener;
        _channel = channel;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await _listener.ListenAsync(_channel, stoppingToken);
    }
}
