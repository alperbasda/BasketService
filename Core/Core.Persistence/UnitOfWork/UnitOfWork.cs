using Microsoft.EntityFrameworkCore;

namespace Core.Persistence.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
    private readonly DbContext _context;

    public UnitOfWork(DbContext dbContext)
    {
        _context = dbContext;
    }

    [Obsolete("artık Repositorylerde yapılıyor")]
    public int Commit()
    {
        var count = _context.SaveChanges();
        if (count <= 0)
        {
            throw new Exception("Data Could Not Be Create or Update");
        }
        return count;
    }

    [Obsolete("artık Repositorylerde yapılıyor")]
    public async Task<int> CommitAsync()
    {
        var count = await _context.SaveChangesAsync();
        if (count <= 0)
        {
            throw new Exception("Data Could Not Be Create or Update");
        }
        return count;
    }
}
