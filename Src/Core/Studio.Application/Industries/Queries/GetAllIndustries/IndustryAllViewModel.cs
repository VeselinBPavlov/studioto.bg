namespace Studio.Application.Industries.Queries.GetAllIndustries
{
    using AutoMapper;
    using Studio.Application.Interfaces.Mapping;
    using Studio.Domain.Entities;

    public class IndustryAllViewModel : IHaveCustomMapping
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Possition { get; set; }

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<Industry, IndustryAllViewModel>();
        }
    }
}