using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using DVSA.MOT.SDK.Interfaces;
using DVSA.MOT.SDK.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace DVSA.TEST.WEB.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly IMotTests _motTests;
        private IOptions<ApiKey> _apiKey;
        public TestController(IMotTests motTests, IOptions<ApiKey> apiKey)
        {
            _motTests = motTests;
            _apiKey = apiKey;
        }

        // GET api/values
        [HttpGet]
        public async Task<ActionResult<string>> GetVehicle(string registration)
        {
            var vehicleDetails = await _motTests.GetSingleVehicleMotHistoryByRegistration(registration);

            if (vehicleDetails != null)
            {
                return Ok(vehicleDetails);
            }

            return NotFound(registration);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
