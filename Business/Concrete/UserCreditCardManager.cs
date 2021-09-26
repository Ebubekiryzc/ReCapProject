using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System.Collections.Generic;

namespace Business.Concrete
{
    public class UserCreditCardManager : IUserCreditCardService
    {
        private IUserCreditCardDal _userCreditCardDal;
        public UserCreditCardManager(IUserCreditCardDal userCreditCardDal)
        {
            _userCreditCardDal = userCreditCardDal;
        }

        public IDataResult<List<UserCreditCard>> GetAll()
        {
            return new SuccessDataResult<List<UserCreditCard>>(_userCreditCardDal.GetAll(),
                Messages.UserCreditCardsListed);
        }

        public IDataResult<UserCreditCard> GetById(int id)
        {
            return new SuccessDataResult<UserCreditCard>(_userCreditCardDal.Get(ucc => ucc.Id == id), Messages.UserCreditCardListed);
        }

        public IResult Add(UserCreditCard userCreditCard)
        {
            _userCreditCardDal.Add(userCreditCard);
            return new SuccessResult(Messages.OperationSuccessful);
        }

        public IResult Update(UserCreditCard userCreditCard)
        {
            _userCreditCardDal.Update(userCreditCard);
            return new SuccessResult(Messages.OperationSuccessful);
        }

        public IResult Delete(UserCreditCard userCreditCard)
        {
            _userCreditCardDal.Delete(userCreditCard);
            return new SuccessResult(Messages.OperationSuccessful);
        }
    }
}
