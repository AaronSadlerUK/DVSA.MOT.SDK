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

        /// <summary>
        /// Get paginated full extract of the database
        /// </summary>
        /// <param name="page">[0-58002]</param>
        /// <returns>A full extract of the database.The last page normally increments by 10 each day.</returns>
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

        /// <summary>
        /// Get paginated extract of the database by date
        /// </summary>
        /// <param name="date">YYYYMMDD</param>
        /// <param name="page">[1-1440]</param>
        /// <returns>A extract of the database filtered by day</returns>
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
