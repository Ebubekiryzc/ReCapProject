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
    public class ColorOperations : IDisposable
    {
        private IColorDal _colorDal;
        private IColorService _colorManager;

        public ColorOperations(out IColorDal colorDal)
        {
            Console.Write("Which dal do you want to use? (1- EfColorDal, 2- InMemoryColorDal): ");
            int option = Convert.ToInt32(Console.ReadLine());

            switch (option)
            {
                case 1:
                    {
                        colorDal = new EfColorDal();
                        break;
                    }
                case 2:
                    {
                        colorDal = new InMemoryColorDal();
                        break;
                    }
                default:
                    {
                        colorDal = new EfColorDal();
                        break;
                    }
            }

            _colorDal = colorDal;
            _colorManager = new ColorManager(_colorDal);
        }

        public void GetAll()
        {
            var result = _colorManager.GetAll();
            Console.WriteLine(result.Message);
            foreach (var color in result.Data)
            {
                Console.WriteLine("Id: {0}, Name: {1}", color.Id, color.Name);
            }
        }

        public void GetById()
        {
            Console.Write("Please enter the id of color you want to get: ");
            int id = Convert.ToInt32(Console.ReadLine());
            var result = _colorManager.GetById(id);
            Console.WriteLine("{0}\nId: {1}, Name: {2}", result.Message, result.Data.Id, result.Data.Name);
        }

        public void AddToDatabase(Color color)
        {
            Console.WriteLine($"{_colorManager.Add(CreateColor(color)).Message}\n");
        }

        public void UpdateToDatabase(Color color)
        {
            Console.WriteLine($"{_colorManager.Update(UpdateColor(color)).Message}\n");
        }

        public void DeleteFromDatabase(Color color)
        {
            Console.WriteLine($"{_colorManager.Delete(DeleteBrand(color)).Message}\n");
        }

        private Color UpdateColor(Color color)
        {
            var result = CheckBrandExists(color);
            if (result)
            {
                Console.WriteLine("\nFor new entity: ");
                Console.Write("Please enter the new color name for update: ");
                color.Name = Console.ReadLine();
            }
            return color;
        }
        private Color DeleteBrand(Color color)
        {
            var result = CheckBrandExists(color);
            if (!result)
            {
                color.Id = -1;
            }

            return color;
        }

        private bool CheckBrandExists(Color color)
        {
            EnterIdentityInfo(color);
            EnterBodyInfo(color);
            bool checkBrandExists = _colorManager.GetAll().Data.Any(c => c.Id == color.Id && c.Name == color.Name);
            if (!checkBrandExists)
            {
                Console.WriteLine("There is no color matcing that instance.");
                color.Id = -1;
            }

            return checkBrandExists;
        }

        private void EnterBodyInfo(Color color)
        {
            Console.Write("Please enter the name for your color: ");
            color.Name = Console.ReadLine();
        }

        private void EnterIdentityInfo(Color color)
        {
            Console.Write("Please enter the id of the color you want to affect: ");
            int id = Convert.ToInt32(Console.ReadLine());
            color.Id = id;
        }

        private Color CreateColor(Color color)
        {
            color = new Color();
            EnterBodyInfo(color);
            return color;
        }

        public void Dispose()
        {
            _colorManager = null;
            _colorDal = null;
        }
    }
}
