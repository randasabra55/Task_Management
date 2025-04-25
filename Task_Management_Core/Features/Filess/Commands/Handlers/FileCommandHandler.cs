using AutoMapper;
using MediatR;
using Task_Management_Core.Bases;
using Task_Management_Core.Features.Filess.Commands.Models;
using Task_Management_Data.Entities;
using Task_Management_Service.Abstracts;

namespace Task_Management_Core.Features.Filess.Commands.Handlers
{
    public class FileCommandHandler : ResponseHandler,
                                    IRequestHandler<AddFileCommand, Response<string>>,
                                    IRequestHandler<EditFileCommand, Response<string>>,
                                    IRequestHandler<DeleteFileCommand, Response<string>>,
                                    IRequestHandler<AddFileToGoogleDriveCommand, Response<string>>,
                                    IRequestHandler<EditFileInGoogleDriveCommand, Response<string>>
    {
        IFileService fileService;
        IMapper mapper;
        public FileCommandHandler(IFileService fileService, IMapper mapper)
        {
            this.fileService = fileService;
            this.mapper = mapper;
        }
        public async Task<Response<string>> Handle(AddFileCommand request, CancellationToken cancellationToken)
        {
            Files file = mapper.Map<Files>(request);
            var result = await fileService.AddFileAsync(file, request.FileURL);
            if (result == "Success")
                return Success("File add successfully");
            else
                return BadRequest<string>("can not upload file, try again");
        }

        public async Task<Response<string>> Handle(EditFileCommand request, CancellationToken cancellationToken)
        {
            var file = await fileService.GetFileById(request.Id);
            if (file == null)
                return NotFound<string>("there is no file with this id to edit");
            var fileMapper = mapper.Map(request, file);
            var result = await fileService.EditFileAsync(file, request.FileURL);
            if (result == "Success")
                return Success("file updated successfully");
            return BadRequest<string>("can not update file, try again");
        }

        public async Task<Response<string>> Handle(DeleteFileCommand request, CancellationToken cancellationToken)
        {
            var file = await fileService.GetFileById(request.Id);
            if (file == null)
                return NotFound<string>("there is no file with this id to delete");
            var result = await fileService.DeleteFileAsync(request.Id);
            if (result == "Success")
                return Success("deleted successfully");
            return BadRequest<string>("can not delete file, try again");
        }

        public async Task<Response<string>> Handle(AddFileToGoogleDriveCommand request, CancellationToken cancellationToken)
        {
            Files file = mapper.Map<Files>(request);
            var result = await fileService.UploadFileToGoogleDriveAsync(file, request.FileURL);
            if (result == "Success")
                return Success("File add successfully");
            else
                return BadRequest<string>("can not upload file, try again");
        }

        public async Task<Response<string>> Handle(EditFileInGoogleDriveCommand request, CancellationToken cancellationToken)
        {
            var file = await fileService.GetFileById(request.Id);
            if (file == null)
                return NotFound<string>("there is no file with this id to edit");
            var fileMapper = mapper.Map(request, file);
            var result = await fileService.EditFileOnGoogleDriveAsync(file, request.FileURL);
            if (result == "Edit Success")
                return Success("file updated successfully");
            return BadRequest<string>("can not update file, try again");
        }
    }
}
