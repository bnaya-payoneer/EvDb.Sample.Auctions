using EvDb.Core;

namespace EvDb.Sample.Auctions.Abstractions.EventsPayload;

[EvDbEventPayload("bid-rejected")]
public readonly partial record struct BidRejected(int AuctionId,
                                                  DateTimeOffset AcceptedAt);