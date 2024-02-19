using EvDb.Core;

namespace EvDb.Sample.Auctions.Abstractions.EventsPayload;

[EvDbEventPayload("bid-rejected")]
public readonly partial record struct BidRejectedEvent(int AuctionId,
                                                  DateTimeOffset AcceptedAt);