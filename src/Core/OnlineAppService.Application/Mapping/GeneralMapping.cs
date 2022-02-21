using AutoMapper;
using OnlineAppService.Application.Features.Commands.Add;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineAppService.Application.Mapping
{
    internal class GeneralMapping : Profile
    {
        public GeneralMapping()
        {
             CreateMap<Domain.Entities.User, Dto.UserDto>().ReverseMap();
             CreateMap<Domain.Entities.User, AddUserCommand>().ReverseMap();
        }
    }
}
