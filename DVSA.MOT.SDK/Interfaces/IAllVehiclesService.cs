using System;
using System.Threading.Tasks;
using DVSA.MOT.SDK.Models;

namespace DVSA.MOT.SDK.Interfaces
{
    public interface IAllVehiclesService
    {
        Task<ApiResponse> GetAllMotTests(int page);
        Task<ApiResponse> GetAllMotTestsByDate(DateTime date, int page);
    }
}
