using Core.Entities.Abstract;
using System;

namespace Entities.DTOs
{
    public class CreditCardForUserOperationsDto : IDto
    {
        public int Id { get; set; }
        public int CreditCardTypeId { get; set; }
        public string CreditCardNumber { get; set; }
        public string Cvc { get; set; }
        public DateTime ExpirationDate { get; set; }
    }
}
