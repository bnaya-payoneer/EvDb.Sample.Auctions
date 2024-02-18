using EvDb.Core;
using EvDb.Sample.Auctions.Abstractions.EventsPayload;

namespace EvDb.Sample.Auctions.Abstractions;

[EvDbEventAdder<AuctionCreated>]
[EvDbEventAdder<BidPlaced>]
[EvDbEventAdder<BidAccepted>]

public partial interface IAuctionAdder
{
}
