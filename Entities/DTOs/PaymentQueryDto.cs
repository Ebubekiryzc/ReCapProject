using Core.Entities.Abstract;
using Entities.Concrete;

namespace Entities.DTOs
{
    public class PaymentQueryDto : IDto
    {
        public CreditCardForUserOperationsDto CreditCardForUserOperationsDto { get; set; }
        public Rental Rental { get; set; }
    }
}
