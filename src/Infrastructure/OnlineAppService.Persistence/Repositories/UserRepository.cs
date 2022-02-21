using OnlineAppService.Application.Interfaces.Repository;
using OnlineAppService.Domain.Entities;
using OnlineAppService.Persistence.Context;

namespace OnlineAppService.Persistence.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(ApplicationDbContext context) : base(context)
        {

        }
    }
}
