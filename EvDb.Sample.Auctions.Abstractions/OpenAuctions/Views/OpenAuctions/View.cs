using EvDb.Core;
using EvDb.Sample.Auctions.Abstractions.EventsPayload;
using System.Collections.Immutable;

namespace EvDb.Sample.Auctions.Abstractions.Views.OpenAuctions;

using State = ImmutableArray<AuctionStatus.State>;

[EvDbViewType<State, IOpenAuctionsAdder>("auction")]
internal partial class View
{
    protected override State DefaultState => State.Empty;

    protected override State Fold(State state, AuctionCreatedEvent payload, IEvDbEventMeta meta)
    {
        if (state.Any(x => x.AuctionId == payload.AuctionId))
            return state;

        var item = new AuctionStatus.State(payload.AuctionId, payload.ProductName, payload.StartingPrice);
        return state.Add(item);
    }

    protected override State Fold(State state, BidPlacedEvent payload, IEvDbEventMeta meta)
    {
        var item = state.FirstOrDefault(m => m.AuctionId == payload.AuctionId);
        if (item == default)
            throw new InvalidOperationException($"Auction Id [{payload.AuctionId}] is missing");
        if (!item.CurrentBid.HasValue || payload.Bid > item.CurrentBid)
        {
            state = state.Remove(item)
                        .Insert(0, item with
                        {
                            BidderId = payload.UserId,
                            CurrentBid = payload.Bid,
                            PlacedAt = meta.CapturedAt
                        });
        }
        return state;
    }

    protected override State Fold(State state, AuctionClosedEvent payload, IEvDbEventMeta meta)
    {
        var result = state.Where(x => x.AuctionId != payload.AuctionId)
                           .ToImmutableArray();
        return result;
    }
}
