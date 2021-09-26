using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results;
using Core.Utilities.Security.Hashing;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System.Collections.Generic;

namespace Business.Concrete
{
    public class CreditCardManager : ICreditCardService
    {
        private ICreditCardDal _creditCardDal;

        public CreditCardManager(ICreditCardDal creditCardDal)
        {
            _creditCardDal = creditCardDal;
        }

        public IDataResult<List<CreditCard>> GetAll()
        {
            return new SuccessDataResult<List<CreditCard>>(_creditCardDal.GetAll(), Messages.CreditCardsListed);
        }

        public IDataResult<CreditCard> GetById(int id)
        {
            return new SuccessDataResult<CreditCard>(_creditCardDal.Get(c => c.Id == id), Messages.CreditCardListed);
        }

        public IResult Add(CreditCardForUserOperationsDto creditCardForUserOperationsDto)
        {
            var creditCard = CreateCreditCardFromDto(creditCardForUserOperationsDto);
            _creditCardDal.Add(creditCard);
            return new SuccessResult(Messages.OperationSuccessful);
        }

        public IResult Update(CreditCardForUserOperationsDto creditCardForUserOperationsDto)
        {
            var creditCard = CreateCreditCardFromDto(creditCardForUserOperationsDto);
            _creditCardDal.Update(creditCard);
            return new SuccessResult(Messages.OperationSuccessful);
        }

        public IResult Delete(CreditCardForUserOperationsDto creditCardForUserOperationsDto)
        {
            var creditCardToCheck = GetById(creditCardForUserOperationsDto.Id).Data;
            if (creditCardToCheck is null)
            {
                return new ErrorResult(Messages.CreditCardNotFound);
            }

            if (!HashingHelper.VerifyHash(creditCardForUserOperationsDto.CreditCardNumber, creditCardToCheck.CardNumberHash,
                creditCardToCheck.CardNumberSalt))
            {
                return new ErrorResult(Messages.CreditCardNumberNotMatching);
            }

            if (!HashingHelper.VerifyHash(creditCardForUserOperationsDto.Cvc, creditCardToCheck.CvcHash,
                creditCardToCheck.CvcSalt))
            {
                return new ErrorResult(Messages.CvcNotMatching);
            }

            if (!HashingHelper.VerifyHash(creditCardForUserOperationsDto.ExpirationDate.ToString(),
                creditCardToCheck.ExpirationDateHash, creditCardToCheck.ExpirationDateSalt))
            {
                return new ErrorResult(Messages.ExpirationDateNotMatching);
            }


            _creditCardDal.Delete(creditCardToCheck);
            return new SuccessResult(Messages.OperationSuccessful);
        }

        private CreditCard CreateCreditCardFromDto(CreditCardForUserOperationsDto creditCardForUserOperationsDto)
        {
            byte[] creditCardNumberHash, creditCardNumberSalt, cvcHash, cvcSalt, expirationDateHash, expirationDateSalt;
            HashingHelper.CreateHash(creditCardForUserOperationsDto.CreditCardNumber, out creditCardNumberHash,
                out creditCardNumberSalt);
            HashingHelper.CreateHash(creditCardForUserOperationsDto.Cvc, out cvcHash, out cvcSalt);
            HashingHelper.CreateHash(creditCardForUserOperationsDto.ExpirationDate.ToString(), out expirationDateHash,
                out expirationDateSalt);
            var creditCard = new CreditCard
            {
                CardNumberHash = creditCardNumberHash,
                CardNumberSalt = creditCardNumberSalt,
                CreditCardTypeId = creditCardForUserOperationsDto.CreditCardTypeId,
                CvcHash = cvcHash,
                CvcSalt = cvcSalt,
                ExpirationDateHash = expirationDateHash,
                ExpirationDateSalt = expirationDateSalt
            };
            return creditCard;
        }
    }
}
