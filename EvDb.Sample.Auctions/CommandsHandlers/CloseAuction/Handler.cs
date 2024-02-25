
using EvDb.Core;
using EvDb.Sample.Auctions.Abstractions;
using EvDb.Sample.Auctions.Abstractions.EventsPayload;
using EvDb.Sample.Auctions.Abstractions.Views.AuctionStatus;
using System.Threading.Channels;

namespace EvDb.Sample.Auctions.CommandsHandlers.CloseAuction;

public class Handler : IHandler
{
    private readonly IEvDbAuctionStreamFactory _factory;
    private readonly Channel<PublishedEvent> _channel;

    public Handler(IEvDbAuctionStreamFactory factory,
                   [FromKeyedServices(Constants.OpenAuctionsChannelKey)]
                   Channel<PublishedEvent> channel)
    {
        _factory = factory;
        _channel = channel;
    }

    public async Task<State> HandleAsync(Command command, CancellationToken cancellationToken = default)
    {
        int id = command.AuctionId;
        var stream = await _factory.GetAsync(id.ToString(), cancellationToken);
        var payload = new AuctionClosedEvent(id, command.BidderId, command.Bid, command.PlacedAt, command.ClosedAt);
        IEvDbEventMeta meta = stream.Add(payload);
        await stream.SaveAsync(cancellationToken);
        State response = stream.Views.Status!.Value;

        var message = new PublishedEvent(payload, meta);
        await _channel.Writer.WriteAsync(message);

        return response;
    }
}
