using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DVSA.MOT.SDK.Interfaces;
using DVSA.MOT.SDK.Models;
using Microsoft.Extensions.Logging;

namespace DVSA.MOT.SDK.Services
{
    public class AllVehiclesService : IAllVehiclesService
    {
        private readonly ILogger<SingleVehicleService> _logger;
        private readonly IProcessApiResponse _processApiResponse;

        public AllVehiclesService(ILogger<SingleVehicleService> logger, IProcessApiResponse processApiResponse)
        {
            _logger = logger;
            _processApiResponse = processApiResponse;
        }

        public async Task<ApiResponse> GetAllMotTests(int page)
        {
            try
            {
                var parameters = new List<KeyValuePair<string, string>>
                    {
                        new KeyValuePair<string, string>(Constants.Parameters.Page, page.ToString())
                    };
                return await _processApiResponse.GetData(parameters);
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, ex, ex.Message);
                return null;
            }
        }

        public async Task<ApiResponse> GetAllMotTestsByDate(DateTime date, int page)
        {
            try
            {
                var parameters = new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>(Constants.Parameters.Date, date.ToString("yyyyMMdd")),
                    new KeyValuePair<string, string>(Constants.Parameters.Page, page.ToString())
                };
                return await _processApiResponse.GetData(parameters);
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, ex, ex.Message);
                return null;
            }
        }
    }
}
