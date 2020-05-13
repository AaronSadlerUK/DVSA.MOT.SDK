using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using DVSA.MOT.SDK.Interfaces;
using DVSA.MOT.SDK.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace DVSA.MOT.SDK.Services
{
    public class ProcessApiResponse : IProcessApiResponse
    {
        private readonly IOptions<ApiKey> _apiKey;
        private readonly ILogger<ProcessApiResponse> _logger;

        public ProcessApiResponse(ILogger<ProcessApiResponse> logger, IOptions<ApiKey> apiKey)
        {
            _logger = logger;
            _apiKey = apiKey;
        }

        /// <summary>
        /// Returns an object containing the results from the database
        /// </summary>
        /// <param name="parameters">Query string parameters</param>
        /// <returns></returns>
        public async Task<ApiResponse> GetData(List<KeyValuePair<string, string>> parameters)
        {
            var apiResponse = new ApiResponse();
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue(Constants.ApiAcceptHeader));
                httpClient.DefaultRequestHeaders.Add(Constants.ApiKeyHeader, _apiKey.Value.DVSAApiKey);

                var url = GenerateUrl(parameters);
                var request = await httpClient.GetAsync(url);
                apiResponse.ReasonPhrase = request.ReasonPhrase;
                apiResponse.StatusCode = (int)request.StatusCode;
                var responseMessage = ResponseMessage(request.StatusCode);
                apiResponse.ResponseMessage = responseMessage;
                apiResponse.VehicleDetails = await ConvertToObject(request.Content);

                if (!request.IsSuccessStatusCode)
                {
                    _logger.Log(LogLevel.Error, responseMessage);
                }
                return apiResponse;
            }
        }

        /// <summary>
        /// Convert the json response to ApiResponse object
        /// </summary>
        /// <param name="httpContent">HttpClient response content</param>
        /// <returns>ApiResponse object</returns>
        private async Task<List<VehicleDetails>> ConvertToObject(HttpContent httpContent)
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

        /// <summary>
        /// Convert the status code response into a valid DVSA api response
        /// </summary>
        /// <param name="statusCode"></param>
        /// <returns></returns>
        private string ResponseMessage(HttpStatusCode statusCode)
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

        /// <summary>
        /// Generate a url with parameters for the HttpClient request
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        private static string GenerateUrl(IEnumerable<KeyValuePair<string, string>> parameters)
        {
            var baseURl = $"{Constants.ApiRootUrl}{Constants.ApiPath}";
            var uriBuilder = new UriBuilder(baseURl);
            var query = HttpUtility.ParseQueryString(uriBuilder.Query);
            foreach (var param in parameters)
            {
                query.Add(param.Key, param.Value);
            }
            uriBuilder.Query = query.ToString();
            baseURl = uriBuilder.ToString();
            return baseURl;
        }
    }
}
