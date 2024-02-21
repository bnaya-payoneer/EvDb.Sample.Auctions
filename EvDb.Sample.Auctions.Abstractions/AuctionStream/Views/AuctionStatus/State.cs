namespace EvDb.Sample.Auctions.Abstractions.Views.AuctionStatus;

public readonly partial record struct State(int AuctionId,
                                            string ProductName,
                                            int CurrentBid)
{
    public int? BidderId { get; init; }
    public DateTimeOffset? PlacedAt { get; init; }
    public DateTimeOffset? AcceptedAt { get; init; }
}
