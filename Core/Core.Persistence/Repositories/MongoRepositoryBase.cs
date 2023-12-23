using Core.Persistence.Dynamic;
using Core.Persistence.Models;
using Core.Persistence.Paging;
using Core.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using MongoDB.Driver;
using System.Linq.Expressions;

namespace MongoDbAdapter.Repository
{
    public class MongoRepositoryBase<TEntity, TEntityId> : IAsyncRepository<TEntity, TEntityId>
        where TEntity : class, IEntity<TEntityId>, new()
    {
        protected readonly IMongoCollection<TEntity> Collection;
        private readonly DatabaseOptions _settings;

        public MongoRepositoryBase(DatabaseOptions settings)
        {
            _settings = settings;
            var client = new MongoClient(_settings.MongoConnectionString);
            var db = client.GetDatabase(_settings.DatabaseName);
            Collection = db.GetCollection<TEntity>(typeof(TEntity).Name.ToLowerInvariant());
        }


        public virtual async Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> predicate, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null, bool withDeleted = false, bool enableTracking = true, CancellationToken cancellationToken = default)
        {
            IQueryable<TEntity> queryable = Query();
            if (!enableTracking)
                queryable = queryable.AsNoTracking();
            if (include != null)
                queryable = include(queryable);
            if (!withDeleted)
                queryable = queryable.Where(w => w.DeletedTime == null);
            return queryable.FirstOrDefault(predicate);
        }

        public virtual async Task<Paginate<TEntity>> GetListAsync(Expression<Func<TEntity, bool>>? predicate = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null, int index = 0, int size = 10, bool withDeleted = false, bool enableTracking = true, CancellationToken cancellationToken = default)
        {
            IQueryable<TEntity> queryable = Query();
            if (include != null)
                queryable = include(queryable);
            if (!withDeleted)
                queryable = queryable.Where(w => w.DeletedTime == null);
            if (predicate != null)
                queryable = queryable.Where(predicate);
            if (orderBy != null)
                return orderBy(queryable).ToPaginate(index, size);
            return queryable.ToPaginate(index, size);
        }

        public virtual async Task<Paginate<TEntity>> GetListByDynamicAsync(DynamicQuery? dynamic, Expression<Func<TEntity, bool>>? predicate = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null, int index = 0, int size = 10, bool withDeleted = false, bool enableTracking = true, CancellationToken cancellationToken = default)
        {
            IQueryable<TEntity> queryable = Query().ToDynamic(dynamic);

            if (include != null)
                queryable = include(queryable);
            if (!withDeleted)
                queryable = queryable.Where(w => w.DeletedTime == null);
            if (predicate != null)
                queryable = queryable.Where(predicate);
            return queryable.ToPaginate(index, size);
        }

        public virtual async Task<bool> AnyAsync(Expression<Func<TEntity, bool>>? predicate = null, bool withDeleted = false, bool enableTracking = true, CancellationToken cancellationToken = default)
        {
            IQueryable<TEntity> queryable = Query();
            if (!withDeleted)
                queryable = queryable.Where(w => w.DeletedTime == null);
            if (predicate != null)
                queryable = queryable.Where(predicate);
            return queryable.Any();
        }

        public async Task<int> CountAsync(Expression<Func<TEntity, bool>>? predicate = null, bool withDeleted = false, bool enableTracking = true, CancellationToken cancellationToken = default)
        {
            IQueryable<TEntity> queryable = Query();
            if (!withDeleted)
                queryable = queryable.Where(w => w.DeletedTime == null);
            if (predicate != null)
                queryable = queryable.Where(predicate);
            return queryable.Count();
        }

        public IQueryable<TEntity> Query() => Collection.AsQueryable();

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            entity.CreatedTime = DateTime.Now;
            var options = new InsertOneOptions { BypassDocumentValidation = false };
            await Collection.InsertOneAsync(entity, options);
            return entity;
        }

        public async Task<ICollection<TEntity>> AddRangeAsync(ICollection<TEntity> entities)
        {
            var list = entities.ToList();
            list.ForEach(w => w.CreatedTime = DateTime.Now);
            await Collection.InsertManyAsync(list);

            return entities;

        }

        public async Task<TEntity> DeleteAsync(TEntity entity, bool permanent = false)
        {
            if (!permanent)
            {
                entity.DeletedTime = DateTime.Now;
                await UpdateAsync(entity);
            }
            else
            {
                Collection.FindOneAndDelete(x => x.Id.Equals(entity.Id));
            }
            return entity;
        }

        public async Task<ICollection<TEntity>> DeleteRangeAsync(ICollection<TEntity> entities, bool permanent = false)
        {
            if (!permanent)
            {
                foreach (var entity in entities)
                {
                    entity.DeletedTime = DateTime.Now;
                    await UpdateAsync(entity);
                }

            }
            else
            {
                foreach (var entity in entities)
                {
                    Collection.FindOneAndDelete(x => x.Id.Equals(entity.Id));
                }

            }
            return entities;
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            entity.UpdatedTime = DateTime.Now;
            return await Collection.FindOneAndReplaceAsync(x => x.Id.Equals(entity.Id), entity);
        }

        public async Task<ICollection<TEntity>> UpdateRangeAsync(ICollection<TEntity> entities)
        {
            foreach (var item in entities)
            {
                item.UpdatedTime = DateTime.Now;
                await Collection.FindOneAndReplaceAsync(x => x.Id.Equals(item.Id), item);
            }
            return entities;

        }
    }
}
