namespace Studio.Application.Cities.Queries.GetLocationIndustryById
{
    using System;
    using System.Linq.Expressions;
    using Studio.Domain.Entities;

    public class LocationIndustryViewModel
    {
        public int LocationId { get; set; }

        public string LocationName { get; set; }

        public int IndustryId { get; set; }        

        public string IndustryName { get; set; }

        public string Description { get; set; }

        public static Expression<Func<LocationIndustry, LocationIndustryViewModel>> Projection
        {
            get
            {
                return locationIndustry => new LocationIndustryViewModel
                {
                    LocationId = locationIndustry.LocationId,
                    LocationName = locationIndustry.Location.Name,                    
                    IndustryId = locationIndustry.IndustryId,
                    IndustryName = locationIndustry.Industry.Name,
                    Description = locationIndustry.Description,
                };
            }
        }

        public static LocationIndustryViewModel Create(LocationIndustry locationIndustry)
        {
            return Projection.Compile().Invoke(locationIndustry);
        }
    }
}