using EvDb.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvDb.Sample.Auctions.Abstractions.EventsPayload;

[EvDbEventPayload("auction-created")]
public readonly partial record struct AuctionCreated(int AuctionId,
                                                         string ProductName,
                                                         int StartingPrice);