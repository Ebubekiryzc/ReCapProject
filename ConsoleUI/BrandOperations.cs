using Business.Abstract;
using Business.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using DataAccess.Concrete.InMemory;
using Entities.Concrete;
using System;
using System.Linq;

namespace ConsoleUI
{
    class BrandOperations : IDisposable
    {
        private IBrandDal _brandDal;
        private IBrandService _brandManager;

        public BrandOperations(out IBrandDal brandDal)
        {
            Console.Write("Which dal do you want to use? (1- EfBrandDal, 2- InMemoryBrandDal): ");
            int option = Convert.ToInt32(Console.ReadLine());

            switch (option)
            {
                case 1:
                    {
                        brandDal = new EfBrandDal();
                        break;
                    }
                case 2:
                    {
                        brandDal = new InMemoryBrandDal();
                        break;
                    }
                default:
                    {
                        brandDal = new EfBrandDal();
                        break;
                    }
            }

            _brandDal = brandDal;
            _brandManager = new BrandManager(_brandDal);
        }

        public void GetAll()
        {
            var result = _brandManager.GetAll();
            Console.WriteLine(result.Message);
            foreach (var brand in result.Data)
            {
                Console.WriteLine("Id: {0}, Name: {1}", brand.Id, brand.Name);
            }
        }

        public void GetById()
        {
            Console.Write("Please enter the id of brand you want to get: ");
            int id = Convert.ToInt32(Console.ReadLine());
            var result = _brandManager.GetById(id);
            Console.WriteLine("{0}\nId: {1}, Name: {2}", result.Message, result.Data.Id, result.Data.Name);
        }

        public void AddToDatabase(Brand brand)
        {
            Console.WriteLine($"{_brandManager.Add(CreateBrand(brand)).Message}\n");
        }

        public void UpdateToDatabase(Brand brand)
        {
            Console.WriteLine($"{_brandManager.Update(UpdateBrand(brand)).Message}\n");
        }

        public void DeleteFromDatabase(Brand brand)
        {
            Console.WriteLine($"{_brandManager.Delete(DeleteBrand(brand)).Message}\n");
        }

        private Brand UpdateBrand(Brand brand)
        {
            var result = CheckBrandExists(brand);
            if (result)
            {
                Console.WriteLine("\nFor new entity: ");
                Console.Write("Please enter the new brand name for update: ");
                brand.Name = Console.ReadLine();
            }
            return brand;
        }
        private Brand DeleteBrand(Brand brand)
        {
            var result = CheckBrandExists(brand);
            if (!result)
            {
                brand.Id = -1;
            }

            return brand;
        }

        private bool CheckBrandExists(Brand brand)
        {
            EnterIdentityInfo(brand);
            EnterBodyInfo(brand);
            bool checkBrandExists = _brandManager.GetAll().Data.Any(b => b.Id == brand.Id && b.Name == brand.Name);
            if (!checkBrandExists)
            {
                Console.WriteLine("There is no brand matcing that instance.");
                brand.Id = -1;
            }

            return checkBrandExists;
        }

        private void EnterBodyInfo(Brand brand)
        {
            Console.Write("Please enter the name for your brand: ");
            brand.Name = Console.ReadLine();
        }

        private void EnterIdentityInfo(Brand brand)
        {
            Console.Write("Please enter the id of the brand you want to affect: ");
            int id = Convert.ToInt32(Console.ReadLine());
            brand.Id = id;
        }

        private Brand CreateBrand(Brand brand)
        {
            brand = new Brand();
            EnterBodyInfo(brand);
            return brand;
        }

        public void Dispose()
        {
            _brandManager = null;
            _brandDal = null;
        }
    }
}