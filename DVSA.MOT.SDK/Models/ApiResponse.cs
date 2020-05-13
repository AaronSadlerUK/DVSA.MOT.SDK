using System.Collections.Generic;

namespace DVSA.MOT.SDK.Models
{
    public class ApiResponse
    {
        public int StatusCode { get; set; }
        public string ResponseMessage { get; set; }
        public string ReasonPhrase { get; set; }
        public List<VehicleDetails> VehicleDetails { get; set; }
    }
}
