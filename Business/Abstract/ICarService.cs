using System.Collections.Generic;
using Entities.Concrete;
using Entities.DTOs;

namespace Business.Abstract
{
    public interface ICarService
    {
        Car GetById(int id);
        List<Car> GetCarsByBrandId(int brandId);
        List<Car> GetCarsByColorId(int colorId);
        List<Car> GetAll();
        List<CarDetailDto> GetCarsWithDetail();
        void Add(Car car);
        void Update(Car car);
        void Delete(Car car);
    }
}
