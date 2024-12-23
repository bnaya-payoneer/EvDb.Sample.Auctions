﻿using EvDb.Core;
using System.Collections.Immutable;

namespace EvDb.Sample.Auctions.Abstractions;


[EvDbAttachView<Views.AuctionStatus.View>("Status")]
[EvDbStreamFactory<IAuctionAdder>]
public partial class AuctionStreamFactory
{
    #region Partition

    public override EvDbPartitionAddress PartitionAddress { get; } =
        new EvDbPartitionAddress("auction-house", "auction");

    #endregion // PartitionAddress
}
