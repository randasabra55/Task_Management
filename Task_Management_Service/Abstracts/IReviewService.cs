
using Task_Management_Data.Entities;

namespace Task_Management_Service.Abstracts
{
    public interface IReviewService
    {
        public Task<string> AddReview(Reviews review);
        public Task<string> EditReview(Reviews review);
        public Task<Reviews> GetReviewById(int reviewId);
        public Task<string> DeleteReview(int reviewId);
        public IQueryable<Reviews> GetAllReviewsAboutTaskQuerable(int TaskId);
        //public IQueryable<Reviews> GetReviewsQuerable();
    }
}
