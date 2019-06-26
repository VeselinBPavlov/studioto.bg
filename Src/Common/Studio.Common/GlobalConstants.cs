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
        public const string IndustryPossition = "Hairstyler";
        public const string IndustryNotFoundExceptionMessage = "Entity \"Industry\" ({0}) was not found.";
        public const string IndustryDeleteFalueExceptionMessage = "Deletion of entity \"Industry\" ({0}) failed. There are existing services associated with this industry.";

        // Service
        public const string ServiceValidName = "Haircut";

        // Client
        public const string ClientValidName = "VND Group";
        public const string ClientSecondValidName = "CET";
        public const string ClientThirdValidName = "Beauty";

        public const string ClientValidVatNumber = "BG114564897";

        public const string ClientValidPhone = "+359887889884";

        public const string ClientValidManagerFirstName = "Ivan";

        public const string ClientValidManagerLastName = "Ivanov";
      
        public const string ClientNotFoundExceptionMessage = "Entity \"Client\" ({0}) was not found.";
        public const string ClientDeleteFalueExceptionMessage = "Deletion of entity \"Client\" ({0}) failed. There are existing locations associated with this client.";
        public const string ClientDeleteFalueIsDeletedTrue = "Deletion of entity \"Client\" ({0}) failed. Client is already deleted.";


        // Location
        public const string LocationValidName = "CentralStudio";


        #endregion

    }
}
