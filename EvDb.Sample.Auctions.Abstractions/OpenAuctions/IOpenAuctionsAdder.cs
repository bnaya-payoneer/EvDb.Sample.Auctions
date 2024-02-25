using EvDb.Core;
using EvDb.Sample.Auctions.Abstractions.EventsPayload;

namespace EvDb.Sample.Auctions.Abstractions;

[EvDbEventAdder<AuctionCreatedEvent>]
[EvDbEventAdder<BidPlacedEvent>]
[EvDbEventAdder<AuctionClosedEvent>]
public partial interface IOpenAuctionsAdder
{
}
