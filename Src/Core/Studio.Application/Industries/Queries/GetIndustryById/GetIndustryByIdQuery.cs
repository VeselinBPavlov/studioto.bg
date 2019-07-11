namespace Studio.Application.Industries.Queries.GetIndustryById
{
    using MediatR;

    public class GetIndustryByIdQuery : IRequest<IndustryViewModel>
    {
        public int Id { get; set; }
    }
}