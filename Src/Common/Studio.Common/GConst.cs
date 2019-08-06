namespace Studio.Common
{
    public static class GConst
    {
        #region Studio.Application.Core

        public const string BaseException = "One or more validation failures have occurred.";
        public const string FailureException = "Възникна грешка при {0} на \"{1}\" ({2}). {3}";
        public const string NotFoundException = "Обектът от тип \"{0}\" ({1}) не е намерен.";
        public const string DeleteException = "Съществуват {0} свързани с {1}.";
        public const string RefereceException = "Не съществува {0} с id {1}.";
        public const string UniqueNameException = "Съшествува {0} със същото име.";
        public const string InvalidAppointmentHourException = "Работното време на обекта е от  {0} до {1} часа.";
        public const string ReservedHourException = "{0} already has an appointment on {1} on {2}.";
        public const string SenderEmail = "studio@gmail.bg";
        public const string SenderName = "studioto.bg";
        public const string SenderSubject = "Получено запитване.";
        public const string SenderMessage = "Вашето запитване е получено успешно. Очаквайте свързване с администратор.";
        public const string ApiKey = "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";
        public const string AppointmentSubject = "Успешна резервация в Studioto.bg!";
        public const string AppointmentMessage = "Вие успешно резервирахте час за {0} в {1} от {2} часа.";  
        public const string ErrorRequiredMessage = "Полето \"{0}\" e задължително";
        public const string ErrorLengthMessage = "Полето \"{0}\" трябва да е дължина {1} до {2} символа";
        public const string ErrorInvalidMessage = "Невалидна стойност на полето \"{0}\"";
        public const string ErrorPhoneMessage = "Невалиден телефонен формат. Примери: +359884456456, 0884456456";
        public const string ErrorVatNumberMessage = "Невалиден формат на данъчния номер. Примери: 123456789, BG123456789";
        public const string ErrorEmailMessage = "Невалиден email формат. Пример: user@provider.com";
        public const string ErrorPriceMessage = "Цената трябва да е по-голяма от 0.00";
        public const string PuctureErrorMessage = "Нямате прикачена снимка!";
        public const string ReservationDateError = "Няма свободни часове за избраната дата.";
        public const string ClashAppointmentMessage = "{0} вече има запазен час на {1} от {2} часа. Моля, опитайте с друг час.";

        #endregion

        #region Studio.Application.Domain

        public const string ValueObjectException = "{0} is invalid.";
        public const string AddressFormatException = "Улицата и номера са задължителни!";
        public const string ManagerException = "Невалиден формат за име и фамилия!";
        public const string Industry = "Бизнес";
        public const string Country = "Държава";
        public const string City = "Град";
        public const string Address = "Адрес";
        public const string Location = "Обект";
        public const string Manager = "Управител";
        public const string Client = "Фирма";
        public const string Service = "Услуга";
        public const string Employee = "Служител";
        public const string Appointment = "Резервация";
        public const string EmployeeService = "Връзка Служител-Услуга";
        public const string LocationIndustry = "Връзка Обект-Бизнес";
        public const string User = "Потребител";
        public const string IndustryLower = "бизнес";
        public const string CountryLower = "държава";
        public const string CityLower = "град";
        public const string AddressLower = "адрес";
        public const string LocationLower = "обект";
        public const string ManagerLower = "управител";
        public const string ClientLower = "фирма";
        public const string ServiceLower = "услуга";
        public const string EmployeeLower = "служител";
        public const string AppointmentLower = "регистрация";
        public const string UserLower = "потребител";
        public const string Addresses = "адреси";
        public const string Locations = "обекти";
        public const string Employees = "служители";
        public const string Cities = "градове";
        public const string Services = "услуги";
        public const string Industries = "бизнеси";
        public const string Appointments = "резервации";
        public const string Users = "потребители";
        public const string Create = "създаването";
        public const string Delete = "изтриването";
        public const string Update = "промяната";

        #endregion

        #region Studio.Application.Tests

        public const string SuccessStatus = "RanToCompletion";
        public const string InvalidName = "AbcdefghijAbcdefghijAbcdefghijAbcdefghijAbcdefghijAbcdefghijAbcdefghijAbcdefghijAbcdefghijAbcdefghijAbcdefghij";
        public const int InvalidId = 100;
        public const int ValidId = 1;
        public const int ValidCount = 1;
        public const int ValidQueryCount = 3;
        public const string ValidName = "CETech";
        public const decimal ValidPrice = 2.10M;
        public const decimal ZeroPrice = 0.00M;
        public const decimal InvalidPrice = -2.30M;
        public const string InvalidHourBefore = "6:00";
        public const string InvalidHourAfter = "22:00";        
        public const int ZeroId = 0;
        public const string ValidEmail = "steve@gmail.com";
        public const string UpdatedName = "Mars";
        public const string ValidVatNumber = "BG114564897";
        public const string ValidPhone = "+359887889884";
        public const string ValidAddressNumber = "1";
        public const string ValidHour = "08:00";
        public const string ValidStartHour = "8";
        public const string ValidEndHour = "18";
        public const string ValidServiceDuration = "30";
        public const string ValidStartDay = "1";
        public const string ValidEndDay = "5";
        public const string AllHoursBusy = "busy";
        public const string NotFoundExceptionMessage = "Обектът от тип \"{0}\" ({1}) не е намерен.";
        public const string ReferenceExceptionMessage = "Възникна грешка при {0} на \"{1}\" ({2}). Не съществува {3} с id {4}.";
        public const string DeleteFailureExceptionMessage = "Възникна грешка при изтриването на \"{0}\" ({1}). Съществуват {2} свързани с {3}.";       
        public const string UniqueNameExceptionMessage = "Възникна грешка при {0} на \"{1}\" ({2}). Съшествува {3} със същото име.";
        public const string ValueObjectExceptionMessage = "Невалиден {0}.";
        public const string NotAvalableHours = "Няма свободни часове за {0}";
        public const string NotAvalableAppointments = "Няма свободни часове";
        public const int AvailableAppointmentsCount = 20;

        #endregion

        #region Studio.User.WebApp

        public const string AdministratorRole = "Administrator";
        public const string AdministratorArea = "Administrator";
        public const string UserRole = "User";
        public const string Email = "Email";
        public const string FirstName = "Име";
        public const string LastName = "Фамилия";
        public const string Phone = "Телефон";
        public const string Password = "Парола";

        #endregion
    }
}
