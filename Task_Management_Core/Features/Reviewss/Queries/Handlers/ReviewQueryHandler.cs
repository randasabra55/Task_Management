using AutoMapper;
using MediatR;
using Task_Management_Core.Bases;
using Task_Management_Core.Features.Reviewss.Queries.Models;
using Task_Management_Core.Features.Reviewss.Queries.Results;
using Task_Management_Core.Wrapper;
using Task_Management_Service.Abstracts;

namespace Task_Management_Core.Features.Reviewss.Queries.Handlers
{
    public class ReviewQueryHandler : ResponseHandler,
                                      IRequestHandler<GetReviewByIdQuery, Response<GetReviewsPaginatedResult>>,
                                      IRequestHandler<GetReviewsPaginatedAboutTaskQuery, PaginatedResult<GetReviewsPaginatedResult>>

    {
        IReviewService reviewService;
        ITaskService taskService;
        //ICourseService courseService;
        IMapper mapper;
        public ReviewQueryHandler(IReviewService reviewService, IMapper mapper, ITaskService taskService)
        {
            this.reviewService = reviewService;
            this.mapper = mapper;
            this.taskService = taskService;
            // this.courseService = courseService;
        }

        public async Task<Response<GetReviewsPaginatedResult>> Handle(GetReviewByIdQuery request, CancellationToken cancellationToken)
        {
            var review = await reviewService.GetReviewById(request.Id);
            if (review == null)
                return NotFound<GetReviewsPaginatedResult>("review not found");
            //map
            var result = mapper.Map<GetReviewsPaginatedResult>(review);
            return Success(result);

        }

        public async Task<PaginatedResult<GetReviewsPaginatedResult>> Handle(GetReviewsPaginatedAboutTaskQuery request, CancellationToken cancellationToken)
        {
            var task = await taskService.GetTaskByIdAsync(request.TaskId);
            if (task != null)
            {
                var reviews = reviewService.GetAllReviewsAboutTaskQuerable(request.TaskId);
                //map
                var result = await mapper.ProjectTo<GetReviewsPaginatedResult>(reviews)
                                   .ToPaginatedListAsync(request.PageNumber, request.PageSize);
                return result;
            }
            throw new Exception("Task not found.");
        }

        /*public async Task<PaginatedResult<GetReviewsPaginatedResult>> Handle(GetReviewsPaginatedAboutCourseQuery request, CancellationToken cancellationToken)
        {
            var course = await courseService.GetCourseByIdAsync(request.CourseId);
            if (course != null)
            {
                //return ( "$\"thete is no course with id {request.CourseId} to find reviews" );
                var reviews = reviewService.GetReviewsByCourseIdQuerable(request.CourseId);
                //map
                var result = await mapper.ProjectTo<GetReviewsPaginatedResult>(reviews)
                                        .ToPaginatedListAsync(request.PageNumber, request.PageSize);
                return result;
                // }
            }*/
    }
}
