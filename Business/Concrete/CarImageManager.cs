using System;
using System.Collections.Generic;
using System.Linq;
using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;

namespace Business.Concrete
{
    public class CarImageManager : ICarImageService
    {
        private ICarImageDal _carImageDal;
        public CarImageManager(ICarImageDal carImageDal)
        {
            _carImageDal = carImageDal;
        }

        public IDataResult<List<CarImage>> GetAll()
        {
            return new SuccessDataResult<List<CarImage>>(_carImageDal.GetAll(), Messages.OperationSuccessful);
        }

        public IDataResult<List<CarImage>> GetByCarId(int carId)
        {
            return CheckIfCarHaveImage(carId);
        }

        public IDataResult<CarImage> GetById(int id)
        {
            return new SuccessDataResult<CarImage>(_carImageDal.Get(cI => cI.Id == id), Messages.OperationSuccessful);
        }

        [ValidationAspect(typeof(CarImageValidator))]
        public IResult Add(CarImage carImage)
        {
            var result = BusinessRules.Check(CheckIfCarHaveMoreThanFiveImage(carImage.CarId));

            if (result is ErrorResult)
            {
                return result;
            }

            SetUploadDateToNow(carImage);
            _carImageDal.Add(carImage);
            return new SuccessResult(Messages.OperationSuccessful);
        }

        [ValidationAspect(typeof(CarImageValidator))]
        public IResult Update(CarImage carImage)
        {

            SetUploadDateToNow(carImage);
            _carImageDal.Update(carImage);
            return new SuccessResult(Messages.OperationSuccessful);
        }

        public IResult Delete(CarImage carImage)
        {
            _carImageDal.Delete(carImage);
            return new SuccessResult(Messages.OperationSuccessful);
        }

        private IResult CheckIfCarHaveMoreThanFiveImage(int carId)
        {
            if (GetByCarId(carId).Data.Count >= 5)
            {
                return new ErrorResult(Messages.MaxImageOfCarExceeded);
            }

            return new SuccessResult();
        }

        private IDataResult<List<CarImage>> CheckIfCarHaveImage(int carId)
        {
            if (_carImageDal.GetAll().Any(ci => ci.CarId == carId))
            {
                return new SuccessDataResult<List<CarImage>>(_carImageDal.GetAll(ci => ci.CarId == carId), Messages.OperationSuccessful);
            }

            return new SuccessDataResult<List<CarImage>>(
                new List<CarImage>
                    { new() { CarId = carId, ImagePath = DefaultRoutes.DefaultImage, UploadDate = DateTime.Now } },
                Messages.OperationSuccessful);
        }

        private void SetUploadDateToNow(CarImage carImage)
        {
            carImage.UploadDate = DateTime.Now;
        }
    }
}
