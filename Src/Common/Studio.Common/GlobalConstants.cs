namespace Studio.Common
{
    public static class GlobalConstants
    {
        #region Studio.Application.Core

        public const string BaseException = "One or more validation failures have occurred.";
        public const string FailureException = "{0} of entity \"{1}\" ({2}) failed. {3}";
        public const string NotFoundException = "Entity \"{0}\" ({1}) was not found.";
        public const string DeleteException = "There are existing {0} associated with this {1}.";
        public const string RefereceException = "There are no existing {0} with id {1}.";
        public const string UniqueNameException = "There are existing {0} with the same name.";
               
        #endregion

        #region Studio.Application.Domain

        public const string ValueObjectException = "{0} is invalid.";
        public const string AddressFormatException = "Street and number are required!";
        public const string ManagerException = "First and last name are with incorrect format!";

        #endregion

        #region Studio.Application.Tests

        // Valid for all entities
        public const string SuccessStatus = "RanToCompletion";
        public const string InvalidName = "AbcdefghijAbcdefghijAbcdefghijAbcdefghijAbcdefghijAbcdefghijAbcdefghijAbcdefghijAbcdefghijAbcdefghijAbcdefghij";
        public const int InvalidId = 100;
        public const int ValidId = 1;
        public const string FirstValidName = "Fitness";
        public const string SecondValidName = "Haircut";
        public const string ThirdValidName = "CET";
        public const string NotFoundExceptionMessage = "Entity \"{0}\" ({1}) was not found.";
        public const string DeleteFailureExceptionMessage = "Deletion of entity \"{0}\" ({1}) failed. There are existing {2} associated with this {3}.";
        public const string UpdateFailureExceptionMessage = "Update of entity \"{0}\" ({1}) failed. There are no existing {2} with id {3}.";

        // Industry
        public const string IndustryValidName = "Hairstyle";
        public const string IndustrySecondValidName = "Fitness";
        public const string IndustryPossition = "Hairstyler";
       
        
        
        // Service
        public const string ServiceValidName = "Haircut";
        public const string ServiceCreateFailureExceptionMessageIsNull = "Creation of entity \"Service\" ({0}) failed. There are no existing industry with id {1}.";
        
        


        // Client
        public const string ClientValidName = "VND Group";
        public const string ClientSecondValidName = "CET";
        public const string ClientThirdValidName = "Beauty";
        public const string ClientValidVatNumber = "BG114564897";
        public const string ClientValidPhone = "+359887889884";
        public const string ClientValidManagerFirstName = "Ivan";
        public const string ClientValidManagerLastName = "Ivanov";      
        
        
                     
        // Location
        public const string LocationValidName = "CentralStudio";

        // Countries
        public const string CountryValidName = "Bulgaria";
        public const string CountrySecondValidName = "Greece";
        
        
        public const string CountryCreateFailureExceptionMessage = "Creation of entity \"Country\" ({0}) failed. There are existing country with the same name.";
        public const string CountryCreateFailureExceptionMessageIsDeleted = "Creation of entity \"Country\" ({0}) failed. Country with id {0} is deleted.";
        public const string CountryUpdateFailureExceptionMessage = "Update of entity \"Country\" ({0}) failed. There are existing country with the same name.";

        // Cities
        public const string CityValidName = "Sofia";
        public const string CityCreateFailureExceptionMessageIsNull = "Creation of entity \"City\" ({0}) failed. There are no existing country with id {1}.";
        public const string CityCreateFailureExceptionMessageIsDeleted = "Creation of entity \"City\" ({0}) failed. Country with id {1} is deleted.";
        
        public const string CityDeleteFalueExceptionMessageIsDeleted = "Deletion of entity \"City\" ({0}) failed. City is already deleted.";

        // Address
        public const string AddressFromatExceptionMessage = "Address is invalid.";
        public const string AddressCreateFailureExceptionMessageCityNotFound = "Creation of entity \"Address\" ({0}) failed. There are no existing city with id {1}.";
        
        
        

        #endregion

    }
}
