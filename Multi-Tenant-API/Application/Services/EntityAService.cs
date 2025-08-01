using Multi_Tenant_API.Application.Interfaces;
using Multi_Tenant_API.Domain;
using Multi_Tenant_API.Infrastructure.Data;

namespace Multi_Tenant_API.Application.Services
{
    public class EntityAService : IEntityAService
    {
        IEntityARepository EntityARepository;
        private readonly TenantResolver tenantResolver;

        public EntityAService(IEntityARepository EntityARepository, TenantResolver tenantResolver) {
            
            this.EntityARepository = EntityARepository;
            this.tenantResolver = tenantResolver;
        }

        public async Task<bool> AddEntity(EntityA entityA) {

            int? tenantId = tenantResolver.GetTenantId();

            if (tenantId == null)
                throw new UnauthorizedAccessException("Tenant ID missing.");
            bool isAdded = await this.EntityARepository.AddEntity(entityA);
            return isAdded; 
        }

        public async Task<bool> DeleteEntity(int id) {
            int? tenantId = tenantResolver.GetTenantId();

            if (tenantId == null)
                throw new UnauthorizedAccessException("Tenant ID missing.");
            bool isDeleted= await this.EntityARepository.DeleteEntity(id);
            return isDeleted;


        }

        public async Task<ICollection<EntityA>> GetEntities() {
            int? tenantId = tenantResolver.GetTenantId();

            if (tenantId == null)
                throw new UnauthorizedAccessException("Tenant ID missing.");
            return await this.EntityARepository.GetEntities();
        }

        public async Task<bool> UpdateEntity(EntityA entityA) {
            int? tenantId = tenantResolver.GetTenantId();

            if (tenantId == null)
                throw new UnauthorizedAccessException("Tenant ID missing.");
            bool isUpdated= await this.EntityARepository.UpdateEntity(entityA);
            return isUpdated;

        }

        public async Task<int?> Auth(String key) { 
            
            return await this.EntityARepository.GetTenantId(key);
        }


    }
}
