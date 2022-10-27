using Application.Interfaces;
using Application.ViewModels.Login;
using FluentValidation;

namespace Application.Validators
{
    public class LoginRequestValidator :
        AbstractValidator<LoginRequest>,
        IRequestValidator<LoginRequest>
    {
        public LoginRequestValidator()
        {
            RuleFor(x => x.Username)
                .NotEmpty();

            RuleFor(x => x.Password)
                .NotEmpty();
        }
    }
}