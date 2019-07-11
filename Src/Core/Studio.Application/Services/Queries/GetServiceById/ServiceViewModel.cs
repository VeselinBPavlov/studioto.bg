namespace Studio.Application.Services.Queries.GetServiceById
{
    using System;
    using System.Linq.Expressions;
    using Studio.Domain.Entities;

    public class ServiceViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int IndustryId { get; set; }

        public string IndustryName { get; set; }

        public static Expression<Func<Service, ServiceViewModel>> Projection
        {
            get
            {
                return service => new ServiceViewModel
                {
                    Id = service.Id,
                    Name = service.Name,
                    IndustryId = service.IndustryId,
                    IndustryName = service.Industry.Name
                };
            }
        }

        public static ServiceViewModel Create(Service service)
        {
            return Projection.Compile().Invoke(service);
        }
    }
}