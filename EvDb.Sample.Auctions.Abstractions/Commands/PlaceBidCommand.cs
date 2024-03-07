namespace EvDb.Sample.Auctions.Abstractions.Commands;

public record PlaceBidCommand(int AuctionId,
    int UserId,
    int Bid);