using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Multi_Tenant_API.Application.Interfaces;
using Multi_Tenant_API.Domain;

namespace Multi_Tenant_API.Infrastructure.Data
{

    //CRUD OPERATIONS ON ENTITY 
    public class EntityARepository : IEntityARepository
    {
        private readonly ApplicationDbContext context;

        public EntityARepository(ApplicationDbContext datacontext)
        {
            context = datacontext;
        }

        public async Task<bool> AddEntity(EntityA entityA)
        {
            try
            {
                await context.EntityA.AddAsync(entityA);
                await context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false!;
            }
        }

        public async Task<bool> DeleteEntity(int id)
        {
            var entity = await context.EntityA.FindAsync(id);
            if (entity != null)
            {
                var entityEntry = context.EntityA.Update(entity);
                    entityEntry.Property(nameof(EntityA.IsDeleted)).CurrentValue = true;
                return true;
            }
            return false;
        }

        public async Task<ICollection<EntityA>> GetEntities(int id)
        {
            return await context.EntityA
            .Where(e => e.TenantId == id)
            .ToListAsync();
        }

        public async Task<bool> UpdateEntity(EntityA entityA)
        {
            var existing = await context.EntityA.FindAsync(entityA.Id);

            if (existing == null)
                return false;

            existing.Name = entityA.Name; // Only update what you need

            return true;
        }

        public async Task<int?> GetTenantIdOfEntity(int id) {

            var tenantId = await context.EntityA
           .Where(t => t.Id == id)
           .Select(t => (int?)t.TenantId)
           .FirstOrDefaultAsync();

            return tenantId;


        }

        public async Task<int?> GetTenantId(String key)
        {
            var tenantId = await context.Tenant
            .Where(t => t.ApiKey == key)
            .Select(t => (int?)t.Id)
            .FirstOrDefaultAsync();

            return tenantId;

        }

    }
}
