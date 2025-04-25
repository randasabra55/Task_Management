using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Task_Management_Core.Bases;
using Task_Management_Core.Features.Authentications.Queries.Models;
using Task_Management_Core.Features.Authentications.Queries.Results;
using Task_Management_Core.Wrapper;
using Task_Management_Data.Entities.Identity;

namespace Task_Management_Core.Features.Authentications.Queries.Handlers
{
    public class UserQueryHandler : ResponseHandler,
                                   IRequestHandler<GetUserByIdQuery, Response<GetUserByIdResponse>>,
                                   IRequestHandler<GetUserPaginatedQuery, PaginatedResult<GetUserPaginatedList>>
    {
        IMapper mapper;
        UserManager<User> userManager;
        public UserQueryHandler(IMapper mapper, UserManager<User> userManager)
        {
            this.mapper = mapper;
            this.userManager = userManager;
        }
        public async Task<Response<GetUserByIdResponse>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var user = userManager.Users.FirstOrDefault(u => u.Id == request.Id);
            if (user == null)
            {
                return NotFound<GetUserByIdResponse>("User not found");
            }
            //mapp
            var response = mapper.Map<GetUserByIdResponse>(user);
            return Success(response);
        }

        public async Task<PaginatedResult<GetUserPaginatedList>> Handle(GetUserPaginatedQuery request, CancellationToken cancellationToken)
        {
            var list = userManager.Users.AsQueryable();
            var paginateList = await mapper.ProjectTo<GetUserPaginatedList>(list)
                                     .ToPaginatedListAsync(request.PageNumber, request.PageSize);
            return paginateList;

        }

    }
}
