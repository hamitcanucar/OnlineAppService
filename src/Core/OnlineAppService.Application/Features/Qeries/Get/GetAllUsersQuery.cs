using MediatR;
using OnlineAppService.Model;

namespace OnlineAppService.Application.Features.Qeries
{
    public class GetAllUsersQuery : IRequest<List<UserModel>>
    {
        public GetAllUsersQuery()
        {

        }
    }
}
