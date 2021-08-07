using System;
using Business.Abstract;
using Business.Concrete;
using DataAccess.Concrete.EntityFramework;
using DataAccess.Concrete.InMemory;
using Entities.Concrete;

namespace ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("---------------------- Brand Operations ----------------------");

            IBrandService brandService = new BrandManager(new EfBrandDal());
            Brand brand = new Brand { Name = "Ford" };
            AddBrand(brandService, brand);
            GetAllBrands(brandService);
            GetBrandById(brandService, 1);
            brand = new Brand { Id = 1, Name = "Toyota" };
            UpdateBrand(brandService, brand);
            DeleteBrand(brandService, brand);
            Console.WriteLine("Addition again:");
            brand = new Brand { Name = "Mercedes" };
            AddBrand(brandService, brand);

            Console.WriteLine("---------------------- Color Operations ----------------------");

            IColorService colorService = new ColorManager(new EfColorDal());
            Color color = new Color { Name = "Yellow" };
            AddColor(colorService, color);
            GetAllColors(colorService);
            GetColorById(colorService, 1);
            color = new Color { Id = 1, Name = "Green" };
            UpdateColor(colorService, color);
            DeleteColor(colorService, color);
            Console.WriteLine("Addition again:");
            color = new Color { Name = "Red" };
            AddColor(colorService, color);

            Console.WriteLine("---------------------- Car Operations ----------------------");

            ICarService carService = new CarManager(new EfCarDal());
            Car car = new Car { BrandId = 2, ColorId = 2, Description = "Kuga Diesel", DailyPrice = 180, ModelYear = 1999 };
            AddCar(carService, car);
            GetAllCars(carService);
            GetCarById(carService, 1);
            GetAllCarsWithDetails(carService);
            car = new Car { Id = 1, BrandId = 2, ColorId = 2, Description = "Kuga Gasoline", DailyPrice = 180, ModelYear = 1999 };
            UpdateCar(carService, car);
            DeleteCar(carService, car);
        }

        private static void GetAllCarsWithDetails(ICarService carService)
        {
            if (carService.GetCarsWithDetail().Success)
            {
                foreach (var carDetailDto in carService.GetCarsWithDetail().Data)
                {
                    Console.WriteLine("{0} {1} {2} {3}", carDetailDto.CarName, carDetailDto.BrandName,
                        carDetailDto.ColorName,
                        carDetailDto.DailyPrice);
                }
            }
        }

        private static void DeleteColor(IColorService colorService, Color color)
        {
            Console.WriteLine("\n---Delete---");
            colorService.Delete(color);
            Console.Write("\nAfter Delete ");
            GetAllColors(colorService);
        }

        private static void UpdateColor(IColorService colorService, Color color)
        {
            Console.WriteLine("\n---Update---");
            colorService.Update(color);
            Console.Write("\nAfter Update ");
            GetAllColors(colorService);
        }

        private static void AddColor(IColorService colorService, Color color)
        {
            Console.WriteLine("\n---Add---");
            colorService.Add(color);
            Console.Write("\nAfter Addition ");
            GetAllColors(colorService);
        }

        private static void GetColorById(IColorService colorService, int id)
        {
            Console.WriteLine("\nGet by Id:");
            Console.WriteLine(colorService.GetById(id).Data.Name);
        }

        private static void GetAllColors(IColorService colorService)
        {
            if (colorService.GetAll().Success)
            {
                Console.WriteLine("All Colors:");
                foreach (var color in colorService.GetAll().Data)
                {
                    Console.WriteLine(color.Name);
                }
            }
        }

        private static void DeleteBrand(IBrandService brandService, Brand brand)
        {
            Console.WriteLine("\n---Delete---");
            brandService.Delete(brand);
            Console.Write("\nAfter Delete ");
            GetAllBrands(brandService);
        }
        private static void UpdateBrand(IBrandService brandService, Brand brand)
        {
            Console.WriteLine("\n---Update---");
            brandService.Update(brand);
            Console.Write("\nAfter Update ");
            GetAllBrands(brandService);
        }

        private static void AddBrand(IBrandService brandService, Brand brand)
        {
            Console.WriteLine("\n---Add---");
            brandService.Add(brand);
            Console.Write("\nAfter Addition ");
            GetAllBrands(brandService);
        }

        private static void GetBrandById(IBrandService brandService, int id)
        {
            if (brandService.GetById(id).Success)
            {
                Console.WriteLine("\nGet by Id:");
                Console.WriteLine(brandService.GetById(id).Data.Name);
            }
        }

        private static void GetAllBrands(IBrandService brandService)
        {
            if (brandService.GetAll().Success)
            {
                Console.WriteLine("All Brands:");
                foreach (var brand in brandService.GetAll().Data)
                {
                    Console.WriteLine(brand.Name);
                }
            }
        }

        private static void DeleteCar(ICarService carService, Car car)
        {
            Console.WriteLine("\n---Delete---");
            carService.Delete(car);
            Console.Write("\nAfter Delete ");
            GetAllCars(carService);
        }

        private static void UpdateCar(ICarService carService, Car car)
        {
            Console.WriteLine("\n---Update---");
            carService.Update(car);
            Console.Write("\nAfter Update");
            GetAllCars(carService);
        }

        private static void AddCar(ICarService carService, Car car)
        {
            Console.WriteLine("\n---Add---");
            carService.Add(car);
            Console.Write("\nAfter Addition ");
            GetAllCars(carService);
        }

        private static void GetCarById(ICarService carService, int id)
        {
            if (carService.GetById(id).Success)
            {
                Console.WriteLine("\nGet by Id:");
                Console.WriteLine(carService.GetById(id).Data.Description);
            }
        }

        private static void GetAllCars(ICarService carService)
        {
            if (carService.GetAll().Success)
            {
                Console.WriteLine("All Cars:");
                foreach (var car in carService.GetAll().Data)
                {
                    Console.WriteLine(car.Description);
                }
            }
        }
    }
}