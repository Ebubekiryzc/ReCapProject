using Core.Utilities.Results;
using Entities.Concrete;
using Entities.DTOs;
using System.Collections.Generic;

namespace Business.Abstract
{
    public interface ICarService
    {
        IDataResult<List<Car>> GetAll();
        IDataResult<List<Car>> GetCarsByBrandId(int brandId);
        IDataResult<List<Car>> GetCarsByColorId(int colorId);
        IDataResult<Car> GetById(int id);
        IDataResult<List<CarDetailDto>> GetCarsWithDetails();
        IDataResult<CarDetailDto> GetCarWithDetailsById(int id);
        IDataResult<List<CarDetailDto>> GetCarsWithDetailsByBrandId(int brandId);
        IDataResult<List<CarDetailDto>> GetCarsWithDetailsByColorId(int colorId);
        IDataResult<List<CarDetailDto>> GetCarsWithDetailsByBrandIdAndColorId(int brandId, int colorId);
        IResult Add(Car car);
        IResult Update(Car car);
        IResult Delete(Car car);
    }
}
