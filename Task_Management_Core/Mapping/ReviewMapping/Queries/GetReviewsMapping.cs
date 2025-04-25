using Task_Management_Core.Features.Reviewss.Queries.Results;
using Task_Management_Data.Entities;

namespace Task_Management_Core.Mapping.ReviewMapping
{
    public partial class ReviewProfile
    {
        public void GetReviewsMapping()
        {
            CreateMap<Reviews, GetReviewsPaginatedResult>()
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.User.FullName));
        }
    }
}
