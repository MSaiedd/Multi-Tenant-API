namespace Multi_Tenant_API.Domain
{
    public class EntityA
    {
        public int Id { get; set; }
        public  string Name { get; set; }

        public  int TenantId { get; set; }
        public  Tenant Tenant { get; set; }
        public bool IsDeleted { get; set; }
    }
}
