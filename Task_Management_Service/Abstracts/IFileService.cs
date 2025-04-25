using Microsoft.AspNetCore.Http;
using Task_Management_Data.Entities;

namespace Task_Management_Service.Abstracts
{
    public interface IFileService
    {
        public Task<string> AddFileAsync(Files file, IFormFile formFile);
        public Task<string> DeleteFileAsync(int id);
        public Task<string> EditFileAsync(Files file, IFormFile formFile);
        public Task<Files> GetFileById(int id);
        public IQueryable<Files> GetAllFilesForTask(int taskId);
        public Task<string> UploadFileToGoogleDriveAsync(Files file, IFormFile formFile);
        public Task<string> EditFileOnGoogleDriveAsync(Files file, IFormFile newFormFile);
    }
}
