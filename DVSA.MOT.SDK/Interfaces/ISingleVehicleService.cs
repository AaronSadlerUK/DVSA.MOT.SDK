using System;
using System.Threading.Tasks;
using DVSA.MOT.SDK.Models;

namespace DVSA.MOT.SDK.Interfaces
{
    public interface ISingleVehicleService
    {
        /// <summary>
        /// To request the MOT test history by vehicle registration
        /// </summary>
        /// <param name="registration">Registration of vehicle</param>
        /// <returns>A single vehicles details and mot history</returns>
        Task<ApiResponse> GetSingleVehicleMotHistoryByRegistration(string registration);

        /// <summary>
        /// To request the MOT test history by vehicle id
        /// </summary>
        /// <param name="id">Id of vehicle</param>
        /// <returns>A single vehicles details and mot history</returns>
        Task<ApiResponse> GetSingleVehicleMotHistoryById(string id);
    }
}
