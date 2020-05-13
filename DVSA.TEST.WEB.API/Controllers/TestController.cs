using System.Threading.Tasks;
using DVSA.MOT.SDK.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DVSA.TEST.WEB.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly ISingleVehicleService _motTests;
        public TestController(ISingleVehicleService motTests)
        {
            _motTests = motTests;
        }

        // GET api/values
        [HttpGet("GetVehicleByRegistration")]
        public async Task<ActionResult<string>> GetVehicleByRegistration(string registration)
        {
            var vehicleDetails = await _motTests.GetSingleVehicleMotHistoryByRegistration(registration);

            if (vehicleDetails != null)
            {
                return Ok(vehicleDetails);
            }

            return NotFound(registration);
        }
    }
}
