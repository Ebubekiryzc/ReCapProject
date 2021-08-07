using System.Collections.Generic;
using Business.Abstract;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;

namespace Business.Concrete
{
    public class CarManager : ICarService
    {
        private ICarDal _carDal;
        public CarManager(ICarDal carDal)
        {
            _carDal = carDal;
        }

        public IDataResult<Car> GetById(int id)
        {
            return new SuccessDataResult<Car>(_carDal.Get(c => c.Id == id), "Car listed by id.");
        }

        public IDataResult<List<Car>> GetCarsByBrandId(int brandId)
        {
            return new SuccessDataResult<List<Car>>(_carDal.GetAll(c => c.BrandId == brandId), "Cars listed by brand id.");
        }

        public IDataResult<List<Car>> GetCarsByColorId(int colorId)
        {
            return new SuccessDataResult<List<Car>>(_carDal.GetAll(c => c.ColorId == colorId), "Cars listed by color id.");
        }

        public IDataResult<List<Car>> GetAll()
        {
            return new SuccessDataResult<List<Car>>(_carDal.GetAll(), "All cars listed.");
        }

        public IDataResult<List<CarDetailDto>> GetCarsWithDetail()
        {
            return new SuccessDataResult<List<CarDetailDto>>(_carDal.GetCarsWithDetail(), "All cars listed with details.");
        }

        public IResult Add(Car car)
        {
            if (CheckDescription(car) || CheckDailyPrice(car))
            {
                return new ErrorResult("An error has occurred when adding car.");
            }
            _carDal.Add(car);
            return new SuccessResult("Car added successfully.");
        }

        public IResult Update(Car car)
        {
            _carDal.Update(car);
            return new SuccessResult("Car updated successfully.");
        }

        public IResult Delete(Car car)
        {
            _carDal.Delete(car);
            return new SuccessResult("Car deleted successfully.");
        }

        public bool CheckDescription(Car car)
        {
            return car.Description.Length < 2;
        }

        public bool CheckDailyPrice(Car car)
        {
            return car.DailyPrice <= 0;
        }
    }
}
