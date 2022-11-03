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
        public PhoneContactRequestValidator(IContactRepository contactRepository)
        {
            RuleFor(x => x.Description)
                .MaximumLength(200);

            RuleFor(x => x.PhoneContactTypeId)
                .Must(phoneContactTypeId => Enumeration.GetAll<PhoneContactType>().Any(x => x.Id == phoneContactTypeId))
                .WithMessage("Não existe tipo de telefone informado.");

            RuleFor(x => x.FormattedPhone)
                .Must(tel => tel.ValidatePhone())
                .WithMessage("{PropertyName}: {PropertyValue} - Telefone inválido: (xx) xxxxx-xxxx");

            RuleFor(x => x.FormattedPhone)
                .Must(tel => tel.ValidateHomePhone())
                .WithMessage("{PropertyName}: {PropertyValue} - Telefone residencial inválido: (xx) xxxxx-xxxx")
                .When(vm => vm.PhoneContactTypeId == PhoneContactType.Casa.Id);

            RuleFor(x => x.FormattedPhone)
                .Must(tel => tel.ValidateCellPhone())
                .WithMessage("{PropertyName}: {PropertyValue} - Telefone celular inválido: (xx) xxxxx-xxxx")
                .When(vm => vm.PhoneContactTypeId == PhoneContactType.Celular.Id);

            RuleFor(x => x.FormattedPhone)
                .MustAsync(contactRepository.PhoneExists)
                .WithMessage("Telefone já existe {PropertyName}: {PropertyValue}");
        }
    }
}