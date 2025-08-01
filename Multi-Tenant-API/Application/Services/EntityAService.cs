using Microsoft.AspNetCore.Mvc;
using Multi_Tenant_API.Application.DTOs;
using Multi_Tenant_API.Application.Interfaces;
using Multi_Tenant_API.Application.Mapping;
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

        public async Task<bool> AddEntity([FromBody]EntityCreationDto entityCreationDto) {

            int? tenantId = tenantResolver.GetTenantId();

            if (tenantId == null)
                throw new UnauthorizedAccessException("Tenant ID missing.");
            bool isAdded = await this.EntityARepository.AddEntity(EntityAMapper.ToEntity(entityCreationDto,(int)tenantId));
            return isAdded;
        }

        public async Task<bool> DeleteEntity(int id) {
            int? tenantId = tenantResolver.GetTenantId();

            int? entityId = await this.EntityARepository.GetTenantIdOfEntity(id);
            if (tenantId == null || entityId == null || tenantId != entityId)
                throw new UnauthorizedAccessException("Tenant ID missing.");

            bool isDeleted = await this.EntityARepository.DeleteEntity(id);
            return isDeleted;


        }

        public async Task<ICollection<EntityDTO>> GetEntities() {
            int? tenantId = tenantResolver.GetTenantId();

            if (tenantId == null)
                throw new UnauthorizedAccessException("Tenant ID missing.");

            var entities = await EntityARepository.GetEntities((int)tenantId);

            return EntityAMapper.ToDtoList(entities);
        }

        public async Task<bool> UpdateEntity(EntityCreationDto entityCreationDto, int id) {

            Console.WriteLine($"Error updating entity: ", id);
            int? tenantId = tenantResolver.GetTenantId();

            int? entityId = await this.EntityARepository.GetTenantIdOfEntity(id);


            if (tenantId == null || entityId==null ||tenantId!=entityId)
                throw new UnauthorizedAccessException("Tenant ID missing.");

            var dto = new EntityDTO { Id = id, Name = entityCreationDto.Name};

            bool isUpdated = await this.EntityARepository.UpdateEntity(EntityAMapper.ToEntity(dto, (int)tenantId));
            return isUpdated;

        }

        


    }
}
