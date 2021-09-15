using Core.Utilities.Results;
using Entities.Concrete;
using System.Collections.Generic;
using Entities.DTOs;

namespace Business.Abstract
{
    public interface IRentalService
    {
        IDataResult<List<Rental>> GetAll();
        IDataResult<Rental> GetById(int id);
        IDataResult<List<RentalDetailsForIndividualCustomers>> GetAllRentalsWithIndividualCustomerDetails();
        IDataResult<RentalDetailsForIndividualCustomers> GetRentalDetailsForIndividualCustomersByRentalId(int id);
        IResult Add(Rental rental);
        IResult Update(Rental rental);
        IResult Delete(Rental rental);
    }
}
