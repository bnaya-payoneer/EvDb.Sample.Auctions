using EvDb.Sample.Auctions.Abstractions.Views.AuctionStatus;

namespace EvDb.Sample.Auctions.CommandsHandlers.PlaceBid;

public interface IHandler
{
    Task<BidResult> HandleAsync(Command command, CancellationToken cancellationToken = default);
} 
