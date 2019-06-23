namespace Studio.Common
{
    public static class GlobalConstants
    {
        #region Studio.Application.Tests

        // Valid for all entities
        public const string SuccessStatus = "RanToCompletion";
        public const string InvalidName = "AbcdefghijAbcdefghijAbcdefghijAbcdefghijAbcdefghijAbcdefghijAbcdefghijAbcdefghijAbcdefghijAbcdefghijAbcdefghij";
        public const int InvalidId = 100;

        // Industry
        public const string IndustryValidName = "Hairstyle";
        public const string IndustrySecondValidName = "Fitness";
        public const string IndustryNotFoundExceptionMessage = "Entity \"Industry\" ({0}) was not found.";
        public const string IndustryDeleteFalueExceptionMessage = "Deletion of entity \"Service\" ({0}) failed. There are existing services associated with this industry.";

        // Service
        public const string ServiceValidName = "Haircut";

        // Client
        public const string ClientValidName = "VND Group";
        public const string ClientSecondValidName = "CET";
        public const string ClientNotFoundExceptionMessage = "Entity \"Client\" ({0}) was not found.";
        public const string ClientDeleteFalueExceptionMessage = "Deletion of entity \"Location\" ({0}) failed. There are existing locations associated with this client.";

        // Location
        public const string LocationValidName = "CentralStudio";


        #endregion

    }
}
