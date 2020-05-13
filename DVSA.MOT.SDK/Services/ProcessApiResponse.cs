using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using DVSA.MOT.SDK.Interfaces;
using DVSA.MOT.SDK.Models;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace DVSA.MOT.SDK.Services
{
    public class ProcessApiResponse : IProcessApiResponse
    {
        private readonly ILogger<ProcessApiResponse> _logger;

        public ProcessApiResponse(ILogger<ProcessApiResponse> logger)
        {
            _logger = logger;
        }

        public async Task<List<VehicleDetails>> ConvertToObject(HttpContent httpContent)
        {
            try
            {
                if (httpContent == null) 
                    return null;

                var json = await httpContent.ReadAsStringAsync();
                if (string.IsNullOrEmpty(json)) 
                    return null;

                var motTestResponses = JsonConvert.DeserializeObject<List<VehicleDetails>>(json);
                return motTestResponses;
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, ex, ex.Message);
                return null;
            }
        }

        public string ResponseMessage(HttpStatusCode statusCode)
        {
            var responseMessage = string.Empty;
            try
            {
                switch (statusCode)
                {
                    case HttpStatusCode.OK:
                        responseMessage = Constants.LanguageStrings.Ok;
                        break;
                    case HttpStatusCode.NotFound:
                        responseMessage = Constants.LanguageStrings.SingleVehicleMotHistory.VehicleNotFound;
                        break;
                    case HttpStatusCode.BadRequest:
                        responseMessage = Constants.LanguageStrings.SingleVehicleMotHistory.BadRequest;
                        break;
                    case HttpStatusCode.Forbidden:
                        responseMessage = Constants.LanguageStrings.MissingApiKey;
                        break;
                    case HttpStatusCode.UnsupportedMediaType:
                        responseMessage = Constants.LanguageStrings.IncorrectContentType;
                        break;
                    case HttpStatusCode.InternalServerError:
                        responseMessage = Constants.LanguageStrings.ServerError;
                        break;
                    case HttpStatusCode.ServiceUnavailable:
                        responseMessage = Constants.LanguageStrings.ServiceNotAvailable;
                        break;
                    case HttpStatusCode.GatewayTimeout:
                        responseMessage = Constants.LanguageStrings.GatewayTimeout;
                        break;
                    default:
                        if ((int)statusCode == 429)
                        {
                            responseMessage = Constants.LanguageStrings.TooManyRequests;
                        }
                        break;
                }

                return responseMessage;
            }
            catch (Exception ex)
            {
                responseMessage = ex.Message;
                _logger.Log(LogLevel.Error, ex, ex.Message);
                return responseMessage;
            }
        }
    }
}
