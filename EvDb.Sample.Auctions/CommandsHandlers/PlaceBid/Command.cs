namespace EvDb.Sample.Auctions.CommandsHandlers.PlaceBid;

public readonly record struct Command(int AuctionId,
                                      int UserId,
                                      int Bid);
