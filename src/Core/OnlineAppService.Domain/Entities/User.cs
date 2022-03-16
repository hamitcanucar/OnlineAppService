using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineAppService.Domain.Common;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace OnlineAppService.Domain.Entities
{
    public class Role
    {
        public const string USER = "User";
        public const string POWER_USER = "PowerUser";
        public const string ADMIN = "Admin";
    }

    public class JWTUser
    {
        public const string ID = "id";
    }

    public class User : BaseEntity
    {
        [EmailAddress, MaxLength(128)]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }

        [MinLength(3),MaxLength(50), DataType(DataType.Text)]
        public string Name { get; set; }

        [MinLength(3),MaxLength(50), DataType(DataType.Text)]
        public string Surname { get; set; }

        [DataType(DataType.PhoneNumber), MaxLength(20),]
        public string PhoneNumber { get; set; }
        public UserTypes UserType { get; set; }
    }

    public class UserEntityConfiguration : EntityConfiguration<User>
    {
        public UserEntityConfiguration() : base("user")
        {
        }

        public override void Configure(EntityTypeBuilder<User> builder)
        {
            base.Configure(builder);

            builder.HasIndex(u => u.Email).IsUnique();

            builder.HasIndex(u => u.UserType);

            builder.Property(u => u.Email)
                .HasColumnName("email")
                .HasColumnType("varchar(64)")
                .IsRequired();
            builder.Property(u => u.Password)
                .HasColumnName("password")
                .HasColumnType("varchar(128)")
                .IsRequired();
            builder.Property(u => u.Name)
                .HasColumnName("first_name")
                .HasColumnType("varchar(64)");
            builder.Property(u => u.Surname)
                .HasColumnName("last_name")
                .HasColumnType("varchar(64)");
            builder.Property(u => u.PhoneNumber)
                .HasColumnName("phone_number")
                .HasColumnType("varchar(64)");

            builder.Property(u => u.UserType)
                .HasColumnName("user_type")
                .HasColumnType("varchar(16)")
                .HasDefaultValue(UserTypes.User)
                .HasConversion(
                    ut => ut.ToString(),
                    ut => (UserTypes)Enum.Parse(typeof(UserTypes), ut)
                );
        }
    }
}
