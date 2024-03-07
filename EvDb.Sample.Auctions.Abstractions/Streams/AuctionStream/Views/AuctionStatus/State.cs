namespace EvDb.Sample.Auctions.Abstractions.Views.AuctionStatus;

public readonly partial record struct State(int AuctionId,
                                            string ProductName,
                                            int StartingPrice)
{
    public int? CurrentBid { get; init; }
    public int? BidderId { get; init; }
    public DateTimeOffset? PlacedAt { get; init; }
    public DateTimeOffset? ClosedAt { get; init; }
}
