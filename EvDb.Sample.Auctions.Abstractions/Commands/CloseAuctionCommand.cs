namespace EvDb.Sample.Auctions.Abstractions.Commands;

public readonly record struct CloseAuctionCommand(int AuctionId,
    int BidderId,
    int Bid,
    DateTimeOffset PlacedAt,
    DateTimeOffset ClosedAt)
{
}
