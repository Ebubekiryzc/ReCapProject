using Core.DataAccess.InMemory;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess.Concrete.InMemory
{
    public class InMemoryRentalDal : InMemoryRepositoryBase<Rental>, IRentalDal
    {
        private List<Brand> _brands;
        private List<Car> _cars;
        private List<IndividualCustomer> _individualCustomers;

        public InMemoryRentalDal(List<Brand> brands, List<Car> cars, List<IndividualCustomer> individualCustomers)
        {
            _brands = brands;
            _cars = cars;
            _individualCustomers = individualCustomers;
        }

        public List<RentalDetailsForIndividualCustomers> GetAllRentalsWithIndividualCustomerDetails()
        {
            var result = from rental in GetAll()
                         join car in _cars on rental.CarId equals car.Id
                         join brand in _brands on car.BrandId equals brand.Id
                         join individualCustomer in _individualCustomers on rental.CustomerId equals
                             individualCustomer.Id
                         select new RentalDetailsForIndividualCustomers { Id = rental.Id, BrandName = brand.Name, CustomerName = $"{individualCustomer.FirstName}  {individualCustomer.LastName}", RentDate = rental.RentDate, ReturnDate = rental.ReturnDate };
            return result.ToList();
        }

        public RentalDetailsForIndividualCustomers GetRentalDetailsForIndividualCustomersByRentalId(int id)
        {
            var result = from rental in GetAll()
                         join car in _cars on rental.CarId equals car.Id
                         join brand in _brands on car.BrandId equals brand.Id
                         join individualCustomer in _individualCustomers on rental.CustomerId equals
                             individualCustomer.Id
                         where rental.Id == id
                         select new RentalDetailsForIndividualCustomers
                         {
                             Id = rental.Id,
                             BrandName = brand.Name,
                             CustomerName = $"{individualCustomer.FirstName}  {individualCustomer.LastName}",
                             RentDate = rental.RentDate,
                             ReturnDate = rental.ReturnDate
                         };
            return result.SingleOrDefault();
        }
    }
}
