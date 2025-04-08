using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TransportApp.Server.Repository;

namespace TransportApp.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CommissionBillController : ControllerBase
    {
        private readonly ICommissionBillRepository _commissionBillService;

        public CommissionBillController(ICommissionBillRepository commissionBillService)
        {
            _commissionBillService = commissionBillService;
        }

        [HttpGet("GetLoad/{lcNo}")]
        public async Task<IActionResult> GetLoad(string lcNo)
        {
            if (string.IsNullOrWhiteSpace(lcNo))
            {
                return BadRequest("LC Number cannot be empty.");
            }

            var result = await _commissionBillService.GetLoadAsync(lcNo);

            if (result == null)
            {
                return NotFound("No data found for the given LC Number.");
            }

            return Ok(result);
        }
    }

}
