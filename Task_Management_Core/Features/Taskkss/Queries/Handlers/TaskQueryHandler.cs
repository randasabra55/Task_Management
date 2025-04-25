using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Task_Management_Core.Bases;
using Task_Management_Core.Features.Taskkss.Queries.Models;
using Task_Management_Core.Features.Taskkss.Queries.Results;
using Task_Management_Core.Wrapper;
using Task_Management_Infrastructure.Data;
using Task_Management_Service.Abstracts;

namespace Task_Management_Core.Features.Taskkss.Queries.Handlers
{
    public class TaskQueryHandler : ResponseHandler,
                                    IRequestHandler<GetTaskByIdQuery, Response<GetTaskResult>>,
                                    IRequestHandler<GetTasksPaginatedQuery, PaginatedResult<GetTaskResult>>,
                                    IRequestHandler<GetUserAllTasksQuery, Response<List<GetAllTasksResult>>>
    {
        IMapper mapper;
        ITaskService taskService;
        private readonly Context _dbContext;
        public TaskQueryHandler(IMapper mapper, ITaskService taskService, Context dbContext)
        {
            this.mapper = mapper;
            this.taskService = taskService;
            this._dbContext = dbContext;
        }

        public async Task<Response<GetTaskResult>> Handle(GetTaskByIdQuery request, CancellationToken cancellationToken)
        {
            var task = await taskService.GetTaskByIdAsync(request.Id);
            if (task == null)
                return NotFound<GetTaskResult>("Task not found");
            //map
            var result = mapper.Map<GetTaskResult>(task);
            return Success(result);
        }

        public async Task<PaginatedResult<GetTaskResult>> Handle(GetTasksPaginatedQuery request, CancellationToken cancellationToken)
        {
            var tasks = taskService.GetTasksPaginated();
            var result = await mapper.ProjectTo<GetTaskResult>(tasks)
                             .ToPaginatedListAsync(request.PageNumber, request.PageSize);
            return result;
        }

        public async Task<Response<List<GetAllTasksResult>>> Handle(GetUserAllTasksQuery request, CancellationToken cancellationToken)
        {
            /*var internalTasks = await _dbContext.tasksses
            .Where(t => t.UserId == request.UserId)
            .Select(t => new GetAllTasksResult
            {
                Title = t.Title,
                Description = t.Description,
                DueDate = t.EndDate,
                Source = "Internal"
            })
            .ToListAsync(cancellationToken);*/
            var externalTaskss = _dbContext.externalAPITasks
                           .Where(t => t.UserId == request.UserId);
            var externalTasks = await _dbContext.externalAPITasks
                .Where(t => t.UserId == request.UserId)
                .Select(t => new GetAllTasksResult
                {
                    Title = t.Title,
                    Description = t.Description,
                    DueDate = t.DueDate,
                    Source = "Google Task"
                })
                .ToListAsync(cancellationToken);

            /*return Success(internalTasks.Concat(externalTasks).ToList());*/
            return Success(externalTasks);
        }
    }
}
