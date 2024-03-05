
using EvDb.Core;
using EvDb.Sample.Auctions.Abstractions;
using EvDb.Sample.Auctions.Abstractions.EventsPayload;
using EvDb.Sample.Auctions.Abstractions.Views.AuctionStatus;
using System.Collections.Immutable;
using System.ComponentModel;
using System.Threading.Channels;

namespace EvDb.Sample.Auctions.Projectors.OpenAuctions;

using Status = ImmutableArray<State>;

public class Handler : IView, IListener
{
    private readonly Task<IEvDbOpenAuctionsStream> _streamTask;

    public Handler([FromKeyedServices(Constants.OpenAuctionsProjectionKey)]
                    Task<IEvDbOpenAuctionsStream> streamTask)
    {
        _streamTask = streamTask;
    }
    

    public async Task ListenAsync(Channel<PublishedEvent> channel, CancellationToken cancellationToken)
    {
        var stream = await _streamTask;
        await foreach (var message in channel.Reader.ReadAllAsync(cancellationToken))
        {
            switch (message.Payload)
            {
                case AuctionCreatedEvent created:
                    stream.Add(created);
                    break;
                case BidPlacedEvent bid:
                    stream.Add(bid);
                    break;
                case AuctionClosedEvent closed:
                    stream.Add(closed);
                    break;
                default:
                    throw new NotSupportedException();
            }
            await stream.SaveAsync();
        }
    }

    public async Task<Status> ReadStateAsync()
    {
        var stream = await _streamTask;
        var status = stream.Views.Status;
        return status;
    }
}
