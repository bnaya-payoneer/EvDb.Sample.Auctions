
using EvDb.Core;
using EvDb.Sample.Auctions.Abstractions;
using EvDb.Sample.Auctions.Abstractions.EventsPayload;
using EvDb.Sample.Auctions.Abstractions.Views.AuctionStatus;
using System.Threading.Channels;
using EvDb.Sample.Auctions.Abstractions.Commands;

namespace EvDb.Sample.Auctions.CommandsHandlers.CreateAuction;

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

    public async Task<State> HandleAsync(CreateAuctionCommand command, CancellationToken cancellationToken = default)
    {
        int id = command.AuctionId;
        IEvDbAuctionStream stream = await _factory.GetAsync(id.ToString(), cancellationToken);
        var payload = new AuctionCreatedEvent(id, command.ProductName, command.StartingPrice);
        IEvDbEventMeta meta = stream.Add(payload);
        await stream.SaveAsync(cancellationToken);
        State response = stream.Views.Status!.Value;
        var message = new PublishedEvent(payload, meta);
        await _channel.Writer.WriteAsync(message);

        return response;
    }
}
