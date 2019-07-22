using System;
using System.Linq.Expressions;
using Studio.Domain.Entities;

namespace Studio.Application.Locations.Queries.GetLocationByIdPage
{
    public class LocationIndustryPageViewModel
    {
        public int IndustryId { get; set; }

        public int LocationId { get; set; }

        public string IndustryName { get; set; }

        public static Expression<Func<LocationIndustry, LocationIndustryPageViewModel>> Projection
        {
            get
            {
                return locationIndustry => new LocationIndustryPageViewModel
                {
                    IndustryId = locationIndustry.IndustryId,
                    IndustryName = locationIndustry.Industry.Name,
                    LocationId = locationIndustry.LocationId                  
                };
            }
        }

        public static LocationIndustryPageViewModel Create(LocationIndustry locationIndustry)
        {
            return Projection.Compile().Invoke(locationIndustry);
        }
    }
}