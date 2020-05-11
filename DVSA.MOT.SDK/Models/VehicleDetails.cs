namespace DVSA.MOT.SDK.Models
{
    public class VehicleDetails
    {
        public string Registration { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public string FirstUsedDate { get; set; }
        public string FuelType { get; set; }
        public string PrimaryColour { get; set; }
        public string VehicleId { get; set; }
        public string RegistrationDate { get; set; }
        public string ManufactureDate { get; set; }
        public string EngineSize { get; set; }
        public MotTest[] MotTests { get; set; }
    }
}