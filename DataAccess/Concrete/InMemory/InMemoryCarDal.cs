using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;

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

        public List<Car> GetAll(Expression<Func<Car, bool>> filter = null)
        {
            return _cars;
        }

        public Car Get(Expression<Func<Car, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public void Add(Car entity)
        {
            throw new NotImplementedException();
        }

        public void Update(Car entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Car entity)
        {
            throw new NotImplementedException();
        }

        public List<CarDetailDto> GetCarsWithDetail()
        {
            throw new NotImplementedException();
        }
    }
}
