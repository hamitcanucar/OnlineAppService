using OnlineAppService.Domain.Common;

namespace OnlineAppService.Model
{
    public abstract class BaseEntityModel<TEntity, TModel> where TEntity : BaseEntity
    {
        public Guid Id { get; set; }
        public virtual void SetValuesFromEntity(TEntity entity)
        {
            Id = entity.Id;
        }
        public abstract TEntity ToEntity();
    }
}
