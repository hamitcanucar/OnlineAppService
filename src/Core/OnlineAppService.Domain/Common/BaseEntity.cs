namespace OnlineAppService.Domain.Common
{
    public class BaseEntity
    {
        public Guid Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsSoftDeleted { get; set; } = false;
        public bool IsActive { get; set; }
    }
}
