namespace EvDb.Sample.Auctions.Abstractions.Views.AuctionStatus;

public readonly partial record struct State(int AuctionId,
                                            string ProductName,
                                            int CurrentBid,
                                            int UserId)
{
    public DateTimeOffset? PlacedAt { get; init; }
    public DateTimeOffset? AcceptedAt { get; init; }
}
