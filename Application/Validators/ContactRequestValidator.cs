using Application.Interfaces;
using Application.ViewModels.Contact;
using Domain.Interfaces;
using FluentValidation;

namespace Application.Validators
{
    public class ContactRequestValidator : AbstractValidator<ContactRequest>, IRequestValidator<ContactRequest>
    {
        public ContactRequestValidator(IContactRepository contatoRepository)
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(200);

            RuleForEach(x => x.Phones)
                .SetValidator(new PhoneContactRequestValidator(contatoRepository));

            RuleFor(x => x.Phones)
                .Must(phones => phones.GroupBy(x => x.FormattedPhone).All(group => group.Count() == 1))
                .WithMessage("Existem telefones duplicados na lista.");
        }
    }
}