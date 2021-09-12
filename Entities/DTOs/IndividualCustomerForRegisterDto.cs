using Core.Entities.Abstracts;

namespace Entities.DTOs
{
    public class IndividualCustomerForRegisterDto : UserForRegisterDto, IDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CompanyName { get; set; }
    }
}
