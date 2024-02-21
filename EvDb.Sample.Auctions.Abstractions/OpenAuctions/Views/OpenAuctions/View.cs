﻿using EvDb.Core;
using EvDb.Sample.Auctions.Abstractions.EventsPayload;
using System.Collections.Immutable;

namespace EvDb.Sample.Auctions.Abstractions.Views.OpenAuctions;

using State = ImmutableArray<OpenAuction>;

[EvDbViewType<State, IOpenAuctionsAdder>("auction")]

internal partial class View
{
    protected override State DefaultState => State.Empty;

    protected override State Fold(State state, AuctionCreatedEvent payload, IEvDbEventMeta meta)
    {
        if(state.Any(x => x.AuctionId == payload.AuctionId))
            return state;

        var item = new OpenAuction(payload.AuctionId, payload.ProductName);
        return state.Add(item);
    }

    protected override State Fold(State state, BidAcceptedEvent payload, IEvDbEventMeta meta)
    {
        var result = state.Where(x => x.AuctionId != payload.AuctionId)
                           .ToImmutableArray();
        return result;
    }
}