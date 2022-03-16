using MediatR;
using Microsoft.Extensions.Configuration;
using OnlineAppService.Application.Extensions;
using OnlineAppService.Application.Helpers;
using OnlineAppService.Application.Interfaces.Repository;
using OnlineAppService.Model;

namespace OnlineAppService.Application.Features.Commands.Login
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, UserModel>
    {
        IUserRepository _userRepository;

        private readonly IConfiguration _configuration;

        public LoginCommandHandler(IUserRepository userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _configuration = configuration;
        }

        public async Task<UserModel> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            GenerateJwtHelper generateJwtHelper = new GenerateJwtHelper(_configuration);

            var hashedPass = request.Password.HashToSha256();

            var user = await _userRepository.Login(request.Email, hashedPass);

            if (user == null)
                return null;

            generateJwtHelper.GenerateJwtToken(user);

            return user;
        }
    }
}
