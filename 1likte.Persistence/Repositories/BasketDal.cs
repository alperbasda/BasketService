using _1likte.Application.Contracts;
using _1likte.Domain.MongoEntities;
using Core.Persistence.Models;
using MongoDbAdapter.Repository;

namespace _1likte.Persistence.Repositories;

public class BasketDal : MongoRepositoryBase<Basket, Guid>, IBasketDal
{
    public BasketDal(DatabaseOptions settings) : base(settings)
    {
    }
}
