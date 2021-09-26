using Core.Entities.Abstract;

namespace Entities.Concrete
{
    public class CreditCard : IEntity
    {
        public int Id { get; set; }
        public int CreditCardTypeId { get; set; }
        public byte[] CardNumberSalt { get; set; }
        public byte[] CardNumberHash { get; set; }
        public byte[] ExpirationDateSalt { get; set; }
        public byte[] ExpirationDateHash { get; set; }
        public byte[] CvcSalt { get; set; }
        public byte[] CvcHash { get; set; }
    }
}
