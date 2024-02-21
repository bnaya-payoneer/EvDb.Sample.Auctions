using EvDb.Core;
using EvDb.Sample.Auctions.Abstractions.EventsPayload;

namespace EvDb.Sample.Auctions.Abstractions;

[EvDbEventAdder<AuctionCreatedEvent>]
[EvDbEventAdder<BidAcceptedEvent>]
public partial interface IOpenAuctionsAdder
{
}
