
using EvDb.Sample.Auctions.Abstractions;
using EvDb.Sample.Auctions.Abstractions.EventsPayload;
using EvDb.Sample.Auctions.Abstractions.Views.AuctionStatus;

namespace EvDb.Sample.Auctions.CommandsHandlers.PlaceBid;

public class Handler : IHandler
{
    private readonly IEvDbAuctionStreamFactory _factory;

    public Handler(IEvDbAuctionStreamFactory factory)
    {
        _factory = factory;
    }

    public async Task HandleAsync(Command command, CancellationToken cancellationToken = default)
    {
        int id = command.AuctionId;
        var stream = await _factory.GetAsync(id.ToString(), cancellationToken);
        var payload = new BidPlacedEvent(id, command.UserId, command.Bid);
        stream.Add(payload);
        await stream.SaveAsync(cancellationToken);

        if(stream.Views.Status!.Value.BidderId == payload.UserId)
        {
            // TODO: update open auctions
        }
    }
}
