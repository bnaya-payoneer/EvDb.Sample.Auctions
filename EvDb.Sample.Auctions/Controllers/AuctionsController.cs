using Microsoft.AspNetCore.Mvc;

namespace EvDb.Sample.Auctions.Controllers;
[ApiController]
[Route("[controller]")]
public class AuctionsController : ControllerBase
{
    private readonly ILogger<AuctionsController> _logger;

    public AuctionsController(ILogger<AuctionsController> logger)
    {
        _logger = logger;
    }

    [HttpPost]
    public Task PostAsync()
    {
        throw new NotImplementedException();
    }
}
