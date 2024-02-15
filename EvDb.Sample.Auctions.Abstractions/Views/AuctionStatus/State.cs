using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvDb.Sample.Auctions.Abstractions.Views.AuctionStatus;
public readonly partial record struct State(int AuctionId,
                                            string ProductName,
                                            int CurrentBid)
{
    public DateTimeOffset? PlacedAt { get; init; }
    public DateTimeOffset? AcceptedAt { get; init; }    
}
