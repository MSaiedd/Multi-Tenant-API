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
        IUnitOfWork unitOfWork;
        private readonly TenantResolver tenantResolver;
        private readonly ILogger<AuthService> logger;

        public EntityAService(
            IEntityARepository EntityARepository,
            TenantResolver tenantResolver,
            IUnitOfWork unitOfWork,
            ILogger<AuthService> logger
            )
        {

            this.EntityARepository = EntityARepository;
            this.tenantResolver = tenantResolver;
            this.unitOfWork= unitOfWork;
            this.logger = logger;
        }

        public async Task<bool> AddEntity([FromBody]EntityCreationDto entityCreationDto) {

            int? tenantId = tenantResolver.GetTenantId();

            if (tenantId == null) {

                logger.LogError("Tenant ID Missing --> Cant Add Entity");
                throw new UnauthorizedAccessException("Tenant ID missing.");

            }
                
            bool isAdded = await this.EntityARepository.AddEntity(EntityAMapper.ToEntity(entityCreationDto,(int)tenantId));
            if (isAdded) {
                logger.LogDebug("Entity Added ");
                await unitOfWork.Save();
                logger.LogInformation("Entity Added Succsessfully");
            }
                
            return isAdded;
        }

        public async Task<bool> DeleteEntity(int id) {
            int? tenantId = tenantResolver.GetTenantId();

            int? entityId = await this.EntityARepository.GetTenantIdOfEntity(id);
            if (tenantId == null || entityId == null || tenantId != entityId) {
                logger.LogError("Tenant ID Missing And Cant Add Entity");
                throw new UnauthorizedAccessException("Tenant ID missing.");

            }
               

            bool isDeleted = await this.EntityARepository.DeleteEntity(id);
            if (isDeleted) {
                logger.LogDebug("Entity Found ");
                await unitOfWork.Save();
                logger.LogInformation("Entity Deleted Succsessfully");

            }
            return isDeleted;


        }

        public async Task<ICollection<EntityDTO>> GetEntities() {
            int? tenantId = tenantResolver.GetTenantId();

            if (tenantId == null) {
                logger.LogError("Tenant ID Missing And Cant Fetch Entity");
                throw new UnauthorizedAccessException("Tenant ID missing.");
            }
                

            var entities = await EntityARepository.GetEntities((int)tenantId);

            return EntityAMapper.ToDtoList(entities);
        }

        public async Task<bool> UpdateEntity(EntityCreationDto entityCreationDto, int id) {

            int? tenantId = tenantResolver.GetTenantId();

            int? entityId = await this.EntityARepository.GetTenantIdOfEntity(id);


            if (tenantId == null || entityId == null || tenantId != entityId) {

                logger.LogError("Tenant ID Missing And Cant Update Entity");
                throw new UnauthorizedAccessException("Tenant ID missing.");

            }
               

            var dto = new EntityDTO { Id = id, Name = entityCreationDto.Name};

            bool isUpdated = await this.EntityARepository.UpdateEntity(EntityAMapper.ToEntity(dto, (int)tenantId));
            if (isUpdated) {
                logger.LogDebug("Entity Found ");
                await unitOfWork.Save();
                logger.LogInformation("Entity Updated Succsessfully");
            }
            return isUpdated;

        }

        


    }
}
