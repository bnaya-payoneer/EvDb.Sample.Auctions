using EvDb.Core;

namespace EvDb.Sample.Auctions.Abstractions.EventsPayload;

[EvDbEventPayload("auction-close")]
public readonly partial record struct AuctionClosedEvent(int AuctionId,
                                                        int BidderId, 
                                                        int Bid,
                                                        DateTimeOffset PlacedAt,
                                                        DateTimeOffset ClosedAt);