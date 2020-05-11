﻿using System;
using System.Threading.Tasks;
using DVSA.MOT.SDK.Models;

namespace DVSA.MOT.SDK.Interfaces
{
    public interface IMotTests
    {
        Task<VehicleDetails> GetSingleVehicleMotHistoryByRegistration(string registration);
        MotTestResponses GetSingleVehicleMotHistoryById(string id);
        MotTestResponses GetAllMotTests(int page);
        MotTestResponses GetAllMotTestsByDate(DateTime date, int page);
    }
}
