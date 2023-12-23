using _1likte.Domain.MongoEntities;
using Core.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1likte.Application.Contracts;

public interface IBasketDal : IAsyncRepository<Basket,Guid>
{
}
