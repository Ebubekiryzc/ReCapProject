using Core.Entities.Concrete;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(u => u.Email).NotEmpty();
            RuleFor(u => u.PasswordSalt).NotEmpty();
            RuleFor(u => u.PasswordHash).NotEmpty();
            RuleFor(u => u.Status).NotEmpty();
        }
    }
}
