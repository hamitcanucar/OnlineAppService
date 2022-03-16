using AutoMapper;
using MediatR;
using OnlineAppService.Application.Interfaces.Repository;
using OnlineAppService.Model;

namespace OnlineAppService.Application.Features.Qeries.Get
{
    public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, List<UserModel>>
    {
        public IUserRepository _userRepository { get; }
        public IMapper _mapper { get; }

        public GetAllUsersQueryHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<List<UserModel>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
           var users = await _userRepository.GetAllAsync();
           var viewModel = _mapper.Map<List<UserModel>>(users);

            return viewModel;
        }
    }
}
