using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using DVSA.MOT.SDK.Models;

namespace DVSA.MOT.SDK.Interfaces
{
    public interface IProcessApiResponse
    {
        Task<List<VehicleDetails>> ConvertToObject(HttpContent json);
        string ResponseMessage(HttpStatusCode statusCode);
    }
}