using Multi_Tenant_API.Application.DTOs;
using Multi_Tenant_API.Domain;

namespace Multi_Tenant_API.Application.Interfaces
{
    public interface IEntityAService
    {
        public Task<bool> AddEntity(EntityCreationDto entityCreationDto);
        public Task<ICollection<EntityDTO>> GetEntities();

        public Task<bool> UpdateEntity(EntityCreationDto entityCreationDto, int id);
        public Task<bool> DeleteEntity(int id);

        }
    }
