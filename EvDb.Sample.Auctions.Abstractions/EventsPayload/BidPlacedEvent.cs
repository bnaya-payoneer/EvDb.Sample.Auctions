using EvDb.Core;

namespace EvDb.Sample.Auctions.Abstractions.EventsPayload;

[EvDbEventPayload("bid-placed")]
public readonly partial record struct BidPlacedEvent(int AuctionId,
                                                         int UserId,
                                                         int Bid);