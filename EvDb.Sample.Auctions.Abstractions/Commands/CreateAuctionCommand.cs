namespace EvDb.Sample.Auctions.Abstractions.Commands;

public record CreateAuctionCommand(int AuctionId,
    string ProductName,
    int StartingPrice);