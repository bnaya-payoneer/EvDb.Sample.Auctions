using EvDb.Core;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvDb.Sample.Auctions.Abstractions.Views.AuctionStatus;

[EvDbView<State?, IAuctionAdder>("auction")]

internal partial class View
{
    protected override State? DefaultState => null;
}
