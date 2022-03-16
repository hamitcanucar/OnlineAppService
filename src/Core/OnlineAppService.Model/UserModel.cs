using OnlineAppService.Domain.Common;
using OnlineAppService.Domain.Entities;

namespace OnlineAppService.Model
{
    public class UserModel : BaseEntityModel<User, UserModel>
    {
        public UserModel()
        {
            Id = Guid.NewGuid();
        }

        public string Email { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string PhoneNumber { get; set; }
        public UserTypes? UserType { get; set; }

        public string Token { get; set; }

        public override void SetValuesFromEntity(User entity)
        {
            if (entity == null) return;

            base.SetValuesFromEntity(entity);

            Id = entity.Id;
            Email = entity.Email;
            Name = entity.Name;
            Surname = entity.Surname;
            PhoneNumber = entity.PhoneNumber;
            UserType = entity.UserType;
        }

        public override User ToEntity()
        {
            return new User
            {
                Id = Id,
                Email = Email,
                Name = Name,
                Surname = Surname,
                UserType = UserType ?? UserTypes.User,
            };
        }
    }

    public static class UserEntityExtentions
    {
        public static UserModel ToModel(this User user)
        {
            var model = new UserModel();
            model.SetValuesFromEntity(user);
            return model;
        }
    }
}
