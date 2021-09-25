using Core.DataAccess;
using Entities.Concrete;
using Entities.DTOs;
using System.Collections.Generic;

namespace DataAccess.Abstract
{
    public interface ICarDal : IEntityRepository<Car>
    {
        List<CarDetailDto> GetCarsWithDetails();
        CarDetailDto GetCarWithDetailsById(int id);
        List<CarDetailDto> GetCarsWithDetailsByBrandId(int brandId);
        List<CarDetailDto> GetCarsWithDetailsByColorId(int colorId);
        List<CarDetailDto> GetCarsWithDetailsByBrandIdAndColorId(int brandId, int colorId);
    }
}
