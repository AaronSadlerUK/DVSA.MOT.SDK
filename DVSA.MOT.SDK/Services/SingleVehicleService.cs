using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using DVSA.MOT.SDK.Interfaces;
using DVSA.MOT.SDK.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace DVSA.MOT.SDK.Services
{
    public class SingleVehicleService : ISingleVehicleService
    {
        private readonly IOptions<ApiKey> _apiKey;
        private readonly ILogger<SingleVehicleService> _logger;

        public SingleVehicleService(IOptions<ApiKey> apiKey, ILogger<SingleVehicleService> logger)
        {
            _apiKey = apiKey;
            _logger = logger;
        }

        public async Task<ApiResponse> GetSingleVehicleMotHistoryByRegistration(string registration)
        {
            var apiResponse = new ApiResponse();
            string errorMessage;
            if (!string.IsNullOrEmpty(registration))
            {
                using (var httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Clear();
                    httpClient.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue(Constants.ApiAcceptHeader));
                    httpClient.DefaultRequestHeaders.Add(Constants.ApiKeyHeader, _apiKey.Value.DVSAApiKey);

                    if (!string.IsNullOrEmpty(registration))
                    {
                        var request = await httpClient.GetAsync($"{Constants.ApiRootUrl}{Constants.ApiPath}?registration={registration}");
                        apiResponse.ReasonPhrase = request.ReasonPhrase;
                        apiResponse.StatusCode = (int)request.StatusCode;
                        string responseMessage;
                        switch (request.StatusCode)
                        {
                            case HttpStatusCode.OK:
                                errorMessage = $"{Constants.LanguageStrings.Ok} - {request.ReasonPhrase}";
                                _logger.Log(LogLevel.Information, errorMessage);
                                responseMessage = errorMessage;
                                break;
                            case HttpStatusCode.NotFound:
                                errorMessage = $"{Constants.LanguageStrings.SingleVehicleMotHistory.VehicleNotFound} - {request.ReasonPhrase}";
                                _logger.Log(LogLevel.Information, errorMessage);
                                responseMessage = errorMessage;
                                break;
                            case HttpStatusCode.BadRequest:
                                errorMessage = $"{Constants.LanguageStrings.SingleVehicleMotHistory.BadRequest} - {request.ReasonPhrase}";
                                _logger.Log(LogLevel.Critical, errorMessage);
                                responseMessage = errorMessage;
                                break;
                            case HttpStatusCode.Forbidden:
                                errorMessage = $"{Constants.LanguageStrings.MissingApiKey} - {request.ReasonPhrase}";
                                _logger.Log(LogLevel.Critical, errorMessage);
                                responseMessage = errorMessage;
                                break;
                            case HttpStatusCode.UnsupportedMediaType:
                                errorMessage = $"{Constants.LanguageStrings.IncorrectContentType} - {request.ReasonPhrase}";
                                _logger.Log(LogLevel.Critical, errorMessage);
                                responseMessage = errorMessage;
                                break;
                            case HttpStatusCode.InternalServerError:
                                errorMessage = $"{Constants.LanguageStrings.ServerError} - {request.ReasonPhrase}";
                                _logger.Log(LogLevel.Critical, errorMessage);
                                responseMessage = errorMessage;
                                break;
                            case HttpStatusCode.ServiceUnavailable:
                                errorMessage = $"{Constants.LanguageStrings.ServiceNotAvailable} - {request.ReasonPhrase}";
                                _logger.Log(LogLevel.Critical, errorMessage);
                                responseMessage = errorMessage;
                                break;
                            case HttpStatusCode.GatewayTimeout:
                                errorMessage = $"{Constants.LanguageStrings.GatewayTimeout} - {request.ReasonPhrase}";
                                _logger.Log(LogLevel.Critical, errorMessage);
                                responseMessage = errorMessage;
                                break;
                            default:
                                if ((int)request.StatusCode == 429)
                                {
                                    errorMessage = $"{Constants.LanguageStrings.TooManyRequests} - {request.ReasonPhrase}";
                                    _logger.Log(LogLevel.Critical, errorMessage);
                                    responseMessage = errorMessage;
                                }
                                else
                                {
                                    errorMessage = $"{request.StatusCode} - {request.ReasonPhrase}";
                                    _logger.Log(LogLevel.Critical, errorMessage);
                                    responseMessage = errorMessage;
                                }
                                break;
                        }
                        var jsonResponse = await request.Content.ReadAsStringAsync();
                        if (!string.IsNullOrEmpty(jsonResponse))
                        {
                            var motTestResponses = JsonConvert.DeserializeObject<List<VehicleDetails>>(jsonResponse);
                            apiResponse.VehicleDetails = motTestResponses;
                        }
                    }
                }
            }

            errorMessage = Constants.LanguageStrings.SingleVehicleMotHistory.NullRegistrationException;
            _logger.Log(LogLevel.Critical, errorMessage);
            apiResponse.ResponseMessage = errorMessage;
            return apiResponse;
        }

        public MotTestResponses GetSingleVehicleMotHistoryById(string id)
        {
            throw new NotImplementedException();
        }
    }
}
