using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineAppService.Application.Features.Commands.Add;
using OnlineAppService.Application.Features.Qeries;
using OnlineAppService.Application.Wrappers;
using OnlineAppService.Model.ResponseModels;

namespace OnlineAppService.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public IMediator _mediator { get; }
        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var query = new GetAllUsersQuery();

            return Ok(await _mediator.Send(query));
        }

        [HttpPost]
        public async Task<ApiResponse<AddUserResponse>> AddUser(AddUserCommand command)
        {
            var res = await _mediator.Send(command);

            return new ApiResponse<AddUserResponse>
            {
                IsSuccess = true,
                Data = new AddUserResponse
                {
                    Email = res.Email,
                    Name = res.Name,
                    Surname = res.Surname,
                }
            };
        }
    }
}
