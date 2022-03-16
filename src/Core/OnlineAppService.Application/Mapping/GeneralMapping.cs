using AutoMapper;
using OnlineAppService.Application.Features.Commands.Register;
using OnlineAppService.Application.Features.Commands.Update;
using OnlineAppService.Model;
using OnlineAppService.Model.ResponseModels;

namespace OnlineAppService.Application.Mapping
{
    internal class GeneralMapping : Profile
    {
        public GeneralMapping()
        {
             CreateMap<Domain.Entities.User, UserModel>().ReverseMap();
             CreateMap<Domain.Entities.User, RegisterCommand>().ReverseMap();
             CreateMap<Domain.Entities.User, UpdateUserCommand>().ReverseMap();
             CreateMap<Domain.Entities.User, UpdateUserResponseModel>().ReverseMap();
        }
    }
}
