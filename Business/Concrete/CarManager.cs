﻿using System.Collections.Generic;
using Business.Abstract;
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

        public Car GetById(int id)
        {
            return _carDal.Get(c => c.Id == id);
        }

        public List<Car> GetCarsByBrandId(int brandId)
        {
            return _carDal.GetAll(c => c.BrandId == brandId);
        }

        public List<Car> GetCarsByColorId(int colorId)
        {
            return _carDal.GetAll(c => c.ColorId == colorId);
        }

        public List<Car> GetAll()
        {
            return _carDal.GetAll();
        }

        public List<CarDetailDto> GetCarsWithDetail()
        {
            return _carDal.GetCarsWithDetail();
        }

        public void Add(Car car)
        {
            if (checkDescription(car) && checkDailyPrice(car))
            {
                _carDal.Add(car);
            }
        }

        public void Update(Car car)
        {
            _carDal.Update(car);
        }

        public void Delete(Car car)
        {
            _carDal.Delete(car);
        }

        public bool checkDescription(Car car)
        {
            return car.Description.Length >= 2;
        }

        public bool checkDailyPrice(Car car)
        {
            return car.DailyPrice > 0;
        }
    }
}
