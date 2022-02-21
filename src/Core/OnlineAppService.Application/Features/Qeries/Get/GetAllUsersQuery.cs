using MediatR;
using OnlineAppService.Application.Dto;
using OnlineAppService.Application.Wrappers;

namespace OnlineAppService.Application.Features.Qeries
{
    public class GetAllUsersQuery : IRequest<List<UserDto>>
    {
        public GetAllUsersQuery()
        {

        }
    }
}
