using Multi_Tenant_API.Application.DTOs;
using Multi_Tenant_API.Domain;

//HELPER CLASS TO MAP BETWEEN ENTITYA MODEL CLASS AND DTOS 
namespace Multi_Tenant_API.Application.Mapping
{
    public static class EntityAMapper
    {
        public static EntityDTO ToDto(this EntityA entity)
        {
            return new EntityDTO
            {
                Id = entity.Id,
                Name = entity.Name
            };
        }

        
        public static EntityA ToEntity(this EntityDTO dto, int tenantId)
        {
            return new EntityA
            {
                Id = dto.Id,
                Name = dto.Name,
                TenantId = tenantId
            };
        }

        public static EntityA ToEntity(EntityCreationDto dto, int tenantId)
        {
            return new EntityA
            {
                Name = dto.Name,
                TenantId = tenantId
            };
        }



        public static List<EntityDTO> ToDtoList(IEnumerable<EntityA> entities)
        {
            return entities.Select(e => new EntityDTO
            {
                Id = e.Id,
                Name = e.Name
            }).ToList();
        }
    }
}
