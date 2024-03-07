using EvDb.Sample.Auctions.Abstractions.Commands;
using EvDb.Sample.Auctions.Abstractions.Views.AuctionStatus;

namespace EvDb.Sample.Auctions.CommandsHandlers.PlaceBid;

public interface IHandler
{
    Task<BidResult> HandleAsync(PlaceBidCommand command, CancellationToken cancellationToken = default);
} 
