using Core.Utilities.Results;
using Entities.Concrete;
using Entities.DTOs;

namespace Business.Abstract
{
    public interface IPaymentService
    {
        IResult GetPayment(CreditCardForUserOperationsDto creditCardForUserOperationsDto, Rental rental);
    }
}
