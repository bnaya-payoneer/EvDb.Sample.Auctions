using EvDb.Core;
using System.Collections.Immutable;

namespace EvDb.Sample.Auctions.Abstractions;


[EvDbAttachView<Views.OpenAuctions.View>("Status")]
[EvDbStreamFactory<IAuctionAdder>]
public partial class OpenAuctionsStreamFactory
{
    #region Ctor

    public OpenAuctionsStreamFactory(IEvDbStorageAdapter storageAdapter) : base(storageAdapter)
    {
    }

    #endregion // Ctor

    #region Partition

    public override EvDbPartitionAddress PartitionAddress { get; } =
        new EvDbPartitionAddress("auction-house", "open-auctions");

    #endregion // PartitionAddress
}
