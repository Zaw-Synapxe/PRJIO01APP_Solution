namespace Application.Interfaces.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IDeveloperRepository Developers { get; }
        IProjectRepository Projects { get; }
        Task<int> CompleteAsync();
    }
}
