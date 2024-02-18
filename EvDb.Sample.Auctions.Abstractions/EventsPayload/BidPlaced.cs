using EvDb.Core;

namespace EvDb.Sample.Auctions.Abstractions.EventsPayload;

[EvDbEventPayload("bid-placed")]
public readonly partial record struct BidPlaced(int AuctionId,
                                                         int UserId,
                                                         int Bid);