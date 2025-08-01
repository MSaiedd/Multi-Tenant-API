using Multi_Tenant_API.Application.Interfaces;
using Multi_Tenant_API.Infrastructure.Data;

namespace Multi_Tenant_API.Application.Services
{
    public class AuthServise : IAuthService
    {
        IEntityARepository EntityARepository;

        public AuthServise(IEntityARepository EntityARepository, TenantResolver tenantResolver)
        {

            this.EntityARepository = EntityARepository;
        }

        public async Task<int?> Auth(String key)
        {

            return await this.EntityARepository.GetTenantId(key);
        }
    }
}
