# Project - Studio

## Type - Reservation Website

## Description

This is a simple Reservation website project for appointments in beauty industry. 
The visitors and users of the website can choose ther best procedure and beauty expert between many studios in different industries.

Guest Users can register and login to their accounts. 
Guest Users can make an appointments, can read information about the website and can send email to the website company.

Regular Users can make an appointments, can delete his appointments and see older appointments.
Regular Users can delete his profile and edit his personal information.

The project also supports Administration. 
Administrators have all rights a Regular User has.
Administrator can see all appointments in website and also edit and delete them.
Administrator can read and remove Users.
Administrator can CRUD Client.
Administrator can CRUD Locations.
Administrator can CRUD Addresses, Cities and Countries.
Administrator can CRUD Employees.
Administrator can CRUD Services.

## Entities

### ASP Indentity is scaffolded.

### Every entity inherits CreatedOn, ModifiedOn, IsDeleted and DeletedOn properties from interfaces.

### StudioUser : IdentityUser
  - FirstName (string)
  - LastName (string)
  - Appointments (ICollection<Appointment>)
  
### StudioRole : IdentityRole

### Client
  -	Id (int)
  - CompanyName (string)
  - VatNumber (string)
  - Manager (Manager)
  - Phone (string)
  - Locations (ICollection<Location>)
  
### Manager : ValueObject
  - FirstName (string)
  - LastName (string)
  - static Manager For(string)
  - static implicit operator string(Manager)
  - static explicit operator Manager(string)
  - override string ToString()
  - override IEnumerable<object> GetEqualityComponents()
  
### Location
  -	Id (int)
  - Name (string)
  - IsOffice (bool)    
  - ClientId (int)
  - Client (Client)    
  - AddressId (int)
  - Address (Address)
  - Employees (ICollection<Employee>)
  - LocationIndustries (ICollection<LocationIndustry>)
  
### LocationIndutry
  - LocationId (int)
  - Location (Location)
  - IndustryId (int)
  - Industry (Industry)
  - IsActive (bool)
  - Description (string)  
  
### Address 
  -	Id (int)
  -	AddressFormat AddressFormat (AddressFormat)
  -	Latitude (decimal)
  -	Longitude (decimal)
  -	CityId (int)
  -	City (City)
  -	Location (Location)
 
### AddressFormat : ValueObject
  - Street (string)
  - Number (string)
  - Building (string)
  - Entrance (string)
  - Floor (string)
  - Apartment (string)
  - District (string)
  - static AddressFormat For(InputAddressData)
  - static implicit operator string(AddressFormat)
  - static explicit operator AddressFormat(InputAddressData)
  - override string ToString()
  - override IEnumerable<object> GetEqualityComponents()
    
### City
  -	Id (int)
  - Name (string)
  - CountryId (int)
  - Country (Country)
  - Addresses (ICollection<Address>)
  
### Country
  -	Id (int)
  - Name (string)
  - Cities (ICollection<City>)
  
### Employee
  - Id (int)
  - FirstName (string)
  - LastName (string) 
  - LocationId (int) 
  - Location (Location)
  - EmployeeServices (ICollection<EmployeeService>)
  - Appointments (ICollection<Appointment>)
  
### Service
  - Id (int)
  - Name (string)
  - IndustryId (int)
  - Industry (Industry)
  - LocationServices (ICollection<EmployeeService>)

### EmployeeService
  - EmployeeId (int)
  - Employee (Employee)
  - ServiceId (int)
  - Service (Service)
  - Price (decimal)
  
### Industry
  - Id (int)
  - Name (string)
  - Possition (string)
  - Services (ICollection<Service>)
  - LocationIndustries (ICollection<LocationIndustry>)
  
### Appointment
  - Id (int)
  - FirstName (string)
  - LastName (string)
  - Email (string)
  - Phone (string)
  - ReservationTime (DateTime)
  - Comment (string)
  - ServiceId (int)
  - Service (Service)
  - EmployeeId (int)
  - Employee (Employee)
  - UserId (string)
  - User (StudioUser)
  
### ContactForm
  - Id (int)
  - FirstName (string)
  - LastName (string)
  - Email (string)
  - Topic (string)
  - Message (string)
