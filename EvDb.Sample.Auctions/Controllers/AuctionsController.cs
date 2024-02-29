using EvDb.Sample.Auctions.Abstractions.Views.AuctionStatus;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Immutable;
using CloseAuction = EvDb.Sample.Auctions.CommandsHandlers.CloseAuction;
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
    private readonly CloseAuction.IHandler _closeAuctionHandler;
    private readonly Projectors.OpenAuctions.IView _openAuctionsView;

    public AuctionsController(
        ILogger<AuctionsController> logger,
        CreateAuction.IHandler createAuctionHandler,
        PlaceBid.IHandler placeBidHandler,
        CloseAuction.IHandler closeAuctionHandler,
        Projectors.OpenAuctions.IView openAuctionsView)
    {
        _logger = logger;
        _createAuctionHandler = createAuctionHandler;
        _placeBidHandler = placeBidHandler;
        _closeAuctionHandler = closeAuctionHandler;
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
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> PlaceBidAsync(PlaceBid.Command command)
    {
        try
        {
            await _placeBidHandler.HandleAsync(command);
            return Ok();
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Data);
        }
    }

    //[HttpPost("close-auction")]
    //[ProducesResponseType<State>(200)]
    //public async Task<IActionResult> CloseAuctionAsync(CloseAuction.Command command)
    //{
    //    var state = await _closeAuctionHandler.HandleAsync(command);
    //    return Ok(state);
    //}

    [HttpGet("open-auctions")]
    [ProducesResponseType<Status>(200)]
    public async Task<IActionResult> OpenAuctionsAsync()
    {
        var res = await _openAuctionsView.ReadStateAsync();
        return Ok(res);
    }
}
