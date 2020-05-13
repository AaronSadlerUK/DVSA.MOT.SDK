using System;
using DVSA.MOT.SDK.Interfaces;
using DVSA.MOT.SDK.Models;

namespace DVSA.MOT.SDK.Services
{
    public class AllVehiclesService : IAllVehiclesService
    {
        public MotTestResponses GetAllMotTests(int page)
        {
            throw new NotImplementedException();
        }

        public MotTestResponses GetAllMotTestsByDate(DateTime date, int page)
        {
            throw new NotImplementedException();
        }
    }
}
