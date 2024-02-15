using EvDb.Core;
using EvDb.Sample.Auctions.Abstractions.EventsPayload;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvDb.Sample.Auctions.Abstractions;

[EvDbEventAdder<AuctionCreated>]

public partial interface IAuctionAdder
{
}
