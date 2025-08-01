namespace Multi_Tenant_API.Infrastructure.Data
{
    public class TenantResolver
    {
        private int? tenantId;

        public int? GetTenantId() => this.tenantId;

        public void SetTenantId(int tenantId) => this.tenantId = tenantId;
    }
}
