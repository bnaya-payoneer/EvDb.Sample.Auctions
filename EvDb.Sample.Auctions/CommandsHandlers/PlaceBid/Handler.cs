
using EvDb.Core;
using EvDb.Sample.Auctions.Abstractions;
using EvDb.Sample.Auctions.Abstractions.EventsPayload;
using EvDb.Sample.Auctions.Abstractions.Views.AuctionStatus;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Threading.Channels;

namespace EvDb.Sample.Auctions.CommandsHandlers.PlaceBid;

public class Handler : IHandler
{
    private readonly IEvDbAuctionStreamFactory _factory;
    private readonly Channel<PublishedEvent> _channel;

    public Handler(
        IEvDbAuctionStreamFactory factory,
        [FromKeyedServices(Constants.OpenAuctionsChannelKey)]
        Channel<PublishedEvent> channel)
    {
        _factory = factory;
        _channel = channel;
    }

    public async Task HandleAsync(Command command, CancellationToken cancellationToken = default)
    {
        int id = command.AuctionId;
        var stream = await _factory.GetAsync(id.ToString(), cancellationToken);
        if(stream.Views.Status == null) 
            throw new KeyNotFoundException(command.AuctionId.ToString());
        if(command.Bid <= stream.Views.Status!.Value.CurrentBid) 
        {
            var rejected = new BidRejectedEvent(id, command.UserId, command.Bid);
            stream.Add(rejected);
            return;
        }
        var payload = new BidPlacedEvent(id, command.UserId, command.Bid);
        IEvDbEventMeta meta = stream.Add(payload);
        await stream.SaveAsync(cancellationToken);
        var message = new PublishedEvent(payload, meta);
        await _channel.Writer.WriteAsync(message);
    }
}
