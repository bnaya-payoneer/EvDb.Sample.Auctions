
using EvDb.Sample.Auctions.Abstractions;
using EvDb.Sample.Auctions.Abstractions.EventsPayload;
using EvDb.Sample.Auctions.Abstractions.Views.AuctionStatus;

namespace EvDb.Sample.Auctions.CommandsHandlers.CreateAuction;

public class Handler : IHandler
{
    private readonly IEvDbAuctionStreamFactory _factory;

    public Handler(IEvDbAuctionStreamFactory factory)
    {
        _factory = factory;
    }

    public async Task<State> HandleAsync(Command command, CancellationToken cancellationToken = default)
    {
        int id = command.AuctionId;
        var stream = await _factory.GetAsync(id.ToString(), cancellationToken);
        var payload = new AuctionCreatedEvent(id, command.ProductName, command.StartingPrice);
        stream.Add(payload);
        await stream.SaveAsync(cancellationToken);
        State response = stream.Views.Status!.Value;
        return response;
    }
}
