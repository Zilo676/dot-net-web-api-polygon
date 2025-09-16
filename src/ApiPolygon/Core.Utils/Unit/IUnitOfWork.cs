using Microsoft.EntityFrameworkCore.Storage;

namespace Core.Utils.Unit;

public interface IUnitOfWork
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    public IDbContextTransaction BeginTransaction();
}