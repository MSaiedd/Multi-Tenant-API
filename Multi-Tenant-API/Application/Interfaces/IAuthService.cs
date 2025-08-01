namespace Multi_Tenant_API.Application.Interfaces
{
    public interface IAuthService
    {
        public Task<int?> Auth(String key);
    }
}
