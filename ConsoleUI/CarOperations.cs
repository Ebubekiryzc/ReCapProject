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
    public class CarOperations : IDisposable
    {
        private ICarDal _carDal;
        private ICarService _carManager;
        private List<Brand> _brands;
        private List<Color> _colors;

        public CarOperations(out ICarDal carDal, List<Brand> brands, List<Color> colors)
        {
            _brands = brands;
            _colors = colors;
            Console.Write("Which dal do you want to use? (1- EfCarDal, 2- InMemoryCarDal): ");
            int option = Convert.ToInt32(Console.ReadLine());
            switch (option)
            {
                case 1:
                    {
                        carDal = new EfCarDal();
                        break;
                    }
                case 2:
                    {
                        if (brands.Count == 0 || colors.Count == 0)
                        {
                            Console.WriteLine("If you want to use InMemoryDal, firstly you must give a brand and color list.");
                        }
                        carDal = new InMemoryCarDal(_colors, _brands);
                        break;
                    }
                default:
                    {
                        carDal = new EfCarDal();
                        break;
                    }
            }
            _carDal = carDal;
            _carManager = new CarManager(_carDal);
        }

        public void GetAll()
        {
            var result = _carManager.GetAll();
            Console.WriteLine(result.Message);
            foreach (var car in result.Data)
            {
                Console.WriteLine("Id: {0}, Brand Id: {1}, Color Id: {2}, Model Year: {3}, Daily Price: {4}, Description: {5}", car.Id, car.BrandId, car.ColorId, car.ModelYear, car.DailyPrice, car.Description);
            }
        }

        public void GetById()
        {
            Console.Write("Please enter the id of car you want to get: ");
            int id = Convert.ToInt32(Console.ReadLine());
            var result = _carManager.GetById(id);
            var car = result.Data;
            Console.WriteLine("{0}\nId: {1}, Brand Id: {2}, Color Id: {3}, Model Year: {4}, Daily Price: {5}, Description: {6}", result.Message, car.Id, car.BrandId, car.ColorId, car.ModelYear, car.DailyPrice, car.Description);
        }

        public void GetCarsByBrandId()
        {
            Console.Write("Please enter the id of brand you want to get all cars from: ");
            int brandId = Convert.ToInt32(Console.ReadLine());
            var result = _carManager.GetCarsByBrandId(brandId);
            PrintCars(result);
        }

        public void GetCarsByColorId()
        {
            Console.Write("Please enter the id of color you want to get all cars from: ");
            int colorId = Convert.ToInt32(Console.ReadLine());
            var result = _carManager.GetCarsByColorId(colorId);
            PrintCars(result);
        }

        public void GetCarsWithDetails()
        {
            var result = _carManager.GetCarsWithDetails();
            var carsWithDetails = result.Data;
            foreach (var carWithDetails in carsWithDetails)
            {
                Console.Write("\nName: {0}, Brand Name: {1}, Color Name: {2}, Daily Price: {3}", carWithDetails.CarName, carWithDetails.BrandName, carWithDetails.ColorName, carWithDetails.DailyPrice);
            }
        }

        public void AddToDatabase(Car car)
        {
            Console.WriteLine($"{_carManager.Add(CreateCar(car)).Message}\n");
        }

        public void UpdateToDatabase(Car car)
        {
            Console.WriteLine($"{_carManager.Update(UpdateCar(car)).Message}\n");
        }

        public void DeleteFromDatabase(Car car)
        {
            Console.WriteLine($"{_carManager.Delete(DeleteCar(car)).Message}\n");
        }

        private void PrintCars(IDataResult<List<Car>> result)
        {
            var cars = result.Data;
            foreach (var car in cars)
            {
                Console.WriteLine("{0}\nId: {1}, Brand Id: {2}, Color Id: {3}, Model Year: {4}, Daily Price: {5}, Description: {6}", result.Message, car.Id, car.BrandId, car.ColorId, car.ModelYear, car.DailyPrice, car.Description);
            }

            Console.WriteLine(result.Message);
        }

        private Car UpdateCar(Car car)
        {
            var result = CheckCarExists(car);
            if (result)
            {
                Console.WriteLine("\nFor new entity:");
                EnterBodyInfo(car);
            }
            return car;
        }
        private Car DeleteCar(Car car)
        {
            var result = CheckCarExists(car);
            if (!result)
            {
                car.Id = -1;
            }

            return car;
        }

        private bool CheckCarExists(Car car)
        {
            EnterIdentityInfo(car);
            EnterBodyInfo(car);
            bool checkCarExists = _carManager.GetAll().Data.Any(c => c.Id == car.Id && c.BrandId == car.BrandId && c.ColorId == car.ColorId && c.DailyPrice == car.DailyPrice && c.Description == car.Description && c.ModelYear == car.ModelYear);
            if (!checkCarExists)
            {
                Console.WriteLine("There is no car matcing that instance.");
            }

            return checkCarExists;
        }

        private void EnterIdentityInfo(Car car)
        {
            Console.Write("Please enter the id for your car: ");
            car.Id = Convert.ToInt32(Console.ReadLine());
        }

        private void EnterBodyInfo(Car car)
        {
            Console.Write("Please enter the id of brand for your car: ");
            car.BrandId = Convert.ToInt32(Console.ReadLine());
            Console.Write("Please enter the id of color for your car: ");
            car.ColorId = Convert.ToInt32(Console.ReadLine());
            Console.Write("Please enter the daily price for your car: ");
            car.DailyPrice = Convert.ToInt32(Console.ReadLine());
            Console.Write("Please enter the description for your car: ");
            car.Description = Console.ReadLine();
            Console.Write("Please enter the model year for your car: ");
            car.ModelYear = Convert.ToInt32(Console.ReadLine());
        }

        private Car CreateCar(Car car)
        {
            car = new Car();
            EnterBodyInfo(car);
            return car;
        }

        public void Dispose()
        {
            _carManager = null;
            _carDal = null;
            _brands = null;
            _colors = null;
        }
    }
}
