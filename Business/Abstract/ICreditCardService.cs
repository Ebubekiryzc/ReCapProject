using Core.Utilities.Results;
using Entities.Concrete;
using Entities.DTOs;
using System.Collections.Generic;

namespace Business.Abstract
{
    public interface ICreditCardService
    {
        IDataResult<List<CreditCard>> GetAll();
        IDataResult<CreditCard> GetById(int id);
        IResult Add(CreditCardForUserOperationsDto creditCardForUserOperationsDto);
        IResult Update(CreditCardForUserOperationsDto creditCardForUserOperationsDto);
        IResult Delete(CreditCardForUserOperationsDto creditCardForUserOperationsDto);
    }
}
