namespace Multi_Tenant_API.Domain
{
    public class Tenant
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public  string ApiKey { get; set; }
        public ICollection<EntityA>? EntityA { get; set; }
    }
}
