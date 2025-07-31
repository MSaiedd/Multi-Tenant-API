using Multi_Tenant_API.Application.Interfaces;
using Multi_Tenant_API.Domain;
using Multi_Tenant_API.Infrastructure.Data;

namespace Multi_Tenant_API.Application.Services
{
    public class EntityAService : IEntityAService
    {
        EntityARepository EntityARepository;
        public EntityAService(EntityARepository EntityARepository) {
            
            this.EntityARepository = EntityARepository;
        }

        public async Task<bool> AddEntity(EntityA entityA) { 
        
            bool isAdded = await this.EntityARepository.AddEntity(entityA);
            return isAdded; 
        }

        public async Task<bool> DeleteEntity(int id) {

            bool isDeleted= await this.EntityARepository.DeleteEntity(id);
            return isDeleted;


        }

        public async Task<ICollection<EntityA>> GetEntities() { 
        
            return await this.EntityARepository.GetEntities();
        }

        public async Task<bool> UpdateEntity(EntityA entityA) {

            bool isUpdated= await this.EntityARepository.UpdateEntity(entityA);
            return isUpdated;

        }


    }
}
