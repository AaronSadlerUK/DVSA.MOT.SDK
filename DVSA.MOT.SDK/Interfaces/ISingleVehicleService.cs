using System;
using System.Threading.Tasks;
using DVSA.MOT.SDK.Models;

namespace DVSA.MOT.SDK.Interfaces
{
    public interface ISingleVehicleService
    {
        Task<ApiResponse> GetSingleVehicleMotHistoryByRegistration(string registration);
        Task<ApiResponse> GetSingleVehicleMotHistoryById(string id);
    }
}
