using Entities.Concrete;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation
{
    public class IndividualCustomerValidator : AbstractValidator<IndividualCustomer>
    {
        public IndividualCustomerValidator()
        {
            RuleFor(c => c.CompanyName).NotEmpty();
        }
    }
}
