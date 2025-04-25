using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using Google.Apis.Upload;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;
using Task_Management_Data.Entities;
using Task_Management_Data.Helper;
using Task_Management_Infrastructure.Abstracts;
using Task_Management_Infrastructure.Data;
using Task_Management_Service.Abstracts;

namespace Task_Management_Service.Implementations
{
    public class FileService : IFileService
    {
        IFileRepository fileRepository;
        Context context;
        ITaskRepository taskRepository;
        IWebHostEnvironment webHost;
        IHttpContextAccessor httpContextAccessor;
        GoogleDriveSettings googleDriveSettings;
        public FileService(IFileRepository fileRepository, IWebHostEnvironment webHost, IHttpContextAccessor httpContextAccessor, ITaskRepository taskRepository, Context context, GoogleDriveSettings googleDriveSettings)
        {
            this.fileRepository = fileRepository;
            this.webHost = webHost;
            this.httpContextAccessor = httpContextAccessor;
            this.taskRepository = taskRepository;
            this.context = context;
            this.googleDriveSettings = googleDriveSettings;
        }
        public async Task<string> AddFileAsync(Files file, IFormFile formFile)
        {
            //add file
            var request = httpContextAccessor.HttpContext?.Request;
            if (request == null) return "Failed to get request context";
            var webRootPath = webHost.WebRootPath ?? Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
            if (!Directory.Exists(webRootPath))
            {
                Directory.CreateDirectory(webRootPath);
            }
            var uploadsFolder = Path.Combine(webHost.WebRootPath, "uploads", "files");
            if (!Directory.Exists(uploadsFolder))
                Directory.CreateDirectory(uploadsFolder);

            var uniqueFileName = Guid.NewGuid().ToString().Substring(0, 5) + Path.GetExtension(formFile.FileName);
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await formFile.CopyToAsync(stream);
            }

            var fileUrl = $"{request.Scheme}://{request.Host}/uploads/files/{uniqueFileName}";

            file.FileURL = fileUrl;

            await fileRepository.AddAsync(file);
            return "Success";
        }

        public async Task<string> DeleteFileAsync(int id)
        {
            var file = await fileRepository.GetByIdAsync(id);
            if (file == null)
                return "NotFound";
            await fileRepository.DeleteAsync(file);
            return "Success";
        }

        public async Task<string> EditFileAsync(Files file, IFormFile formFile)
        {
            if (formFile != null)
            {
                var request = httpContextAccessor.HttpContext?.Request;
                if (request == null) return "Failed to get request context";
                var oldFilePath = Path.Combine(webHost.WebRootPath, file.FileURL.TrimStart('/'));
                if (File.Exists(oldFilePath))
                    File.Delete(oldFilePath);
                var uploadsFolder = Path.Combine(webHost.WebRootPath, "uploads", "files");
                if (!Directory.Exists(uploadsFolder))
                    Directory.CreateDirectory(uploadsFolder);

                var uniqueFileName = Guid.NewGuid().ToString().Substring(0, 5) + Path.GetExtension(formFile.FileName);
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await formFile.CopyToAsync(stream);
                }
                var fileUrl = $"{request.Scheme}://{request.Host}/uploads/files/{uniqueFileName}";

                file.FileURL = fileUrl;
            }

            await fileRepository.UpdateAsync(file);

            return "Success";

        }

        public IQueryable<Files> GetAllFilesForTask(int taskId)
        {
            //var task = taskRepository.GetByIdAsync(taskId);
            return context.files.AsNoTracking().Where(f => f.TaskId == taskId).AsQueryable();
        }

        public async Task<Files> GetFileById(int id)
        {
            return await fileRepository.GetByIdAsync(id);
        }

        public async Task<string> UploadFileToGoogleDriveAsync(Files file, IFormFile formFile)
        {
            var privateKey = googleDriveSettings.PrivateKey
                .Replace("\\n", "\n")
                .Replace("\"", "");

            var credential = new ServiceAccountCredential(
                new ServiceAccountCredential.Initializer(googleDriveSettings.ClientEmail)
                {
                    Scopes = new[] { DriveService.Scope.DriveFile }
                }.FromPrivateKey(privateKey)
            );

            var service = new DriveService(new BaseClientService.Initializer
            {
                HttpClientInitializer = credential,
                ApplicationName = "MyDriveUploader"
            });

            var fileMetadata = new Google.Apis.Drive.v3.Data.File
            {
                Name = formFile.FileName,
                Parents = new[] { googleDriveSettings.FolderId }
            };

            using var stream = formFile.OpenReadStream();
            var request = service.Files.Create(fileMetadata, stream, formFile.ContentType);
            request.Fields = "id";

            var fileResponse = await request.UploadAsync();

            if (fileResponse.Status == UploadStatus.Completed)
            {
                var fileUrl = $"https://drive.google.com/file/d/{request.ResponseBody.Id}/view";

                file.FileURL = fileUrl;
                await fileRepository.AddAsync(file);

                return "Success";
            }
            else
            {
                throw new Exception("Upload to Google Drive failed.");
            }
        }

        public async Task<string> EditFileOnGoogleDriveAsync(Files file, IFormFile newFormFile)
        {
            var privateKey = googleDriveSettings.PrivateKey
                .Replace("\\n", "\n")
                .Replace("\"", "");

            var credential = new ServiceAccountCredential(
                new ServiceAccountCredential.Initializer(googleDriveSettings.ClientEmail)
                {
                    Scopes = new[] { DriveService.Scope.DriveFile }
                }.FromPrivateKey(privateKey)
            );

            var service = new DriveService(new BaseClientService.Initializer
            {
                HttpClientInitializer = credential,
                ApplicationName = "MyDriveUploader"
            });

            if (!string.IsNullOrEmpty(file.FileURL))
            {
                var oldFileId = ExtractFileIdFromUrl(file.FileURL);
                if (!string.IsNullOrEmpty(oldFileId))
                {
                    await service.Files.Delete(oldFileId).ExecuteAsync();
                }
            }

            var fileMetadata = new Google.Apis.Drive.v3.Data.File
            {
                Name = newFormFile.FileName,
                Parents = new[] { googleDriveSettings.FolderId }
            };

            using var stream = newFormFile.OpenReadStream();
            var uploadRequest = service.Files.Create(fileMetadata, stream, newFormFile.ContentType);
            uploadRequest.Fields = "id";

            var uploadResponse = await uploadRequest.UploadAsync();

            if (uploadResponse.Status == UploadStatus.Completed)
            {
                var newFileUrl = $"https://drive.google.com/file/d/{uploadRequest.ResponseBody.Id}/view";

                file.FileURL = newFileUrl;
                await fileRepository.UpdateAsync(file);

                return "Edit Success";
            }
            else
            {
                throw new Exception("File update failed.");
            }
        }

        private string ExtractFileIdFromUrl(string url)
        {
            if (string.IsNullOrEmpty(url)) return null;

            url = url.Replace("\"", "").Trim();

            var match = Regex.Match(url, @"\/file\/d\/([a-zA-Z0-9_-]+)");
            return match.Success ? match.Groups[1].Value : null;
        }


    }
}
