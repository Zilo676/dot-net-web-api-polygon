using Core.Utils.Unit;
using Microsoft.EntityFrameworkCore.Storage;
using Orders.Data;

namespace Orders.Service;

public class UnitOfWork : IUnitOfWork
{
    private readonly OrdersContext _context;

    public UnitOfWork(OrdersContext dbContext)
    {
        this._context = dbContext;
    }

    public IDbContextTransaction BeginTransaction()
    {
        return _context.Database.BeginTransaction();
    }

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return _context.SaveChangesAsync(cancellationToken);
    }
}