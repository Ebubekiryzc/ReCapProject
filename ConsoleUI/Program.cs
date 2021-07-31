using System;
using Business.Abstract;
using Business.Concrete;
using DataAccess.Concrete.InMemory;
using Entities.Concrete;

namespace ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            ICarService carService = new CarManager(new InMemoryCarDal());
            Console.WriteLine("Get All:");
            GetAllCars(carService);

            Console.WriteLine("\nGet by Id:");
            Console.WriteLine(carService.GetById(1).Description);

            Console.WriteLine("\n---Add---");
            carService.Add(new Car { Id = 5, BrandId = 1, ColorId = 3, DailyPrice = 320, Description = "Ford Kuga Diesel", ModelYear = 2021 });
            Console.WriteLine("\nAfter Addition:");
            GetAllCars(carService);

            Console.WriteLine("\n---Update---");
            carService.Update(new Car { Id = 1, BrandId = 1, ColorId = 3, DailyPrice = 320, Description = "Ford Connect Diesel", ModelYear = 2017 });
            Console.WriteLine("\nAfter Update:");
            GetAllCars(carService);

            Console.WriteLine("\n---Delete---");
            carService.Delete(new Car { Id = 1, BrandId = 1, ColorId = 3, DailyPrice = 320, Description = "Ford Focus Diesel", ModelYear = 2017 });
            Console.WriteLine("\nAfter Delete:");
            GetAllCars(carService);
        }

        static void GetAllCars(ICarService carService)
        {
            foreach (var car in carService.GetAll())
            {
                Console.WriteLine(car.Description);
            }
        }
    }
}
