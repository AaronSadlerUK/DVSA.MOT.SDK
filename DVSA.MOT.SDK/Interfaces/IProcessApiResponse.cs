using System.Collections.Generic;
using System.Threading.Tasks;
using DVSA.MOT.SDK.Models;

namespace DVSA.MOT.SDK.Interfaces
{
    public interface IProcessApiResponse
    {
        /// <summary>
        /// Returns an object containing the results from the database
        /// </summary>
        /// <param name="parameters">Query string parameters</param>
        /// <returns></returns>
        Task<ApiResponse> GetData(List<KeyValuePair<string, string>> parameters);
    }
}