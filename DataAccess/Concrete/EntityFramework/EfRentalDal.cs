using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfRentalDal : EfEntityRepositoryBase<Rental, ReCapProjectContext>, IRentalDal
    {
        public List<RentalDetailsForIndividualCustomers> GetAllRentalsWithIndividualCustomerDetails()
        {
            using (ReCapProjectContext context = new ReCapProjectContext())
            {
                var result = from rental in context.Rentals
                             join car in context.Cars on rental.CarId equals car.Id
                             join brand in context.Brands on car.BrandId equals brand.Id
                             join individualCustomer in context.IndividualCustomers on rental.CustomerId equals
                                 individualCustomer.Id
                             select new RentalDetailsForIndividualCustomers { Id = rental.Id, BrandName = brand.Name, CustomerName = $"{individualCustomer.FirstName + individualCustomer.LastName}", RentDate = rental.RentDate, ReturnDate = rental.ReturnDate };
                return result.ToList();
            }
        }

        public RentalDetailsForIndividualCustomers GetRentalDetailsForIndividualCustomersByRentalId(int id)
        {
            using (ReCapProjectContext context = new ReCapProjectContext())
            {
                var result = from rental in context.Rentals
                             join car in context.Cars on rental.CarId equals car.Id
                             join brand in context.Brands on car.BrandId equals brand.Id
                             join individualCustomer in context.IndividualCustomers on rental.CustomerId equals
                                 individualCustomer.Id
                             where rental.Id == id
                             select new RentalDetailsForIndividualCustomers
                             {
                                 Id = rental.Id,
                                 BrandName = brand.Name,
                                 CustomerName = $"{individualCustomer.FirstName + individualCustomer.LastName}",
                                 RentDate = rental.RentDate,
                                 ReturnDate = rental.ReturnDate
                             };
                return result.SingleOrDefault();
            }
        }
    }
}
