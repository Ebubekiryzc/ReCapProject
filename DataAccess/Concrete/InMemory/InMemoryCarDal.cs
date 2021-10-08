using Core.DataAccess.InMemory;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess.Concrete.InMemory
{
    public class InMemoryCarDal : InMemoryRepositoryBase<Car>, ICarDal
    {
        private List<Brand> _brands;
        private List<Color> _colors;

        public InMemoryCarDal(List<Color> colors, List<Brand> brands)
        {
            _colors = colors;
            _brands = brands;
        }

        public List<CarDetailDto> GetCarsWithDetails()
        {
            var result = from car in Instances
                         join brand in _brands on car.BrandId equals brand.Id
                         join color in _colors on car.ColorId equals color.Id
                         select new CarDetailDto
                         {
                             CarId = car.Id,
                             CarName = car.Description,
                             BrandName = brand.Name,
                             ColorName = color.Name,
                             DailyPrice = car.DailyPrice,
                             ModelYear = car.ModelYear
                         };
            return result.ToList();
        }

        public CarDetailDto GetCarWithDetailsById(int id)
        {
            var result = from car in Instances
                         join brand in _brands on car.BrandId equals brand.Id
                         join color in _colors on car.ColorId equals color.Id
                         where car.Id == id
                         select new CarDetailDto
                         {
                             CarId = car.Id,
                             CarName = car.Description,
                             BrandName = brand.Name,
                             ColorName = color.Name,
                             DailyPrice = car.DailyPrice,
                             ModelYear = car.ModelYear
                         };
            return result.SingleOrDefault();
        }

        public List<CarDetailDto> GetCarsWithDetailsByBrandId(int brandId)
        {
            var result = from car in Instances
                         join brand in _brands on car.BrandId equals brand.Id
                         join color in _colors on car.ColorId equals color.Id
                         where car.BrandId == brandId
                         select new CarDetailDto
                         {
                             CarId = car.Id,
                             CarName = car.Description,
                             BrandName = brand.Name,
                             ColorName = color.Name,
                             DailyPrice = car.DailyPrice,
                             ModelYear = car.ModelYear
                         };
            return result.ToList();
        }

        public List<CarDetailDto> GetCarsWithDetailsByColorId(int colorId)
        {
            var result = from car in Instances
                         join brand in _brands on car.BrandId equals brand.Id
                         join color in _colors on car.ColorId equals color.Id
                         where car.ColorId == colorId
                         select new CarDetailDto
                         {
                             CarId = car.Id,
                             CarName = car.Description,
                             BrandName = brand.Name,
                             ColorName = color.Name,
                             DailyPrice = car.DailyPrice,
                             ModelYear = car.ModelYear
                         };
            return result.ToList();
        }

        public List<CarDetailDto> GetCarsWithDetailsByBrandIdAndColorId(int brandId, int colorId)
        {
            var result = from car in Instances
                         join brand in _brands on car.BrandId equals brand.Id
                         join color in _colors on car.ColorId equals color.Id
                         where car.BrandId == brandId && car.ColorId == colorId
                         select new CarDetailDto
                         {
                             CarId = car.Id,
                             CarName = car.Description,
                             BrandName = brand.Name,
                             ColorName = color.Name,
                             DailyPrice = car.DailyPrice,
                             ModelYear = car.ModelYear
                         };
            return result.ToList();
        }

        public List<CarDetailDto> GetTopSixDealsDetails()
        {
            throw new System.NotImplementedException();
        }
    }
}
