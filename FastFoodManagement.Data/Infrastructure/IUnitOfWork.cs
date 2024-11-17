namespace FastFoodManagement.Data.Infrastructure
{
    public interface IUnitOfWork
    {
        void Commit();
        Task CommitAsync();
    }
}
