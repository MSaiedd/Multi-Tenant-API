using Multi_Tenant_API.Application.Interfaces;
using Multi_Tenant_API.Infrastructure.Data;

namespace Multi_Tenant_API.Application.Services
{
    public class AuthService : IAuthService
    {
        IEntityARepository EntityARepository;

        public AuthService(IEntityARepository EntityARepository)
        {

            this.EntityARepository = EntityARepository;
        }


        //Get tenantId to check if it matches the one in jwt
        public async Task<int?> Auth(String key)
        {

            return await this.EntityARepository.GetTenantId(key);
        }
    }
}
