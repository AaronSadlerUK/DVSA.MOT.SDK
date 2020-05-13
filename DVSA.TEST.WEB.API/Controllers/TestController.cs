using System;
using System.Globalization;
using System.Threading.Tasks;
using DVSA.MOT.SDK.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DVSA.TEST.WEB.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly ISingleVehicleService _singleVehicleService;
        private readonly IAllVehiclesService _allVehiclesService;
        public TestController(ISingleVehicleService singleVehicleService, IAllVehiclesService allVehiclesService)
        {
            _singleVehicleService = singleVehicleService;
            _allVehiclesService = allVehiclesService;
        }

        // GET api/Test/GetVehicleByRegistration?registration=ZZ99ABC
        // To request the MOT test history for registration
        [HttpGet("GetVehicleByRegistration")]
        public async Task<ActionResult<string>> GetVehicleByRegistration(string registration)
        {
            var vehicleDetails = await _singleVehicleService.GetSingleVehicleMotHistoryByRegistration(registration);

            if (vehicleDetails != null)
            {
                return Ok(vehicleDetails);
            }

            return NotFound(registration);
        }

        // GET api/Test/GetVehicleById?vehicleId=4Tq319nVKLz+25IRaUo79w==
        // To request the MOT test history for a vehicle with the ID.
        [HttpGet("GetVehicleById")]
        public async Task<ActionResult<string>> GetVehicleById(string vehicleId)
        {
            var vehicleDetails = await _singleVehicleService.GetSingleVehicleMotHistoryById(vehicleId);

            if (vehicleDetails != null)
            {
                return Ok(vehicleDetails);
            }

            return NotFound(vehicleId);
        }

        // GET api/Test/GetAllVehicles?page=0
        // To request a full extract of the database.The last page normally increments by 10 each day.
        [HttpGet("GetAllVehicles")]
        public async Task<ActionResult<string>> GetAllVehicles(string page)
        {
            var vehicleDetails = await _allVehiclesService.GetAllMotTests(Convert.ToInt32(page));

            if (vehicleDetails != null)
            {
                return Ok(vehicleDetails);
            }

            return NotFound();
        }

        // GET api/Test/GetAllVehicles?date=20200513&page=0
        // To request MOT tests completed on 10 March 2017 from page 1 to 1440.

        //Example

        //    page 1 shows all tests completed on 10 March 2017 at the 10/03/2017 at 0:01am
        //    page 300 shows all tests completed on 10 March 2017 at 5am
        //    page 600 shows all tests completed on 10 March 2017 at 10am
        [HttpGet("GetAllVehiclesByDate")]
        public async Task<ActionResult<string>> GetAllVehiclesByDate(string date, string page)
        {
            var dateTime = DateTime.ParseExact(date, "yyyyMMdd", CultureInfo.InvariantCulture);
            var vehicleDetails = await _allVehiclesService.GetAllMotTestsByDate(dateTime,Convert.ToInt32(page));

            if (vehicleDetails != null)
            {
                return Ok(vehicleDetails);
            }

            return NotFound();
        }
    }
}
