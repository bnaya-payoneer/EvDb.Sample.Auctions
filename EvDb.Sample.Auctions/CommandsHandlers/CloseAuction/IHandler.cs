using EvDb.Sample.Auctions.Abstractions.Commands;
using EvDb.Sample.Auctions.Abstractions.Views.AuctionStatus;

namespace EvDb.Sample.Auctions.CommandsHandlers.CloseAuction;

public interface IHandler
{
    Task<State> HandleAsync(CloseAuctionCommand command, CancellationToken cancellationToken = default);

} 
