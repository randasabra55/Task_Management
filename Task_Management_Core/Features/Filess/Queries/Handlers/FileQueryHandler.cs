using AutoMapper;
using MediatR;
using Task_Management_Core.Bases;
using Task_Management_Core.Features.Filess.Queries.Models;
using Task_Management_Core.Features.Filess.Queries.Results;
using Task_Management_Core.Wrapper;
using Task_Management_Service.Abstracts;

namespace Task_Management_Core.Features.Filess.Queries.Handlers
{
    public class FileQueryHandler : ResponseHandler,
                                  IRequestHandler<GetFileByIdQuery, Response<GetFileResult>>,
                                  IRequestHandler<GetAllFilesForTaskQuery, PaginatedResult<GetFileResult>>
    {
        IMapper mapper;
        IFileService fileService;
        ITaskService taskService;
        public FileQueryHandler(IMapper mapper, IFileService fileService, ITaskService taskService)
        {
            this.mapper = mapper;
            this.fileService = fileService;
            this.taskService = taskService;
        }
        public async Task<Response<GetFileResult>> Handle(GetFileByIdQuery request, CancellationToken cancellationToken)
        {
            var file = await fileService.GetFileById(request.Id);
            if (file == null)
                return NotFound<GetFileResult>("File not found");
            else
            {
                //map
                var result = mapper.Map<GetFileResult>(file);
                return Success(result);
            }
        }

        public async Task<PaginatedResult<GetFileResult>> Handle(GetAllFilesForTaskQuery request, CancellationToken cancellationToken)
        {
            var task = await taskService.GetTaskByIdAsync(request.TaskId);
            var files = fileService.GetAllFilesForTask(request.TaskId);
            //map
            var result = await mapper.ProjectTo<GetFileResult>(files)
                                     .ToPaginatedListAsync(request.PageNumber, request.PageSize);
            return result;
        }
    }
}
