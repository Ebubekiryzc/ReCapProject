using Business.Abstract;
using Business.Concrete;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using DataAccess.Concrete.InMemory;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleUI
{
    public class CarImageOperations : IDisposable
    {
        private ICarImageDal _carImageDal;
        private ICarImageService _carImageManager;

        public CarImageOperations(out ICarImageDal carImageDal)
        {
            Console.Write("Which dal do you want to use? (1- EfCarImageDal, 2- InMemoryCarImageDal): ");
            int option = Convert.ToInt32(Console.ReadLine());

            switch (option)
            {
                case 1:
                    {
                        carImageDal = new EfCarImageDal();
                        break;
                    }
                case 2:
                    {
                        carImageDal = new InMemoryCarImageDal();
                        break;
                    }
                default:
                    {
                        carImageDal = new EfCarImageDal();
                        break;
                    }
            }

            _carImageDal = carImageDal;
            _carImageManager = new CarImageManager(_carImageDal);
        }

        public void GetAll()
        {
            var result = _carImageManager.GetAll();
            PrintCarImages(result);
        }

        public void GetCarImagesByCarId()
        {
            Console.Write("Please enter the id of car you want to get all car images from: ");
            int carId = Convert.ToInt32(Console.ReadLine());
            var result = _carImageManager.GetByCarId(carId);
            PrintCarImages(result);
        }

        public void GetById()
        {
            Console.Write("Please enter the id of brand you want to get: ");
            int id = Convert.ToInt32(Console.ReadLine());
            var result = _carImageManager.GetById(id).Data;
            Console.WriteLine("Id: {0}, Car Id: {1}, Image Path: {2}, Upload Date: {3}", result.Id, result.CarId, result.ImagePath, result.UploadDate);
        }

        public void AddToDatabase(CarImage carImage)
        {
            Console.WriteLine($"{_carImageManager.Add(CreateCarImage(carImage)).Message}\n");
        }

        public void UpdateToDatabase(CarImage carImage)
        {
            Console.WriteLine($"{_carImageManager.Update(UpdateCar(carImage)).Message}\n");
        }

        public void DeleteFromDatabase(CarImage carImage)
        {
            Console.WriteLine($"{_carImageManager.Delete(DeleteCar(carImage)).Message}\n");
        }

        private void PrintCarImages(IDataResult<List<CarImage>> result)
        {
            var carImages = result.Data;
            Console.WriteLine(result.Message);
            foreach (var carImage in result.Data)
            {
                Console.WriteLine("Id: {0}, Car Id: {1}, Image Path: {2}, Upload Date: {3}", carImage.Id, carImage.CarId, carImage.ImagePath, carImage.UploadDate);
            }
        }

        private CarImage UpdateCar(CarImage carImage)
        {
            var result = CheckCarImageExists(carImage);
            if (result)
            {
                Console.WriteLine("\nFor new entity:");
                EnterBodyInfo(carImage);
            }
            return carImage;
        }
        private CarImage DeleteCar(CarImage carImage)
        {
            var result = CheckCarImageExists(carImage);
            if (!result)
            {
                carImage.Id = -1;
            }

            return carImage;
        }

        private bool CheckCarImageExists(CarImage carImage)
        {
            EnterIdentityInfo(carImage);
            EnterBodyInfo(carImage);
            bool checkCarExists = _carImageManager.GetAll().Data.Any(c => c.Id == carImage.Id && c.CarId == carImage.CarId && c.ImagePath == carImage.ImagePath);
            if (!checkCarExists)
            {
                Console.WriteLine("There is no car image matcing that instance.");
            }

            return checkCarExists;
        }

        private void EnterIdentityInfo(CarImage carImage)
        {
            Console.Write("Please enter the id for your car image: ");
            carImage.Id = Convert.ToInt32(Console.ReadLine());
        }

        private void EnterBodyInfo(CarImage carImage)
        {
            Console.Write("Please enter the car id for your car image: ");
            carImage.CarId = Convert.ToInt32(Console.ReadLine());
            Console.Write("Please enter the path for your car image: ");
            carImage.ImagePath = Console.ReadLine();
        }

        private CarImage CreateCarImage(CarImage carImage)
        {
            carImage = new CarImage();
            EnterBodyInfo(carImage);
            return carImage;
        }

        public void Dispose()
        {
            _carImageDal = null;
            _carImageManager = null;
        }
    }
}
