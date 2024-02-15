using EvDb.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvDb.Sample.Auctions.Abstractions;


[EvDbAttachView<Views.AuctionStatus.View>]
[EvDbStreamFactory<IAuctionAdder>]
internal partial class AuctionStreamFactory
{
    #region Ctor

    public AuctionStreamFactory(IEvDbStorageAdapter storageAdapter): base(storageAdapter)
    {
    }

    #endregion // Ctor

    #region Partition

    public override EvDbPartitionAddress PartitionAddress { get; } = new EvDbPartitionAddress("auction-house", "auction");

    #endregion // PartitionAddress
}
