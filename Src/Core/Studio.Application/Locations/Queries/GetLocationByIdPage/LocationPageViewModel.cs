namespace Studio.Application.Locations.Queries.GetLocationByIdPage
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using Domain.Entities;

    public class LocationPageViewModel
    {
      public int Id { get; set; }

        public string Name { get; set; }

        public bool IsOffice { get; set; }

        public string Phone { get; set; }

        public string Slogan { get; set; }

        public string Description { get; set; }

        public string StartDay { get; set; }

        public string EndDay { get; set; }

        public string StartHour { get; set; }

        public string EndHour { get; set; }
        
        public int AddressId { get; set; }

        public string Address { get; set; }

        public ICollection<EmployeeLocationPageViewModel> Employees { get; set; }

        public ICollection<LocationIndustryPageViewModel> Industries { get; set; }

        public static Expression<Func<Location, LocationPageViewModel>> Projection
        {
            get
            {
                return location => new LocationPageViewModel
                {
                    Id = location.Id,
                    Name = location.Name,
                    IsOffice = location.IsOffice,
                    Slogan = location.Slogan,
                    Description = location.Description,
                    Phone = location.Phone,
                    StartDay = location.StartDay.ToString(),
                    EndDay = location.EndDay.ToString(),
                    StartHour = location.StartHour,
                    EndHour = location.EndHour,
                    AddressId = location.AddressId,
                    Address = $"гр. {location.Address.City.Name}, {location.Address.AddressFormat.ToString()}",
                    Employees = location.Employees.AsQueryable()
                        .Select(EmployeeLocationPageViewModel.Projection)
                        .ToList(),
                    Industries = location.LocationIndustries.AsQueryable()
                        .Select(LocationIndustryPageViewModel.Projection)
                        .ToList()
                };
            }
        }

        public static LocationPageViewModel Create(Location location)
        {
            return Projection.Compile().Invoke(location);
        }
    }
}