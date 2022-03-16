using MediatR;
using OnlineAppService.Model;
using OnlineAppService.Model.ResponseModels;
using System.ComponentModel.DataAnnotations;

namespace OnlineAppService.Application.Features.Commands.Update
{
    public class UpdateUserCommand : IRequest<UpdateUserResponseModel>
    {
        public Guid UserId { get; set; }

        [EmailAddress, MaxLength(64)]
        public string? Email { get; set; }

        [MaxLength(64)]
        public string? Name { get; set; }

        [MaxLength(64)]
        public string? Surname { get; set; }

        [MinLength(10), MaxLength(13), Phone, Required]
        public string? PhoneNumber { get; set; }
    }
}
