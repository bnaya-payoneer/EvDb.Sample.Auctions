using EvDb.Core;
using EvDb.Sample.Auctions.Abstractions.EventsPayload;

namespace EvDb.Sample.Auctions.Abstractions.Views.AuctionStatus;

[EvDbViewType<State?, IAuctionAdder>("auction")]

internal partial class View
{
    protected override State? DefaultState => null;

    protected override State? Fold(State? state, AuctionCreatedEvent payload, IEvDbEventMeta meta)
    {
        return base.Fold(state, payload, meta);
    }

    protected override State? Fold(State? state, BidPlacedEvent payload, IEvDbEventMeta meta)
    {
        return base.Fold(state, payload, meta);
    }

    protected override State? Fold(State? state, BidAcceptedEvent payload, IEvDbEventMeta meta)
    {
        return base.Fold(state, payload, meta);
    }

    protected override State? Fold(State? state, BidRejectedEvent payload, IEvDbEventMeta meta)
    {
        return base.Fold(state, payload, meta);
    }
}
