using Application.Interfaces;
using Application.ViewModels.User;
using Domain.Core;
using Domain.Interfaces;
using Domain.Models.Auth;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Application.Validators
{
    public class UserRequestValidator :
        AbstractValidator<UserRequest>,
        IRequestValidator<UserRequest>

    {
        public UserRequestValidator(IUserRepository userRepository)
        {
            RuleFor(x => x.Username)
                .NotEmpty()
                .MinimumLength(3)
                .MaximumLength(200);

            RuleFor(x => x.Name)
                .NotEmpty()
                .MinimumLength(3)
                .MaximumLength(200);

            RuleFor(x => x.Password)
                .NotEmpty()
                .MinimumLength(3)
                .MaximumLength(100);

            RuleFor(x => x.Username)
                .MustAsync((username, cancellationToken) => userRepository.Query.AsNoTracking().AllAsync(x => x.Username != username, cancellationToken))
                .WithMessage("{PropertyName} Já existe um usuário com username informado.");

            RuleFor(x => x.UserRoleId)
                .Must(userRoleId => Enumeration.GetAll<UserRole>().Any(x => x.Id == userRoleId))
                .WithMessage("{Propertyname} Não existe UserRoleId com o valor informado.");
        }
    }
}