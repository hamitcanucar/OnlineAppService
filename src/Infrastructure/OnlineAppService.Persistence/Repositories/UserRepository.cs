using Microsoft.EntityFrameworkCore;
using OnlineAppService.Application.Extensions;
using OnlineAppService.Application.Interfaces.Repository;
using OnlineAppService.Domain.Entities;
using OnlineAppService.Model;
using OnlineAppService.Model.ResponseModels;
using OnlineAppService.Persistence.Context;

namespace OnlineAppService.Persistence.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(ApplicationDbContext context) : base(context)
        {

        }

        public async Task<UserModel> Login(string email, string password)
        {
            var query = from u in _context.Users
                        where (u.Email == email) && u.Password == password
                        select u;

            var user = await query.AsNoTracking().FirstOrDefaultAsync();

            if (user == null) return null;

            return user.ToModel();
        }

        public async Task<User> Register(User user)
        {
            var result = await _context.Users.AnyAsync(u => u.Email == user.Email);

            if (result) return null;

            _context.Add(user);
            await _context.SaveChangesAsync();

            return user;
        }

        public async Task<UpdateUserResponseModel> Update(Guid id, UserModel model)
        {
            var result = await _context.Users.Where(u => Guid.Equals(u.Id, id)).FirstOrDefaultAsync();

            if (result == null)
                return new UpdateUserResponseModel { Id = Guid.Empty};

            result = await SetUpdateValues(result, model);
            if (result == null) return null;

            await _context.SaveChangesAsync();
            return new UpdateUserResponseModel
            {
                Id = result.Id,
                Email = result.Email,
                Name = result.Name,
                Surname = result.Surname,
                PhoneNumber = result.PhoneNumber
            };
        }

        public async Task<UserModel> UpdatePassword(Guid id, string oldPassword, string newPassword)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
            if (user == null) return null;

            if (oldPassword.HashToSha256() != user.Password) return new UserModel { Id = Guid.Empty };
            user.Password = newPassword.HashToSha256();
            await _context.SaveChangesAsync();

            return user.ToModel();
        }

        private async Task<User> SetUpdateValues(User user, UserModel model, string password = null)
        {
            if (!String.IsNullOrEmpty(model.Email))
            {
                //check unique columns
                var query = from u in _context.Users
                            where u.Email == model.Email
                            select new
                            {
                                u.Email
                            };
                var conflicts = await query.AsNoTracking().ToListAsync();

                foreach (var c in conflicts)
                {
                    if (c.Email == model.Email && user.Email != model.Email)
                    {
                        return null;
                    }
                }
            }

            user.Email = (String.IsNullOrEmpty(model.Email)) ? user.Email : model.Email;
            user.Name = (String.IsNullOrEmpty(model.Name)) ? user.Name : model.Name;
            user.Surname = (String.IsNullOrEmpty(model.Surname)) ? user.Surname : model.Surname;
            user.PhoneNumber = (String.IsNullOrEmpty(model.PhoneNumber)) ? user.PhoneNumber : model.PhoneNumber;

            return user;
        }
    }
}
