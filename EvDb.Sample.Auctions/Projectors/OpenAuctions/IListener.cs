using EvDb.Sample.Auctions.Abstractions.Views.AuctionStatus;
using System.Collections.Immutable;
using System.Threading.Channels;

namespace EvDb.Sample.Auctions.Projectors.OpenAuctions;

public interface IListener
{
    Task ListenAsync(Channel<PublishedEvent> channel, CancellationToken cancellationToken);
} 
