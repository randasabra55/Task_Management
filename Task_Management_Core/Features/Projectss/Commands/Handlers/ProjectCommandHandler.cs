using AutoMapper;
using MediatR;
using Task_Management_Core.Bases;
using Task_Management_Core.Features.Projectss.Commands.Models;
using Task_Management_Data.Entities;
using Task_Management_Service.Abstracts;

namespace Task_Management_Core.Features.Projectss.Commands.Handlers
{
    public class ProjectCommandHandler : ResponseHandler,
                                       IRequestHandler<AddProjectCommand, Response<string>>,
                                       IRequestHandler<EditProjectCommand, Response<string>>,
                                       IRequestHandler<DeleteProjectCommand, Response<string>>
    {
        IMapper mapper;
        IProjectService projectService;
        public ProjectCommandHandler(IMapper mapper, IProjectService projectService)
        {
            this.mapper = mapper;
            this.projectService = projectService;
        }

        public async Task<Response<string>> Handle(AddProjectCommand request, CancellationToken cancellationToken)
        {
            var project = mapper.Map<Projects>(request);
            var result = await projectService.addProject(project);
            if (result == "Success")
                return Success("Added Successfully");
            else
                return BadRequest<string>("Failed to add project");
            //throw new NotImplementedException();
        }

        public async Task<Response<string>> Handle(EditProjectCommand request, CancellationToken cancellationToken)
        {
            var project = await projectService.GetProjectById(request.Id);
            if (project == null)
                return NotFound<string>("Project not found to edit");
            var projectMapper = mapper.Map(request, project);
            var result = await projectService.EditProject(projectMapper);
            if (result == "Success")
                return Success("project updated successfully");
            else
                return BadRequest<string>("failed to update project");
        }

        public async Task<Response<string>> Handle(DeleteProjectCommand request, CancellationToken cancellationToken)
        {
            var project = await projectService.GetProjectById(request.Id);
            if (project == null)
                return NotFound<string>("Project not found to delete");
            var result = await projectService.DeleteProject(request.Id);
            if (result == "Success")
                return Success("project deleted successfully");
            else
                return BadRequest<string>("failed to delete project");
        }
    }
}


