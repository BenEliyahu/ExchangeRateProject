using Microsoft.AspNetCore.Mvc;
using RatePrinter.Models;
using RatePrinter.Services;

namespace RatePrinter.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ExchangeRateController : ControllerBase
    {
        private readonly ExchangeRateService _service;

        public ExchangeRateController(ExchangeRateService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<ExchangeRate>>> GetAllRates()
        {
            var rates = await _service.GetAllRatesAsync();
            return Ok(rates);
        }

        [HttpGet("{*pairName}")]
        public async Task<ActionResult<ExchangeRate>> GetRate(string pairName)
        {
            var rate = await _service.GetRateByPairAsync(pairName);
            if (rate == null)
                return NotFound();

            return Ok(rate);
        }
    }
}
