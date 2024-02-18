using EvDb.Core;
using EvDb.Sample.Auctions.Abstractions.EventsPayload;

namespace EvDb.Sample.Auctions.Abstractions.Views.AuctionStatus;

[EvDbView<State?, IAuctionAdder>("auction")]

internal partial class View
{
    protected override State? DefaultState => null;

    protected override State? Fold(State? state, AuctionCreated payload, IEvDbEventMeta meta)
    {
        return base.Fold(state, payload, meta);
    }

    protected override State? Fold(State? state, BidPlaced payload, IEvDbEventMeta meta)
    {
        return base.Fold(state, payload, meta);
    }

    protected override State? Fold(State? state, BidAccepted payload, IEvDbEventMeta meta)
    {
        return base.Fold(state, payload, meta);
    }

    protected override State? Fold(State? state, BidRejected payload, IEvDbEventMeta meta)
    {
        return base.Fold(state, payload, meta);
    }
}
