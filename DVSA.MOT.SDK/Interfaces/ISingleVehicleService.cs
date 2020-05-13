using System;
using System.Threading.Tasks;
using DVSA.MOT.SDK.Models;

namespace DVSA.MOT.SDK.Interfaces
{
    public interface ISingleVehicleService
    {
        Task<ApiResponse> GetSingleVehicleMotHistoryByRegistration(string registration);
        MotTestResponses GetSingleVehicleMotHistoryById(string id);
    }
}
