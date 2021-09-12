using Core.Utilities.Results;
using Entities.Concrete;
using System.Collections.Generic;

namespace Business.Abstract
{
    public interface IIndividualCustomerService
    {
        IDataResult<List<IndividualCustomer>> GetAll();
        IDataResult<IndividualCustomer> GetById(int id);
        IResult Add(IndividualCustomer individualCustomer);
        IResult Update(IndividualCustomer individualCustomer);
        IResult Delete(IndividualCustomer individualCustomer);
    }
}
