using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System.Collections.Generic;

namespace Business.Concrete
{
    public class CreditCardTypeManager : ICreditCardTypeService
    {
        private ICreditCardTypeDal _creditCardTypeDal;

        public CreditCardTypeManager(ICreditCardTypeDal creditCardTypeDal)
        {
            _creditCardTypeDal = creditCardTypeDal;
        }

        public IDataResult<List<CreditCardType>> GetAll()
        {
            return new SuccessDataResult<List<CreditCardType>>(_creditCardTypeDal.GetAll(),
                Messages.CreditCardTypesListed);
        }

        public IDataResult<CreditCardType> GetById(int id)
        {
            return new SuccessDataResult<CreditCardType>(_creditCardTypeDal.Get(c => c.Id == id), Messages.CreditCardTypeListed);
        }

        public IResult Add(CreditCardType creditCardType)
        {
            _creditCardTypeDal.Add(creditCardType);
            return new SuccessResult(Messages.OperationSuccessful);
        }

        public IResult Update(CreditCardType creditCardType)
        {
            _creditCardTypeDal.Update(creditCardType);
            return new SuccessResult(Messages.OperationSuccessful);
        }

        public IResult Delete(CreditCardType creditCardType)
        {
            _creditCardTypeDal.Delete(creditCardType);
            return new SuccessResult(Messages.OperationSuccessful);
        }
    }
}
