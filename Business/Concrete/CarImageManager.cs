using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Transaction;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Business;
using Core.Utilities.Helpers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

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
        [TransactionScopeAspect]
        public IResult Add(IFormFile image, CarImage carImage)
        {
            var result = BusinessRules.Check(CheckIfCarHaveMoreThanFiveImage(carImage.CarId), FileHelper.AddAsync(image, carImage.ImagePath));
            if (result is ErrorResult)
            {
                return result;
            }
            carImage.ImagePath = ((SuccessDataResult<string>)result).Data;

            SetUploadDateToNow(carImage);
            _carImageDal.Add(carImage);
            return new SuccessResult(Messages.OperationSuccessful);
        }

        [ValidationAspect(typeof(CarImageValidator))]
        [TransactionScopeAspect]
        public IResult Update(IFormFile image, CarImage carImage)
        {
            var path = Path.GetDirectoryName(carImage.ImagePath);
            var result = BusinessRules.Check(GetById(carImage.Id), FileHelper.DeleteAsync(carImage.ImagePath), FileHelper.AddAsync(image, path));
            if (!result.Success)
            {
                return result;
            }
            carImage.ImagePath = Path.Combine(path, ((SuccessDataResult<string>)result).Data);

            SetUploadDateToNow(carImage);
            _carImageDal.Update(carImage);
            return new SuccessResult(Messages.OperationSuccessful);
        }

        [TransactionScopeAspect]
        public IResult Delete(CarImage carImage)
        {
            _carImageDal.Delete(carImage);

            var operationResult = BusinessRules.Check(FileHelper.DeleteAsync(carImage.ImagePath));
            if (operationResult is ErrorResult)
            {
                return operationResult;
            }

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
