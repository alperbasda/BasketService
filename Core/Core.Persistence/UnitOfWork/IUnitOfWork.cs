namespace Core.Persistence.UnitOfWork;

public interface IUnitOfWork
{
    [Obsolete("artık Repositorylerde yapılıyor")]
    Task<int> CommitAsync();

    [Obsolete("artık Repositorylerde yapılıyor")]
    int Commit();
}
