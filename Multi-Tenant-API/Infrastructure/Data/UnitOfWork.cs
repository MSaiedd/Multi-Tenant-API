using Multi_Tenant_API.Application.Interfaces;

namespace Multi_Tenant_API.Infrastructure.Data
{
    // Unit of Work class to ensure atomicity and separation of concerns
    public class UnitOfWork : IUnitOfWork
    {
        ApplicationDbContext context;

        public UnitOfWork(ApplicationDbContext context)
        {
            this.context= context;
        }
        public async Task Save()
        {
            await context.SaveChangesAsync();
        }


        public void Dispose()
        {
            context.Dispose();
        }
    }
}
