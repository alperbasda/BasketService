using Core.CrossCuttingConcerns.Exceptions.Types;

namespace _1likte.Application.Base;

public class BaseBusinessRules
{
    public virtual Task ThrowExceptionIfDataNull<TEntity>(TEntity? data)
    {
        if (data == null)
            throw new NotFoundException($"{typeof(TEntity).Name} not found");

        return Task.CompletedTask;
    }

    public virtual Task ThrowExceptionIfDataNullOrEmpty<TEntity>(List<TEntity>? data)
    {
        if (data == null || !data.Any())
            throw new NotFoundException($"{typeof(TEntity).Name} not found");

        return Task.CompletedTask;
    }
}
