

using Task_Management_Core.Features.Reviewss.Commands.Models;
using Task_Management_Data.Entities;

namespace Task_Management_Core.Mapping.ReviewMapping
{
    public partial class ReviewProfile
    {
        public void AddReviewMapping()
        {
            CreateMap<AddReviewCommand, Reviews>();
        }
    }
}

