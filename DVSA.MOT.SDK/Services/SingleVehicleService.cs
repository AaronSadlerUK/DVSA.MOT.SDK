using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using DVSA.MOT.SDK.Interfaces;
using DVSA.MOT.SDK.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace DVSA.MOT.SDK.Services
{
    public class SingleVehicleService : ISingleVehicleService
    {
        private readonly IProcessApiResponse _processApiResponse;
        private readonly IOptions<ApiKey> _apiKey;
        private readonly ILogger<SingleVehicleService> _logger;

        public SingleVehicleService(IOptions<ApiKey> apiKey, ILogger<SingleVehicleService> logger, IProcessApiResponse processApiResponse)
        {
            _apiKey = apiKey;
            _logger = logger;
            _processApiResponse = processApiResponse;
        }

        public async Task<ApiResponse> GetSingleVehicleMotHistoryByRegistration(string registration)
        {
            try
            {
                var apiResponse = new ApiResponse();
                string responseMessage;
                if (!string.IsNullOrEmpty(registration))
                {
                    using (var httpClient = new HttpClient())
                    {
                        httpClient.DefaultRequestHeaders.Clear();
                        httpClient.DefaultRequestHeaders.Accept.Add(
                            new MediaTypeWithQualityHeaderValue(Constants.ApiAcceptHeader));
                        httpClient.DefaultRequestHeaders.Add(Constants.ApiKeyHeader, _apiKey.Value.DVSAApiKey);

                        var request = await httpClient.GetAsync($"{Constants.ApiRootUrl}{Constants.ApiPath}?{Constants.Parameters.Registration}={registration}");
                        apiResponse.ReasonPhrase = request.ReasonPhrase;
                        apiResponse.StatusCode = (int)request.StatusCode;
                        responseMessage = _processApiResponse.ResponseMessage(request.StatusCode);
                        apiResponse.ResponseMessage = responseMessage;
                        apiResponse.VehicleDetails = await _processApiResponse.ConvertToObject(request.Content);

                        if (!request.IsSuccessStatusCode)
                        {
                            _logger.Log(LogLevel.Error, responseMessage);
                        }
                        return apiResponse;
                    }
                }

                responseMessage = Constants.LanguageStrings.SingleVehicleMotHistory.NullRegistrationException;
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
                string responseMessage;
                if (!string.IsNullOrEmpty(id))
                {
                    using (var httpClient = new HttpClient())
                    {
                        httpClient.DefaultRequestHeaders.Clear();
                        httpClient.DefaultRequestHeaders.Accept.Add(
                            new MediaTypeWithQualityHeaderValue(Constants.ApiAcceptHeader));
                        httpClient.DefaultRequestHeaders.Add(Constants.ApiKeyHeader, _apiKey.Value.DVSAApiKey);

                        var request = await httpClient.GetAsync($"{Constants.ApiRootUrl}{Constants.ApiPath}?{Constants.Parameters.Registration}={id}");
                        apiResponse.ReasonPhrase = request.ReasonPhrase;
                        apiResponse.StatusCode = (int)request.StatusCode;
                        responseMessage = _processApiResponse.ResponseMessage(request.StatusCode);
                        apiResponse.ResponseMessage = responseMessage;
                        apiResponse.VehicleDetails = await _processApiResponse.ConvertToObject(request.Content);

                        if (!request.IsSuccessStatusCode)
                        {
                            _logger.Log(LogLevel.Error, responseMessage);
                        }
                        return apiResponse;
                    }
                }

                responseMessage = Constants.LanguageStrings.SingleVehicleMotHistory.NullRegistrationException;
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
