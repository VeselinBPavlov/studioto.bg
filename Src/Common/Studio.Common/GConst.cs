namespace Studio.Common
{
    public static class GConst
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
        public const string Industry = "Industry";
        public const string Country = "Country";
        public const string City = "City";
        public const string Address = "Address";
        public const string Location = "Location";
        public const string Manager = "Manager";
        public const string Client = "Client";
        public const string Service = "Service";
        public const string Employee = "Employee";
        public const string Appointment = "Appointment";
        public const string IndustryLower = "industry";
        public const string CountryLower = "country";
        public const string CityLower = "city";
        public const string AddressLower = "address";
        public const string LocationLower = "location";
        public const string ManagerLower = "manager";
        public const string ClientLower = "client";
        public const string ServiceLower = "service";
        public const string EmployeeLower = "employee";
        public const string AppointmentLower = "appointment";
        public const string Addresses = "addresses";
        public const string Locations = "locations";
        public const string Employees = "employees";
        public const string Cities = "cities";
        public const string Services = "services";
        public const string Appointments = "appointments";
        public const string Create = "Creation";
        public const string Delete = "Deletion";
        public const string Update = "Update";

        #endregion

        #region Studio.Application.Tests

        public const string SuccessStatus = "RanToCompletion";
        public const string InvalidName = "AbcdefghijAbcdefghijAbcdefghijAbcdefghijAbcdefghijAbcdefghijAbcdefghijAbcdefghijAbcdefghijAbcdefghijAbcdefghij";
        public const int InvalidId = 100;
        public const int ValidId = 1;
        public const int ValidCount = 1;
        public const string ValidName = "CETech";
        public const string UpdatedName = "Mars";
        public const string ValidVatNumber = "BG114564897";
        public const string ValidPhone = "+359887889884";
        public const string ValidAddressNumber = "1";
        public const string NotFoundExceptionMessage = "Entity \"{0}\" ({1}) was not found.";
        public const string ReferenceExceptionMessage = "{0} of entity \"{1}\" ({2}) failed. There are no existing {3} with id {4}.";
        public const string DeleteFailureExceptionMessage = "Deletion of entity \"{0}\" ({1}) failed. There are existing {2} associated with this {3}.";       
        public const string UniqueNameExceptionMessage = "{0} of entity \"{1}\" ({2}) failed. There are existing {3} with the same name.";
        public const string ValueObjectExceptionMessage = "{0} is invalid.";

        #endregion

    }
}
