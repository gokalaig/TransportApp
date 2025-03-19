using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TransportApp.Server.DataModels;
using TransportApp.Server.Repository;

namespace TransportApp.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class TruckController : ControllerBase
    {
        private readonly ITruckRepository _truckRepository;

        public TruckController(ITruckRepository truckRepository)
        {
            _truckRepository = truckRepository;
        }

        [HttpGet("{truckNo}")]
        public async Task<IActionResult> GetTruck([FromHeader(Name = "Authorization")] string authToken,string truckNo)
        {
            var response = await _truckRepository.GetTruckByNumberAsync(authToken, truckNo);
            return StatusCode(response.status, response);
        }

        [HttpPost]
        public async Task<IActionResult> AddTruck([FromHeader(Name = "Authorization")] string authToken, [FromBody] TruckMaster truck)
        {
            var response = await _truckRepository.AddTruckAsync(authToken, truck);
            return StatusCode(response.status, response);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateTruck([FromHeader(Name = "Authorization")] string authToken, [FromBody] TruckMaster truck)
        {
            var response = await _truckRepository.UpdateTruckAsync(authToken , truck);
            return StatusCode(response.status, response);
        }
    }

}
