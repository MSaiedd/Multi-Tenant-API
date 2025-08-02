namespace Multi_Tenant_API.Application.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        Task Save();
    }
}
