

using Task_Management_Data.Entities;
using Task_Management_Infrastructure.InfrastructureBases;

namespace Task_Management_Infrastructure.Abstracts
{
    public interface IReviewRepository : IGenericRepository<Reviews>
    {
        public IQueryable<Reviews> GetAllReviewsAboutTaskQuerable(int taskId);
        public Task<Reviews> GetReviewsByIdIncludingUser(int reviewId);
    }
}
