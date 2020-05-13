using System;
using System.Threading.Tasks;
using DVSA.MOT.SDK.Models;

namespace DVSA.MOT.SDK.Interfaces
{
    public interface IAllVehiclesService
    {
        /// <summary>
        /// Get paginated full extract of the database
        /// </summary>
        /// <param name="page">[0-58002]</param>
        /// <returns>A full extract of the database.The last page normally increments by 10 each day.</returns>
        Task<ApiResponse> GetAllMotTests(int page);

        /// <summary>
        /// Get paginated extract of the database by date
        /// </summary>
        /// <param name="date">YYYYMMDD</param>
        /// <param name="page">[1-1440]</param>
        /// <returns>A extract of the database filtered by day</returns>
        Task<ApiResponse> GetAllMotTestsByDate(DateTime date, int page);
    }
}
