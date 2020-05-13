using System.Collections.Generic;
using System.Threading.Tasks;
using DVSA.MOT.SDK.Models;

namespace DVSA.MOT.SDK.Interfaces
{
    public interface IProcessApiResponse
    {
        Task<ApiResponse> GetData(List<KeyValuePair<string, string>> parameters);
    }
}