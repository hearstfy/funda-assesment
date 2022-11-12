using FundaAssesment.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace FundaAssesment.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FundaController : ControllerBase
    {
        private readonly IFundaService _service;

        public FundaController(IFundaService service)
        {
            _service = service;
        }

        [HttpGet("top10")]
        public async Task<IActionResult> GetList()
        {
            var response = await _service.GetTop10("amsterdam", "koop");
            if (!response.IsSuccess)
                return Problem(response.ErrorMessage);
            return Ok(response);
        }

        [HttpGet("top10garden")]
        public async Task<IActionResult> GetListGarden()
        {
            var response = await _service.GetTop10("amsterdam/tuin", "koop");
            if (!response.IsSuccess)
                return Problem(response.ErrorMessage);
            return Ok(response);
        }
    }
}
