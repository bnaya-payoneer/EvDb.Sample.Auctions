using EvDb.Core;

namespace EvDb.Sample.Auctions.Abstractions.EventsPayload;

[EvDbEventPayload("bid-accepted")]
public readonly partial record struct BidAccepted(int AuctionId,
                                                  DateTimeOffset AcceptedAt);