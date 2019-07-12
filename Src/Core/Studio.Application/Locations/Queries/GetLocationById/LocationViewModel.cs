namespace Studio.Application.Locations.Queries.GetLocationById
{
    using System;
    using System.Linq.Expressions;
    using Studio.Domain.Entities;

    public class LocationViewModel
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

        public int ClientId { get; set; }

        public string ClientName { get; set; }
        
        public int AddressId { get; set; }

        public string Address { get; set; }

        public static Expression<Func<Location, LocationViewModel>> Projection
        {
            get
            {
                return location => new LocationViewModel
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
                    ClientId = location.ClientId,
                    ClientName = location.Client.CompanyName,
                    AddressId = location.AddressId,
                    Address = $"гр. {location.Address.City.Name}, {location.Address.AddressFormat.ToString()}"
                };
            }
        }

        public static LocationViewModel Create(Location location)
        {
            return Projection.Compile().Invoke(location);
        }
    }
}