using Multi_Tenant_API.Domain;

namespace Multi_Tenant_API.Application.Interfaces
{
    public interface IEntityARepository
    {
        public Task<ICollection<EntityA>> GetEntities();
        public Task<bool> DeleteEntity(int id);

        public Task<bool> AddEntity(EntityA entityA);
        public Task<bool> UpdateEntity(EntityA entityA);
        public Task<int?> GetTenantId(String key);


    }
}
