using Core.Entities.Abstracts;

namespace Entities.Concrete
{
    public class IndividualCustomer : IEntity
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CompanyName { get; set; }
    }
}
