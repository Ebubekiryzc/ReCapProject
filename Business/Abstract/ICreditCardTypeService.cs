using Core.Utilities.Results;
using Entities.Concrete;
using System.Collections.Generic;

namespace Business.Abstract
{
    public interface ICreditCardTypeService
    {
        IDataResult<List<CreditCardType>> GetAll();
        IDataResult<CreditCardType> GetById(int id);
        IResult Add(CreditCardType creditCardType);
        IResult Update(CreditCardType creditCardType);
        IResult Delete(CreditCardType creditCardType);
    }
}
