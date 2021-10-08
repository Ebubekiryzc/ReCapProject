using Core.DataAccess.InMemory;
using DataAccess.Abstract;
using Entities.Concrete;

namespace DataAccess.Concrete.InMemory
{
    public class InMemoryCarImageDal : InMemoryRepositoryBase<CarImage>, ICarImageDal
    {
    }
}
