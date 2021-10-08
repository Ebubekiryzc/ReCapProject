using System.Collections.Generic;
using Core.DataAccess.InMemory;
using Core.Entities.Concrete;
using DataAccess.Abstract;
using Entities.Concrete;

namespace DataAccess.Concrete.InMemory
{
    public class InMemoryUserDal : InMemoryRepositoryBase<User>, IUserDal
    {
        public List<OperationClaim> GetClaims(User user)
        {
            throw new System.NotImplementedException();
        }
    }
}
