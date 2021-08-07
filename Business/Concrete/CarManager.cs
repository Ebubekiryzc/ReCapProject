using System.Collections.Generic;
using Business.Abstract;
using Business.Constants;
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
        public IDataResult<List<Car>> GetAll()
        {
            return new SuccessDataResult<List<Car>>(_carDal.GetAll(), Messages.CarsListed);
        }
        public IDataResult<List<Car>> GetCarsByBrandId(int brandId)
        {
            return new SuccessDataResult<List<Car>>(_carDal.GetAll(c => c.BrandId == brandId), Messages.CarsListed);
        }

        public IDataResult<List<Car>> GetCarsByColorId(int colorId)
        {
            return new SuccessDataResult<List<Car>>(_carDal.GetAll(c => c.ColorId == colorId), Messages.CarsListed);
        }

        public IDataResult<Car> GetById(int id)
        {
            return new SuccessDataResult<Car>(_carDal.Get(c => c.Id == id), Messages.CarListedById);
        }

        public IDataResult<List<CarDetailDto>> GetCarsWithDetail()
        {
            return new SuccessDataResult<List<CarDetailDto>>(_carDal.GetCarsWithDetail(), Messages.CarsListed);
        }

        public IResult Add(Car car)
        {
            if (CheckDescription(car) || CheckDailyPrice(car))
            {
                return new ErrorResult(Messages.CarInvalid);
            }
            _carDal.Add(car);
            return new SuccessResult(Messages.OperationSuccessful);
        }

        public IResult Update(Car car)
        {
            _carDal.Update(car);
            return new SuccessResult(Messages.OperationSuccessful);
        }

        public IResult Delete(Car car)
        {
            _carDal.Delete(car);
            return new SuccessResult(Messages.OperationSuccessful);
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
