using EvDb.Core;

namespace EvDb.Sample.Auctions.Abstractions.EventsPayload;

[EvDbEventPayload("auction-created")]
public readonly partial record struct AuctionCreated(int AuctionId,
                                                         string ProductName,
                                                         int StartingPrice);