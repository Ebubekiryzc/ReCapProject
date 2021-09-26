using Core.Entities.Abstract;

namespace Entities.Concrete
{
    public class CreditCardType : IEntity
    {
        public int Id { get; set; }
        public string TypeName { get; set; }
    }
}
