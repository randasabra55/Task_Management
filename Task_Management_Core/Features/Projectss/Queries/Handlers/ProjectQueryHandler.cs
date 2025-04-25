using AutoMapper;
using MediatR;
using Task_Management_Core.Bases;
using Task_Management_Core.Features.Projectss.Queries.Models;
using Task_Management_Core.Features.Projectss.Queries.Results;
using Task_Management_Core.Wrapper;
using Task_Management_Service.Abstracts;

namespace Task_Management_Core.Features.Projectss.Queries.Handlers
{
    public class ProjectQueryHandler : ResponseHandler,
                                       IRequestHandler<GetProjectByIdQuery, Response<GetProjectResult>>,
                                       IRequestHandler<GetProjectsPaginatedQuery, PaginatedResult<GetProjectResult>>
    {
        IMapper mapper;
        IProjectService projectService;
        public ProjectQueryHandler(IMapper mapper, IProjectService projectService)
        {
            this.mapper = mapper;
            this.projectService = projectService;
        }
        public async Task<Response<GetProjectResult>> Handle(GetProjectByIdQuery request, CancellationToken cancellationToken)
        {
            var project = await projectService.GetProjectById(request.Id);
            if (project == null)
                return NotFound<GetProjectResult>("Project not found");
            //map
            var result = mapper.Map<GetProjectResult>(project);
            return Success(result);
        }

        public async Task<PaginatedResult<GetProjectResult>> Handle(GetProjectsPaginatedQuery request, CancellationToken cancellationToken)
        {
            var projects = projectService.GetProjectsPaginated();
            var result = await mapper.ProjectTo<GetProjectResult>(projects)
                             .ToPaginatedListAsync(request.PageNumber, request.PageSize);
            return result;
        }
    }
}
