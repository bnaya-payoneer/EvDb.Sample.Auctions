using EvDb.Sample.Auctions.Abstractions.Views.AuctionStatus;

namespace EvDb.Sample.Auctions.CommandsHandlers.CloseAuction;

public interface IHandler
{
    Task<State> HandleAsync(Command command, CancellationToken cancellationToken = default);

} 
