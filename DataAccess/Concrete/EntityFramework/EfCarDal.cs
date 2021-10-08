using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfCarDal : EfEntityRepositoryBase<Car, ReCapProjectContext>, ICarDal
    {
        public List<CarDetailDto> GetCarsWithDetails()
        {
            using (ReCapProjectContext context = new ReCapProjectContext())
            {
                var result = from car in context.Cars
                             join brand in context.Brands on car.BrandId equals brand.Id
                             join color in context.Colors on car.ColorId equals color.Id
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
        }

        public CarDetailDto GetCarWithDetailsById(int id)
        {
            using (ReCapProjectContext context = new ReCapProjectContext())
            {
                var result = from car in context.Cars
                             join brand in context.Brands on car.BrandId equals brand.Id
                             join color in context.Colors on car.ColorId equals color.Id
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
        }

        public List<CarDetailDto> GetCarsWithDetailsByBrandId(int brandId)
        {
            using (ReCapProjectContext context = new ReCapProjectContext())
            {
                var result = from car in context.Cars
                             join brand in context.Brands on car.BrandId equals brand.Id
                             join color in context.Colors on car.ColorId equals color.Id
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
        }

        public List<CarDetailDto> GetCarsWithDetailsByColorId(int colorId)
        {
            using (ReCapProjectContext context = new ReCapProjectContext())
            {
                var result = from car in context.Cars
                             join brand in context.Brands on car.BrandId equals brand.Id
                             join color in context.Colors on car.ColorId equals color.Id
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
        }
        public List<CarDetailDto> GetCarsWithDetailsByBrandIdAndColorId(int brandId, int colorId)
        {
            using (ReCapProjectContext context = new ReCapProjectContext())
            {
                var result = from car in context.Cars
                             join brand in context.Brands on car.BrandId equals brand.Id
                             join color in context.Colors on car.ColorId equals color.Id
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
        }

        public List<CarDetailDto> GetTopSixDealsDetails()
        {
            using (ReCapProjectContext context = new ReCapProjectContext())
            {
                var topIds = (from id in (from rental in context.Rentals
                                          join car in context.Cars on rental.CarId equals car.Id
                                          group rental by car.Id
                    into grouping
                                          orderby grouping.Count() descending
                                          select new
                                          {
                                              Id =
                                          grouping.Key
                                          })
                              select id.Id).Take(6);

                var result = from car in context.Cars
                             join brand in context.Brands on car.BrandId equals brand.Id
                             join color in context.Colors on car.ColorId equals color.Id
                             join topSales in topIds on car.Id equals topSales
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
        }
    }
}
