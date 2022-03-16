using MediatR;
using OnlineAppService.Domain.Common;
using OnlineAppService.Model;
using System.ComponentModel.DataAnnotations;

namespace OnlineAppService.Application.Features.Commands.Update
{
    public class UpdatePasswordCommand : IRequest<UserModel>
    {
        [Required]
        public Guid Id { get; set; }

        [Required, MinLength(4), MaxLength(128)]
        public string OldPassword { get; set; }      

        [Required, MinLength(4), MaxLength(128)]
        public string NewPassword { get; set; }
    }
}
