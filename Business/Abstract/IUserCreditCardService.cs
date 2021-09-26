using Core.Utilities.Results;
using Entities.Concrete;
using System.Collections.Generic;

namespace Business.Abstract
{
    public interface IUserCreditCardService
    {
        IDataResult<List<UserCreditCard>> GetAll();
        IDataResult<UserCreditCard> GetById(int id);
        IResult Add(UserCreditCard userCreditCard);
        IResult Update(UserCreditCard userCreditCard);
        IResult Delete(UserCreditCard userCreditCard);
    }
}
