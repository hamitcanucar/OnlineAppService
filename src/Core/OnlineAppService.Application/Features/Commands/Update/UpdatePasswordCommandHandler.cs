using AutoMapper;
using MediatR;
using OnlineAppService.Application.Interfaces.Repository;
using OnlineAppService.Domain.Entities;
using OnlineAppService.Model;

namespace OnlineAppService.Application.Features.Commands.Update
{
    internal class UpdatePasswordCommandHandler : IRequestHandler<UpdatePasswordCommand, UserModel>
    {
        IUserRepository _userRepository;
        public IMapper _mapper { get; }
        public UpdatePasswordCommandHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<UserModel> Handle(UpdatePasswordCommand request, CancellationToken cancellationToken)
        {
            return await _userRepository.UpdatePassword(request.Id, request.OldPassword, request.NewPassword);
        }
    }
}
