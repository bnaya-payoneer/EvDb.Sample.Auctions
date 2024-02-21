namespace EvDb.Sample.Auctions.CommandsHandlers.CreateAuction;

public readonly record struct Command(int AuctionId,
                                      string ProductName,
                                      int StartingPrice)
{
}
