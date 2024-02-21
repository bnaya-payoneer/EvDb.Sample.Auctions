using EvDb.Core;
using EvDb.Sample.Auctions.Abstractions.EventsPayload;

namespace EvDb.Sample.Auctions.Abstractions.Views.AuctionStatus;

[EvDbViewType<State?, IAuctionAdder>("auction")]

internal partial class View
{
    protected override State? DefaultState => null;

    protected override State? Fold(State? state, AuctionCreatedEvent payload, IEvDbEventMeta meta)
    {
        return new State(payload.AuctionId, payload.ProductName, payload.StartingPrice);
    }

    protected override State? Fold(State? state, BidPlacedEvent payload, IEvDbEventMeta meta)
    {
        #region Exception Handling

        if (state is null)
        {
            throw new InvalidDataException("Auction not found");
        }

        if (state.Value.AuctionId != payload.AuctionId)
        {
            throw new InvalidDataException("The stream should be of specific auction");
        }

        #endregion // Exception Handling

        if (payload.Bid <= state.Value.CurrentBid)
            return state;

        return state.Value with
        {
            CurrentBid = payload.Bid,
            BidderId = payload.UserId,
            PlacedAt = DateTimeOffset.UtcNow
        };
    }

    protected override State? Fold(State? state, BidAcceptedEvent payload, IEvDbEventMeta meta)
    {
        #region Exception Handling

        if (state is null)
        {
            throw new InvalidDataException("Auction not found");
        }

        if (state.Value.AuctionId != payload.AuctionId)
        {
            throw new InvalidDataException("The stream should be of specific auction");
        }

        #endregion // Exception Handling

        return state.Value with
        {
            AcceptedAt = DateTimeOffset.UtcNow
        };
    }
}