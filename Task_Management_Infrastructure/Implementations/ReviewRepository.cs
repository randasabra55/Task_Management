
using Microsoft.EntityFrameworkCore;
using Task_Management_Data.Entities;
using Task_Management_Infrastructure.Abstracts;
using Task_Management_Infrastructure.Data;
using Task_Management_Infrastructure.InfrastructureBases;

namespace Task_Management_Infrastructure.Implementations
{
    public class ReviewRepository : GenericRepository<Reviews>, IReviewRepository
    {
        private DbSet<Reviews> reviews;
        public ReviewRepository(Context dbContext) : base(dbContext)
        {
            reviews = dbContext.Set<Reviews>();
        }

        public async Task<Reviews> GetReviewsByIdIncludingUser(int reviewId)
        {
            return await reviews.Include(r => r.User).FirstOrDefaultAsync(r => r.Id == reviewId);
        }

        public IQueryable<Reviews> GetAllReviewsAboutTaskQuerable(int taskId)
        {
            return reviews.AsNoTracking().Include(r => r.User).Where(r => r.TaskId == taskId).AsQueryable();
        }
    }
}
