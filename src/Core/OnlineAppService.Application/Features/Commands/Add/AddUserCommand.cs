using MediatR;
using OnlineAppService.Model.ResponseModels;

namespace OnlineAppService.Application.Features.Commands.Add
{
    public class AddUserCommand : IRequest<AddUserResponse>
    {
        public string Name { get; set; } = string.Empty;
        public string Surname { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
    }
}
