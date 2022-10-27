using Application.Extensions;
using Application.Interfaces;
using Application.ViewModels.PhoneContact;
using Domain.Core;
using Domain.Interfaces;
using Domain.Models;
using FluentValidation;

namespace Application.Validators
{
    public class PhoneContactRequestValidator :
        AbstractValidator<PhoneContactRequest>,
        IRequestValidator<PhoneContactRequest>
    {
        public PhoneContactRequestValidator(IContactRepository contatoRepository)
        {
            RuleFor(x => x.Description)
                .MaximumLength(200);

            RuleFor(x => x.PhoneContactTypeIdId)
                .Must(phoneContactTypeIdId => Enumeration.GetAll<PhoneContactType>().Any(x => x.Id == phoneContactTypeIdId))
                .WithMessage("Não existe tipo de telefone informado.");

            RuleFor(x => x.FormattedPhone)
                .Must(tel => tel.ValidatePhone())
                .WithMessage("{PropertyName}: {PropertyValue} - Telefone inválido: (xx) xxxxx-xxxx");

            RuleFor(x => x.FormattedPhone)
                .Must(tel => tel.ValidateHomePhone())
                .WithMessage("{PropertyName}: {PropertyValue} - Telefone residencial inválido: (xx) xxxxx-xxxx")
                .When(vm => vm.PhoneContactTypeIdId == PhoneContactType.Casa.Id);

            RuleFor(x => x.FormattedPhone)
                .Must(tel => tel.ValidateCellPhone())
                .WithMessage("{PropertyName}: {PropertyValue} - Telefone celular inválido: (xx) xxxxx-xxxx")
                .When(vm => vm.PhoneContactTypeIdId == PhoneContactType.Celular.Id);

            RuleFor(x => x.FormattedPhone)
                .MustAsync(contatoRepository.PhoneExists)
                .WithMessage("Telefone já existe {PropertyName}: {PropertyValue}");
        }
    }
}