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
        public const string IndustryDeleteFalueExceptionMessageService = "Deletion of entity \"Industry\" ({0}) failed. There are existing services associated with this industry.";
        public const string IndustryDeleteFalueExceptionMessageLocation = "Deletion of entity \"Industry\" ({0}) failed. There are existing locations associated with this industry.";
        
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

        // Countries
        public const string CountryValidName = "Bulgaria";
        public const string CountrySecondValidName = "Greece";
        public const string CountryDeleteFalueExceptionMessage = "Deletion of entity \"Country\" ({0}) failed. There are existing cities associated with this country.";
        public const string CountryNotFoundExceptionMessage = "Entity \"Country\" ({0}) was not found.";
        public const string CountryCreateFailureExceptionMessage = "Creation of entity \"Country\" ({0}) failed. There are existing country with the same name.";
        public const string CountryCreateFailureExceptionMessageIsDeleted = "Creation of entity \"Country\" ({0}) failed. Country with id {0} is deleted.";
        public const string CountryUpdateFailureExceptionMessage = "Update of entity \"Country\" ({0}) failed. There are existing country with the same name.";

        // Cities
        public const string CityValidName = "Sofia";
        public const string CityCreateFailureExceptionMessageIsNull = "Creation of entity \"City\" ({0}) failed. There are no existing country with id {1}.";
        public const string CityCreateFailureExceptionMessageIsDeleted = "Creation of entity \"City\" ({0}) failed. Country with id {1} is deleted.";
        public const string CityDeleteFalueExceptionMessage = "Deletion of entity \"City\" ({0}) failed. There are existing address associated with this city.";
        public const string CityNotFoundExceptionMessage = "Entity \"City\" ({0}) was not found.";
        public const string CityDeleteFalueExceptionMessageIsDeleted = "Deletion of entity \"City\" ({0}) failed. City is already deleted.";

        public const string AddressFromatExceptionMessage = "Address is invalid.";
        public const string AddressCreateFailureExceptionMessageCityNotFound = "Creation of entity \"Address\" ({0}) failed. There are no existing city with id {1}.";
        public const string AddressUpdateFailureExceptionCityInvalidId = "Update of entity \"Address\" ({0}) failed. There are no existing city with this id {1}.";
        public const string AddressNotFoundExceptionMessage = "Entity \"Address\" ({0}) was not found.";
        public const string AddressDeleteFailureExceptionMessage = "Deletion of entity \"Address\" ({0}) failed. There are existing location associated with this address.";

        #endregion

    }
}
