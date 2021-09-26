using Core.Entities.Abstract;
using Entities.Concrete;

namespace Entities.DTOs
{
    public class PaymentQueryDto : IDto
    {
        public UserCreditCard UserCreditCard { get; set; }
        public Rental Rental { get; set; }
    }
}
