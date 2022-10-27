using Application.Interfaces;
using Application.ViewModels.Contact;
using Domain.Interfaces;
using FluentValidation;

namespace Application.Validators
{
    public class AdminContactRequestValidator : AbstractValidator<AdminContactRequest>, IRequestValidator<AdminContactRequest>
    {
        public AdminContactRequestValidator(IContactRepository repo)
        {
            RuleFor(x => x.UserId).NotEmpty().NotEqual(0);
            RuleFor(x => x.Contact).SetValidator(new ContactRequestValidator(repo));
        }
    }
}