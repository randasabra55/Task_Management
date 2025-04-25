using AutoMapper;

namespace Task_Management_Core.Mapping.ReviewMapping
{
    public partial class ReviewProfile : Profile
    {
        public ReviewProfile()
        {
            AddReviewMapping();
            EditReviewMapping();
            GetReviewsMapping();
            GetReviewByIdMapping();
        }
    }
}
