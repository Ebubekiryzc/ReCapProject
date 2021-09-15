using Core.DataAccess;
using Entities.Concrete;
using Entities.DTOs;
using System.Collections.Generic;

namespace DataAccess.Abstract
{
    public interface IRentalDal : IEntityRepository<Rental>
    {
        List<RentalDetailsForIndividualCustomers> GetAllRentalsWithIndividualCustomerDetails();
        RentalDetailsForIndividualCustomers GetRentalDetailsForIndividualCustomersByRentalId(int id);
    }
}
