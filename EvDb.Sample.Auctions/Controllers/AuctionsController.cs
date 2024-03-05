using EvDb.Sample.Auctions.Abstractions.Views.AuctionStatus;
using EvDb.Sample.Auctions.CommandsHandlers.PlaceBid;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Immutable;
using CreateAuction = EvDb.Sample.Auctions.CommandsHandlers.CreateAuction;
using PlaceBid = EvDb.Sample.Auctions.CommandsHandlers.PlaceBid;

namespace EvDb.Sample.Auctions.Controllers;

using Status = ImmutableArray<State>;

[ApiController]
[Route("[controller]")]
public class AuctionsController : ControllerBase
{
    private readonly ILogger<AuctionsController> _logger;
    private readonly CreateAuction.IHandler _createAuctionHandler;
    private readonly PlaceBid.IHandler _placeBidHandler;
    private readonly Projectors.OpenAuctions.IView _openAuctionsView;

    public AuctionsController(
        ILogger<AuctionsController> logger,
        CreateAuction.IHandler createAuctionHandler,
        PlaceBid.IHandler placeBidHandler,
        Projectors.OpenAuctions.IView openAuctionsView)
    {
        _logger = logger;
        _createAuctionHandler = createAuctionHandler;
        _placeBidHandler = placeBidHandler;
        _openAuctionsView = openAuctionsView;
    }

    [HttpPost("create-auction")]
    [ProducesResponseType<State>(200)]
    public async Task<IActionResult> CreateAuctionAsync(CreateAuction.Command command)
    {
        var state = await _createAuctionHandler.HandleAsync(command);
        return Ok(state);
    }

    [HttpPost("place-bid")]
    [ProducesResponseType<BidResult>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> PlaceBidAsync(PlaceBid.Command command)
    {
        try
        {
            BidResult result = await _placeBidHandler.HandleAsync(command);
            _logger.LogInformation("Bid placed");
            return Ok(result);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Data);
        }
    }

    [HttpGet("open-auctions")]
    [ProducesResponseType<Status>(200)]
    public async Task<IActionResult> OpenAuctionsAsync()
    {
        var res = await _openAuctionsView.ReadStateAsync();
        return Ok(res);
    }
}
