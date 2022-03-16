using MediatR;
using OnlineAppService.Model;

namespace OnlineAppService.Application.Features.Commands.Login
{
    public class LoginCommand : IRequest<UserModel>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
