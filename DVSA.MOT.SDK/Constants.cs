namespace DVSA.MOT.SDK
{
    public static class Constants
    {
        public static string ApiRootUrl => "https://beta.check-mot.service.gov.uk/";
        public static string ApiPath => "trade/vehicles/mot-tests";
        public static string ApiAcceptHeader => "application/json+v6";
        public static string ApiKeyHeader => "x-api-key";

        public static class LanguageStrings
        {
            public static string MissingApiKey => "The x-api-key is missing or invalid in the header";
            public static string IncorrectContentType => "The Content-Type is wrong in the header";
            public static string TooManyRequests => "You have exceeded your rate or quota";
            public static string ServerError => "Please contact support and provide the request ID returned";
            public static string ServiceNotAvailable => "The service is not available";
            public static string GatewayTimeout => "The service did not respond in the time limit";
            public static class SingleVehicleMotHistory
            {
                public static string NullRegistrationException => "The registration number is missing";
                public static string VehicleNotFound =>
                    "Vehicle with the provided parameters was not found or its test records are not valid";
                public static string BadRequest => "Invalid data in the request. Check your URL and parameters";
            }

            public static class AllMotTestsByDate
            {
                public static string BadRequest =>
                    "Invalid data in the request. Check your URL and parameters. Ensure that if you have provided a date that it is less that 5 weeks before today";
            }
        }
    }
}
