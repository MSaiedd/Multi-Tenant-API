using Microsoft.EntityFrameworkCore;
using Multi_Tenant_API.Application.Interfaces;
using Multi_Tenant_API.Domain;

namespace Multi_Tenant_API.Infrastructure.Data
{
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
                Console.WriteLine($"Error adding entity: {ex.Message}");
                return false!;
            }
        }

        public async Task<bool> DeleteEntity(int id)
        {
            var entity = await context.EntityA.FindAsync(id);
            if (entity != null)
            {
                context.EntityA.Remove(entity);
                await context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<ICollection<EntityA>> GetEntities()
        {
            return await context.EntityA.ToListAsync();
        }

        public async Task<bool> UpdateEntity(EntityA entityA)
        {
            try
            {
                context.EntityA.Update(entityA);
                await context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating entity: {ex.Message}");
                return false;
            }
        }
    }
}
