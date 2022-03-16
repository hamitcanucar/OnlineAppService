using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations;

namespace OnlineAppService.Domain.Common
{
    public class BaseEntity
    {
        public BaseEntity()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }

        [DataType(DataType.DateTime,ErrorMessage = "Created date must be date time format!")]
        public DateTime CreatedDate { get; set; }

        [DataType(DataType.DateTime, ErrorMessage = "Updated date must be date time format!")]
        public DateTime UpdatedDate { get; set; }

        public bool IsSoftDeleted { get; set; }

        public bool IsActive { get; set; }
    }

    public class EntityConfiguration<T> : IEntityTypeConfiguration<T> where T : BaseEntity
    {
        private readonly string tableName;

        public EntityConfiguration(string tableName)
        {
            this.tableName = tableName;
        }

        public virtual void Configure(EntityTypeBuilder<T> builder)
        {
            builder.ToTable(tableName);
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                .HasColumnName("id")
                .HasDefaultValueSql("uuid_generate_v4()");
            builder.Property(e => e.CreatedDate)
                .HasColumnName("created_date")
                .HasDefaultValueSql("now()");
            builder.Property(e => e.UpdatedDate)
                .HasColumnName("created_date")
                .HasDefaultValueSql("now()");
            builder.Property(e => e.IsSoftDeleted)
                .HasColumnName("is_soft_deleted");
            builder.Property(e => e.IsActive)
                .HasColumnName("is_active");
        }
    }
}
