using System;
using DVSA.MOT.SDK.Models;

namespace DVSA.MOT.SDK.Interfaces
{
    public interface IAllVehiclesService
    {
        MotTestResponses GetAllMotTests(int page);
        MotTestResponses GetAllMotTestsByDate(DateTime date, int page);
    }
}
