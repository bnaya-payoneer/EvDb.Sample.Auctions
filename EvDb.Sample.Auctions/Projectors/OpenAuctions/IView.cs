using EvDb.Sample.Auctions.Abstractions.Views.AuctionStatus;
using System.Collections.Immutable;

namespace EvDb.Sample.Auctions.Projectors.OpenAuctions;

using Status = ImmutableArray<State>;

public interface IView
{
    Task<Status> ReadStateAsync();
} 
