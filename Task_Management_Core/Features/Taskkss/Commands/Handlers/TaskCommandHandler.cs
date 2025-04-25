using AutoMapper;
using MediatR;
using Task_Management_Core.Bases;
using Task_Management_Core.Features.Taskkss.Commands.Models;
using Task_Management_Data.Entities;
using Task_Management_Service.Abstracts;

namespace Task_Management_Core.Features.Taskkss.Commands.Handlers
{
    public class TaskCommandHandler : ResponseHandler,
                                    IRequestHandler<AddTaskCommand, Response<string>>,
                                    IRequestHandler<EditTaskCommand, Response<string>>,
                                    IRequestHandler<DeleteTaskCommand, Response<string>>
    {
        IMapper mapper;
        ITaskService taskService;
        INotificationService notificationService;
        public TaskCommandHandler(IMapper mapper, ITaskService taskService, INotificationService notificationService)
        {
            this.mapper = mapper;
            this.taskService = taskService;
            this.notificationService = notificationService;
        }

        public async Task<Response<string>> Handle(AddTaskCommand request, CancellationToken cancellationToken)
        {
            var task = mapper.Map<Taskss>(request);
            var result = await taskService.addTaskAsync(task);
            if (result == "Success")
            {
                var notification = new Notifications
                {
                    Message = "this task assign to you",
                    UserId = request.UserId,
                    IsRead = false
                };
                notificationService.AddNotification(notification);
                return Success("Added Successfully");
            }

            else
                return BadRequest<string>("Failed to add task");
        }

        public async Task<Response<string>> Handle(EditTaskCommand request, CancellationToken cancellationToken)
        {
            var task = await taskService.GetTaskByIdAsync(request.Id);
            if (task == null)
                return NotFound<string>("Task not found to edit");
            var taskMapper = mapper.Map(request, task);
            var result = await taskService.EditTaskAsync(taskMapper);
            if (result == "Success")
                return Success("task updated successfully");
            else
                return BadRequest<string>("failed to update task");
        }

        public async Task<Response<string>> Handle(DeleteTaskCommand request, CancellationToken cancellationToken)
        {
            var task = await taskService.GetTaskByIdAsync(request.Id);
            if (task == null)
                return NotFound<string>("Task not found to delete");
            var result = await taskService.DeleteTaskAsync(request.Id);
            if (result == "Success")
                return Success("Task deleted successfully");
            else
                return BadRequest<string>("failed to delete Task");
        }
    }
}
