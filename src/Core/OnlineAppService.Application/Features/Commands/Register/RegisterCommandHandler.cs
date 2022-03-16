using AutoMapper;
using MediatR;
using OnlineAppService.Application.Extensions;
using OnlineAppService.Application.Interfaces.Repository;
using OnlineAppService.Domain.Common;
using OnlineAppService.Domain.Entities;
using OnlineAppService.Model;

namespace OnlineAppService.Application.Features.Commands.Register
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, UserModel>
    {
        IUserRepository _userRepository;
        public IMapper _mapper { get; }
        public RegisterCommandHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<UserModel> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            var user = _mapper.Map<User>(request);

            user.Password = request.Password.HashToSha256();
            user.IsActive = true;
            user.CreatedDate = DateTime.Now;
            user.UpdatedDate = DateTime.Now;
            user.UserType = UserTypes.User;

            await _userRepository.Register(user);

            return new UserModel
            {
                Name = user.Name,
                Surname = user.Surname,
                Email = user.Email,
            };
        }
    }
}
