using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Transaction;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System.Collections.Generic;

namespace Business.Concrete
{
    public class CarManager : ICarService
    {
        private ICarDal _carDal;
        private ICarImageService _carImageService;

        public CarManager(ICarDal carDal, ICarImageService carImageService)
        {
            _carDal = carDal;
            _carImageService = carImageService;
        }

        [CacheAspect]
        public IDataResult<List<Car>> GetAll()
        {
            return new SuccessDataResult<List<Car>>(_carDal.GetAll(), Messages.CarsListed);
        }

        [CacheAspect]
        public IDataResult<List<Car>> GetCarsByBrandId(int brandId)
        {
            return new SuccessDataResult<List<Car>>(_carDal.GetAll(c => c.BrandId == brandId), Messages.CarsListed);
        }

        [CacheAspect]
        public IDataResult<List<Car>> GetCarsByColorId(int colorId)
        {
            return new SuccessDataResult<List<Car>>(_carDal.GetAll(c => c.ColorId == colorId), Messages.CarsListed);
        }

        [CacheAspect]
        public IDataResult<Car> GetById(int id)
        {
            return new SuccessDataResult<Car>(_carDal.Get(c => c.Id == id), Messages.CarListed);
        }

        public IDataResult<List<CarDetailDto>> GetCarsWithDetails()
        {
            return new SuccessDataResult<List<CarDetailDto>>(_carDal.GetCarsWithDetails(), Messages.CarsListed);
        }

        public IDataResult<CarDetailDto> GetCarWithDetailsById(int id)
        {
            return new SuccessDataResult<CarDetailDto>(_carDal.GetCarWithDetailsById(id), Messages.CarListed);
        }

        public IDataResult<List<CarDetailDto>> GetCarsWithDetailsByBrandId(int brandId)
        {
            return new SuccessDataResult<List<CarDetailDto>>(_carDal.GetCarsWithDetailsByBrandId(brandId),
                Messages.CarsListed);
        }

        public IDataResult<List<CarDetailDto>> GetCarsWithDetailsByColorId(int colorId)
        {
            return new SuccessDataResult<List<CarDetailDto>>(_carDal.GetCarsWithDetailsByColorId(colorId),
                Messages.CarsListed);
        }

        public IDataResult<List<CarDetailDto>> GetCarsWithDetailsByBrandIdAndColorId(int brandId, int colorId)
        {
            return new SuccessDataResult<List<CarDetailDto>>(
                _carDal.GetCarsWithDetailsByBrandIdAndColorId(brandId, colorId), Messages.CarsListed);
        }

        [CacheRemoveAspect("ICarService.Get")]
        //[SecuredOperation("admin,moderator")]
        [ValidationAspect(typeof(CarValidator))]
        public IResult Add(Car car)
        {
            _carDal.Add(car);
            return new SuccessResult(Messages.OperationSuccessful);
        }

        [CacheRemoveAspect("ICarService.Get")]
        //[SecuredOperation("admin,moderator")]
        [ValidationAspect(typeof(CarValidator))]
        public IResult Update(Car car)
        {
            _carDal.Update(car);
            return new SuccessResult(Messages.OperationSuccessful);
        }

        [CacheRemoveAspect("ICarService.Get")]
        //[SecuredOperation("admin,moderator")]
        [TransactionScopeAspect]
        public IResult Delete(Car car)
        {
            bool result = false;
            var imageList = _carImageService.GetByCarId(car.Id).Data;
            if (imageList.Count == 1)
            {
                result = imageList[0].ImagePath.Equals(DefaultRoutes.DefaultImage);
            }

            if (!result)
            {
                foreach (CarImage image in imageList)
                {
                    _carImageService.Delete(image);
                }
            }

            _carDal.Delete(car);
            return new SuccessResult(Messages.OperationSuccessful);
        }
    }
}
