using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DVSA.MOT.SDK.Interfaces;
using DVSA.MOT.SDK.Models;
using Microsoft.Extensions.Logging;

namespace DVSA.MOT.SDK.Services
{
    public class SingleVehicleService : ISingleVehicleService
    {
        private readonly IProcessApiResponse _processApiResponse;
        private readonly ILogger<SingleVehicleService> _logger;

        public SingleVehicleService(ILogger<SingleVehicleService> logger, IProcessApiResponse processApiResponse)
        {
            _logger = logger;
            _processApiResponse = processApiResponse;
        }

        public async Task<ApiResponse> GetSingleVehicleMotHistoryByRegistration(string registration)
        {
            try
            {
                var apiResponse = new ApiResponse();
                if (!string.IsNullOrEmpty(registration))
                {
                    var parameters = new List<KeyValuePair<string, string>>
                    {
                        new KeyValuePair<string, string>(Constants.Parameters.Registration, registration)
                    };
                    return await _processApiResponse.GetData(parameters);
                }

                var responseMessage = Constants.LanguageStrings.SingleVehicleMotHistory.NullRegistrationException;
                _logger.Log(LogLevel.Error, responseMessage);
                apiResponse.ResponseMessage = responseMessage;
                return apiResponse;
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, ex, ex.Message);
                return null;
            }
        }
        public async Task<ApiResponse> GetSingleVehicleMotHistoryById(string id)
        {
            try
            {
                var apiResponse = new ApiResponse();
                if (!string.IsNullOrEmpty(id))
                {
                    var parameters = new List<KeyValuePair<string, string>>
                    {
                        new KeyValuePair<string, string>(Constants.Parameters.VehicleId, id)
                    };
                    return await _processApiResponse.GetData(parameters);
                }

                var responseMessage = Constants.LanguageStrings.SingleVehicleMotHistory.NullRegistrationException;
                _logger.Log(LogLevel.Error, responseMessage);
                apiResponse.ResponseMessage = responseMessage;
                return apiResponse;
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, ex, ex.Message);
                return null;
            }
        }
    }
}
