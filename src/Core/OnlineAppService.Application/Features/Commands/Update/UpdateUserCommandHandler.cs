using AutoMapper;
using MediatR;
using OnlineAppService.Application.Interfaces.Repository;
using OnlineAppService.Domain.Entities;
using OnlineAppService.Model;
using OnlineAppService.Model.ResponseModels;

namespace OnlineAppService.Application.Features.Commands.Update
{
    internal class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, UpdateUserResponseModel>
    {
        IUserRepository _userRepository;
        public IMapper _mapper { get; }
        public UpdateUserCommandHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<UpdateUserResponseModel> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var user = _mapper.Map<User>(request);

            return await _userRepository.Update(request.UserId, user.ToModel());
        }
    }
}