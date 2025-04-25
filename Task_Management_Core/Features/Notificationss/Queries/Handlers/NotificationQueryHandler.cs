using AutoMapper;
using MediatR;
using Task_Management_Core.Bases;
using Task_Management_Core.Features.Notificationss.Queries.Models;
using Task_Management_Core.Features.Notificationss.Queries.Results;
using Task_Management_Service.Abstracts;


namespace Task_Management_Core.Features.Notificationss.Queries.Handlers
{
    public class NotificationQueryHandler : ResponseHandler,
                                          IRequestHandler<GetNotificationByIdQuery, Response<GetNotificationResult>>,
                                          IRequestHandler<GetNotificationListQuery, Response<List<GetNotificationResult>>>
    {
        INotificationService notificationService;
        IMapper mapper;
        public NotificationQueryHandler(INotificationService notificationService, IMapper mapper)
        {
            this.notificationService = notificationService;
            this.mapper = mapper;
        }
        public async Task<Response<GetNotificationResult>> Handle(GetNotificationByIdQuery request, CancellationToken cancellationToken)
        {
            var notification = await notificationService.GetNotificationById(request.Id);
            if (notification == null)
                return NotFound<GetNotificationResult>("there in no notification with this id");
            //map
            var result = mapper.Map<GetNotificationResult>(notification);
            return Success(result);
        }

        public async Task<Response<List<GetNotificationResult>>> Handle(GetNotificationListQuery request, CancellationToken cancellationToken)
        {
            var notifications = await notificationService.GetAllNotificationForUser(request.UserId);
            var result = mapper.Map<List<GetNotificationResult>>(notifications);
            return Success(result);
        }
    }
}
