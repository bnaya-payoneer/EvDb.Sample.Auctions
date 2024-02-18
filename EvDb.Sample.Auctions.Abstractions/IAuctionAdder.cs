﻿using EvDb.Core;
using EvDb.Sample.Auctions.Abstractions.EventsPayload;

namespace EvDb.Sample.Auctions.Abstractions;

[EvDbEventAdder<AuctionCreated>]
[EvDbEventAdder<BidPlaced>]
[EvDbEventAdder<BidAccepted>]
[EvDbEventAdder<BidRejected>]

public partial interface IAuctionAdder
{
}
