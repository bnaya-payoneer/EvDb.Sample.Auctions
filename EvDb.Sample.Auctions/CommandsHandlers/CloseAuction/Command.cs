namespace EvDb.Sample.Auctions.CommandsHandlers.CloseAuction;

public readonly record struct Command(int AuctionId,
                                      int BidderId,
                                      int Bid,
                                      DateTimeOffset PlacedAt,
                                      DateTimeOffset ClosedAt)
{
}
