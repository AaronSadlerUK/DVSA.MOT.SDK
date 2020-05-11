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
    public class MotTests : IMotTests
    {
        private readonly IOptions<ApiKey> _apiKey;
        private readonly ILogger<MotTests> _logger;

        public MotTests(IOptions<ApiKey> apiKey, ILogger<MotTests> logger)
        {
            _apiKey = apiKey;
            _logger = logger;
        }

        public async Task<VehicleDetails> GetSingleVehicleMotHistoryByRegistration(string registration)
        {
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
                        if (request.IsSuccessStatusCode)
                        {
                            var response = await request.Content.ReadAsStringAsync();
                            if (!string.IsNullOrEmpty(response))
                            {
                                var motTestResponses = JsonConvert.DeserializeObject<List<VehicleDetails>>(response);
                                if (motTestResponses != null && motTestResponses.Any())
                                {
                                    var vehicleDetails = motTestResponses.FirstOrDefault();
                                    return vehicleDetails;
                                }
                            }
                        }
                        else if ((int)request.StatusCode == 429)
                        {
                            errorMessage = $"{Constants.LanguageStrings.TooManyRequests} - {request.ReasonPhrase}";
                            _logger.Log(LogLevel.Critical, errorMessage);
                            throw new Exception(errorMessage);
                        }
                        else
                        {
                            switch (request.StatusCode)
                            {
                                case HttpStatusCode.NotFound:
                                    errorMessage = $"{Constants.LanguageStrings.SingleVehicleMotHistory.VehicleNotFound} - {request.ReasonPhrase}";
                                    _logger.Log(LogLevel.Critical, errorMessage);
                                    throw new Exception(errorMessage);
                                case HttpStatusCode.BadRequest:
                                    errorMessage = $"{Constants.LanguageStrings.SingleVehicleMotHistory.BadRequest} - {request.ReasonPhrase}";
                                    _logger.Log(LogLevel.Critical, errorMessage);
                                    throw new Exception(errorMessage);
                                case HttpStatusCode.Forbidden:
                                    errorMessage = $"{Constants.LanguageStrings.MissingApiKey} - {request.ReasonPhrase}";
                                    _logger.Log(LogLevel.Critical, errorMessage);
                                    throw new Exception(errorMessage);
                                case HttpStatusCode.UnsupportedMediaType:
                                    errorMessage = $"{Constants.LanguageStrings.IncorrectContentType} - {request.ReasonPhrase}";
                                    _logger.Log(LogLevel.Critical, errorMessage);
                                    throw new Exception(errorMessage);
                                case HttpStatusCode.InternalServerError:
                                    errorMessage = $"{Constants.LanguageStrings.ServerError} - {request.ReasonPhrase}";
                                    _logger.Log(LogLevel.Critical, errorMessage);
                                    throw new Exception(errorMessage);
                                case HttpStatusCode.ServiceUnavailable:
                                    errorMessage = $"{Constants.LanguageStrings.ServiceNotAvailable} - {request.ReasonPhrase}";
                                    _logger.Log(LogLevel.Critical, errorMessage);
                                    throw new Exception(errorMessage);
                                case HttpStatusCode.GatewayTimeout:
                                    errorMessage = $"{Constants.LanguageStrings.GatewayTimeout} - {request.ReasonPhrase}";
                                    _logger.Log(LogLevel.Critical, errorMessage);
                                    throw new Exception(errorMessage);
                                default:
                                    errorMessage = $"{request.StatusCode} - {request.ReasonPhrase}";
                                    _logger.Log(LogLevel.Critical, errorMessage);
                                    throw new Exception(errorMessage);
                            }
                        }
                    }
                }
            }

            errorMessage = Constants.LanguageStrings.SingleVehicleMotHistory.NullRegistrationException;
            _logger.Log(LogLevel.Critical, errorMessage);
            throw new Exception(errorMessage);
        }

        public MotTestResponses GetSingleVehicleMotHistoryById(string id)
        {
            throw new NotImplementedException();
        }

        public MotTestResponses GetAllMotTests(int page)
        {
            throw new NotImplementedException();
        }

        public MotTestResponses GetAllMotTestsByDate(DateTime date, int page)
        {
            throw new NotImplementedException();
        }
    }
}
