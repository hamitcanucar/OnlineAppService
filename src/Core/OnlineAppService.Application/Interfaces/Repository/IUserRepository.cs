using OnlineAppService.Domain.Entities;
using OnlineAppService.Model;
using OnlineAppService.Model.ResponseModels;

namespace OnlineAppService.Application.Interfaces.Repository
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<UserModel> Login(string email, string password);
        Task<User> Register(User user);
        Task<UpdateUserResponseModel> Update(Guid id, UserModel user);
        Task<UserModel> UpdatePassword (Guid id, string oldPassword, string newPassword);
    }
}