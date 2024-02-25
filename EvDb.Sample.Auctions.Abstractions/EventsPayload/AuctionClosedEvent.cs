using EvDb.Core;

namespace EvDb.Sample.Auctions.Abstractions.EventsPayload;

[EvDbEventPayload("bid-accepted")]
public readonly partial record struct AuctionClosedEvent(int AuctionId,
                                                        int BidderId, 
                                                        int Bid,
                                                        DateTimeOffset PlacedAt,
                                                        DateTimeOffset ClosedAt);