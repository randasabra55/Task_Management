using Microsoft.EntityFrameworkCore;
using Task_Management_Data.Entities;
using Task_Management_Infrastructure.Abstracts;
using Task_Management_Infrastructure.Data;
using Task_Management_Infrastructure.InfrastructureBases;

namespace Task_Management_Infrastructure.Implementations
{
    public class FileRepository : GenericRepository<Files>, IFileRepository
    {
        private DbSet<Files> files;
        public FileRepository(Context dbContext) : base(dbContext)
        {
            files = dbContext.Set<Files>();
        }
    }
}
