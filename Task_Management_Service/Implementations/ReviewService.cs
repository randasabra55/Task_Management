

using Task_Management_Data.Entities;
using Task_Management_Infrastructure.Abstracts;
using Task_Management_Service.Abstracts;

namespace Task_Management_Service.Implementations
{
    public class ReviewService : IReviewService
    {
        IReviewRepository reviewRepository;
        public ReviewService(IReviewRepository reviewRepository)
        {
            this.reviewRepository = reviewRepository;
        }
        public async Task<string> AddReview(Reviews review)
        {
            await reviewRepository.AddAsync(review);
            return "Success";
        }

        public async Task<string> DeleteReview(int reviewId)
        {
            var review = await reviewRepository.GetByIdAsync(reviewId);
            if (review == null)
                return "NotFound";
            await reviewRepository.DeleteAsync(review);
            return "Success";
        }

        public async Task<string> EditReview(Reviews review)
        {
            await reviewRepository.UpdateAsync(review);
            return "Success";
        }

        public IQueryable<Reviews> GetAllReviewsAboutTaskQuerable(int TaskId)
        {
            return reviewRepository.GetAllReviewsAboutTaskQuerable(TaskId);
        }

        public async Task<Reviews> GetReviewById(int reviewId)
        {
            //return await reviewRepository.GetByIdAsync(reviewId);
            return await reviewRepository.GetReviewsByIdIncludingUser(reviewId);
        }


        /*public IQueryable<Reviews> GetReviewsQuerable()
        {
            return reviewRepository.GetReviewsQuerable();
        }*/
    }
}
