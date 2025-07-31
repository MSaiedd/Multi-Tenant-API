using Multi_Tenant_API.Domain;

namespace Multi_Tenant_API.Application.Interfaces
{
    public interface IEntityAService
    {
        public Task<bool> AddEntity(EntityA entityA);
        public Task<ICollection<EntityA>> GetEntities();

        public Task<bool> UpdateEntity(EntityA entityA);
        public Task<bool> DeleteEntity(int id);
        

        }
    }
