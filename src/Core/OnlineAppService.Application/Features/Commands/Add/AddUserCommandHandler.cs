using AutoMapper;
using MediatR;
using OnlineAppService.Application.Interfaces.Repository;
using OnlineAppService.Application.Wrappers;
using OnlineAppService.Domain.Entities;
using OnlineAppService.Model.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineAppService.Application.Features.Commands.Add
{
    public class AddUserCommandHandler : IRequestHandler<AddUserCommand, AddUserResponse>
    {
        IUserRepository _userRepository;
        public IMapper _mapper { get; }
        public AddUserCommandHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }


        public async Task<AddUserResponse> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {
            var user = _mapper.Map<User>(request);
            await _userRepository.AddAsync(user);

            return new AddUserResponse
            {
                Name = user.Name,
                Surname = user.Surname,
                Email = user.Email
            };
        }
    }
}
