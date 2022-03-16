using MediatR;
using OnlineAppService.Model;
using System.ComponentModel.DataAnnotations;

namespace OnlineAppService.Application.Features.Commands.Register
{
    public class RegisterCommand : IRequest<UserModel>
    {
        [EmailAddress, Required, MaxLength(64)]
        public string Email { get; set; }

        [Required, MinLength(4), MaxLength(128)]
        public string Password { get; set; }

        [MaxLength(64)]
        public string Name { get; set; }

        [MaxLength(64)]
        public string Surname { get; set; }
    }
}
