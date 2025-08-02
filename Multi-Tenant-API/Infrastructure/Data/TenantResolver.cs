namespace Multi_Tenant_API.Infrastructure.Data
    //MIDDLEWARE USE IT IN RESOLVING JWT BY KEEPING THE TENANTID
{
    public class TenantResolver
    {
        private int? tenantId;

        public int? GetTenantId() => this.tenantId;

        public void SetTenantId(int tenantId) => this.tenantId = tenantId;
    }
}
