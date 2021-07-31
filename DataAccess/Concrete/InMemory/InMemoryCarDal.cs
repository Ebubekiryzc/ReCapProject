using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Abstract;
using Entities.Concrete;

namespace DataAccess.Concrete.InMemory
{
    public class InMemoryCarDal : ICarDal
    {
        private List<Car> _cars;

        public InMemoryCarDal()
        {
            _cars = new List<Car>
            {
                new Car
                {
                    Id = 1, BrandId = 1, ColorId = 1, DailyPrice = 150, Description = "Ford Focus Diesel",
                    ModelYear = 2019
                },
                new Car
                {
                    Id = 2, BrandId = 2, ColorId = 1, DailyPrice = 90, Description = "Fiat Linea Diesel",
                    ModelYear = 2013
                },
                new Car
                {
                    Id = 3, BrandId = 3, ColorId = 3, DailyPrice = 250, Description = "Volkswagen Golf Gasoline",
                    ModelYear = 2020
                },
                new Car
                {
                    Id = 4, BrandId = 4, ColorId = 2, DailyPrice = 95, Description = "Reanult Symbol Diesel",
                    ModelYear = 2012
                },
            };
        }
        public Car GetById(int id)
        {
            return _cars.SingleOrDefault(c => c.Id == id);
        }

        public List<Car> GetAll()
        {
            return _cars;
        }

        public void Add(Car car)
        {
            _cars.Add(car);
        }

        public void Update(Car car)
        {
            var carToUpdate = _cars.SingleOrDefault(c => c.Id == car.Id);
            carToUpdate.BrandId = car.BrandId;
            carToUpdate.ColorId = car.ColorId;
            carToUpdate.DailyPrice = car.DailyPrice;
            carToUpdate.Description = car.Description;
            carToUpdate.ModelYear = car.ModelYear;
        }

        public void Delete(Car car)
        {
            var carToDelete = _cars.SingleOrDefault(c => c.Id == car.Id);
            _cars.Remove(carToDelete);
        }
    }
}
