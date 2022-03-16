using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineAppService.Application.Exceptions;
using OnlineAppService.Application.Features.Commands.Login;
using OnlineAppService.Application.Features.Commands.Register;
using OnlineAppService.Application.Features.Commands.Update;
using OnlineAppService.Application.Features.Qeries;
using OnlineAppService.Application.Wrappers;
using OnlineAppService.Model;
using OnlineAppService.Model.ResponseModels;

namespace OnlineAppService.WebApi.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : BaseController<UserController>
    {
        public IMediator _mediator { get; }
        public UserController(ILogger<UserController> logger, IMediator mediator) : base(logger)
        {
            _mediator = mediator;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var query = new GetAllUsersQuery();

            return Ok(await _mediator.Send(query));
        }

        /// <summary>
        /// Register for user
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        [Route("register")]
        public async Task<ApiResponse<UserModel>> Register(RegisterCommand command)
        {
            if (!ModelState.IsValid)
            {
                var errorList = ModelState.Where(x => x.Value.Errors.Any()).Select(x => x.Key + " - " + x.Value.Errors.FirstOrDefault()?.ErrorMessage + " - " + x.Value.Errors.FirstOrDefault()?.Exception).ToList();
                return new ApiResponse<UserModel>
                {
                    IsSuccess = false,
                    Data = null,
                    Message = string.Join(" ", errorList)
                };
            }

            var res = await _mediator.Send(command);

            if (res == null)
            {
                return new ApiResponse<UserModel>
                {
                    Code = nameof(ErrorMessages.DUPLICATED_CRIDENTIAL),
                    Message = ErrorMessages.DUPLICATED_CRIDENTIAL
                };
            }

            return new ApiResponse<UserModel>
            {
                IsSuccess = true,
                Data = res
            };
        }

        /// <summary>
        /// User login
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        [Route("login")]
        public async Task<ApiResponse<UserModel>> Login(LoginCommand command)
        {
            if (!ModelState.IsValid)
            {
                var errorList = ModelState.Where(x => x.Value.Errors.Any()).Select(x => x.Key + " - " + x.Value.Errors.FirstOrDefault()?.ErrorMessage + " - " + x.Value.Errors.FirstOrDefault()?.Exception).ToList();
                return new ApiResponse<UserModel>
                {
                    IsSuccess = false,
                    Data = null,
                    Message = string.Join(" ", errorList)
                };
            }

            var res = await _mediator.Send(command);

            if (res == null)
            {
                return new ApiResponse<UserModel>
                {
                    Code = nameof(ErrorMessages.LOGIN_WRONG_CRIDENTIALS),
                    Message = ErrorMessages.LOGIN_WRONG_CRIDENTIALS
                };
            }
            else if (res.Token == null)
            {
                return new ApiResponse<UserModel>
                {
                    Code = nameof(ErrorMessages.LOGIN_DEACTIVE_USER),
                    Message = ErrorMessages.LOGIN_DEACTIVE_USER,
                    Data = res
                };
            }

            return new ApiResponse<UserModel>
            {
                IsSuccess = true,
                Data = res
            };
        }

        /// <summary>
        /// Update user password from user id.
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("update-password")]
        public async Task<ApiResponse<string>> UpdatePassword(UpdatePasswordCommand command)
        {
            if (!ModelState.IsValid)
            {
                var errorList = ModelState.Where(x => x.Value.Errors.Any()).Select(x => x.Key + " - " + x.Value.Errors.FirstOrDefault()?.ErrorMessage + " - " + x.Value.Errors.FirstOrDefault()?.Exception).ToList();
                return new ApiResponse<string>
                {
                    IsSuccess = false,
                    Data = null,
                    Message = string.Join(" ", errorList)
                };
            }

            command.Id = GetUserIdFromToken();

            var res = await _mediator.Send(command);

            if (res == null)
                return new ApiResponse<string>
                {
                    Code = nameof(ErrorMessages.USER_NOT_FOUND),
                    Message = ErrorMessages.USER_NOT_FOUND
                };

            if (res.Id == Guid.Empty)
                return new ApiResponse<string>
                {
                    Code = nameof(ErrorMessages.USER_UPDATE_WRONG_OLDPASS),
                    Message = ErrorMessages.USER_UPDATE_WRONG_OLDPASS
                };

            return new ApiResponse<string> { IsSuccess = true };
        }

        /// <summary>
        /// Update user information
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("update")]
        public async Task<ApiResponse<UpdateUserResponseModel>> Update(UpdateUserCommand command)
        {
            if (!ModelState.IsValid)
            {
                var errorList = ModelState.Where(x => x.Value.Errors.Any()).Select(x => x.Key + " - " + x.Value.Errors.FirstOrDefault()?.ErrorMessage + " - " + x.Value.Errors.FirstOrDefault()?.Exception).ToList();
                return new ApiResponse<UpdateUserResponseModel>
                {
                    IsSuccess = false,
                    Data = null,
                    Message = string.Join(" ", errorList)
                };
            }

            command.UserId = GetUserIdFromToken();

            var res = await _mediator.Send(command);

            if (res == null)
            {
                return new ApiResponse<UpdateUserResponseModel>
                {
                    Code = nameof(ErrorMessages.DUPLICATED_CRIDENTIAL),
                    Message = ErrorMessages.DUPLICATED_CRIDENTIAL
                };
            }
            else if (Guid.Equals(Guid.Empty, res.Id))
            {
                return new ApiResponse<UpdateUserResponseModel>
                {
                    Code = nameof(ErrorMessages.USER_NOT_FOUND),
                    Message = ErrorMessages.USER_NOT_FOUND
                };
            }

            return new ApiResponse<UpdateUserResponseModel>
            {
                IsSuccess = true,
                Data = res
            };
        }
    }
}
