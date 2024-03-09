using EvDb.Sample.Auctions.Abstractions.Commands;
using EvDb.Sample.Auctions.Abstractions.Views.AuctionStatus;

namespace EvDb.Sample.Auctions.CommandsHandlers.CreateAuction;

public interface IHandler
{
    Task<State> HandleAsync(CreateAuctionCommand command, CancellationToken cancellationToken = default);

} 
