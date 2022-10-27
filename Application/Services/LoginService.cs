using Application.Exceptions;
using Application.Interfaces;
using Application.ViewModels.Login;
using Domain.Interfaces;
using Infrastructure.Auth;
using Infrastructure.Utils;
using Microsoft.EntityFrameworkCore;

namespace Application.Services
{
    public class LoginService : ILoginService
    {
        private readonly IUserRepository _userRepository;
        private readonly IRequestValidator<LoginRequest> _requestValidator;
        private readonly IJwtGeneratorService _jwtGenerator;

        public LoginService(
            IUserRepository userRepository,
            IRequestValidator<LoginRequest> requestValidator,
            IJwtGeneratorService jwtGenerator)
        {
            _userRepository = userRepository;
            _requestValidator = requestValidator;
            _jwtGenerator = jwtGenerator;
        }

        public async Task<LoginResponse> Authenticate(LoginRequest model)
        {
            var validate = await _requestValidator.ValidateAsync(model);

            if (!validate.IsValid)
                throw new BadRequestException(validate);

            var user = await _userRepository
                .Query
                .AsNoTracking()
                .Include(x => x.UserRole)
                .FirstOrDefaultAsync(x => x.Username == model.Username);

            // validate
            if (user == null)
                throw new BadRequestException(nameof(model.Username), "Não existe usuário com Username informado.");

            if (!PasswordHasher.Verify(model.Password, user.Password))
                throw new BadRequestException(nameof(model.Password), "Senha inválida.");

            // authentication successful
            var token = _jwtGenerator.GenerateToken(user);
            return new LoginResponse(user, token);
        }
    }
}