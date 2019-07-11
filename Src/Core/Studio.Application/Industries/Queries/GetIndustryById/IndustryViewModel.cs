namespace Studio.Application.Industries.Queries.GetIndustryById
{
    using System;
    using System.Linq.Expressions;
    using Studio.Domain.Entities;

    public class IndustryViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Possition { get; set; }

        public static Expression<Func<Industry, IndustryViewModel>> Projection
        {
            get
            {
                return industry => new IndustryViewModel
                {
                    Id = industry.Id,
                    Name = industry.Name,
                    Possition = industry.Possition
                };
            }
        }

        public static IndustryViewModel Create(Industry industry)
        {
            return Projection.Compile().Invoke(industry);
        }
    }
}