using Application.Interfaces;
using Application.ViewModels.Login;
using Infrastructure.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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


            if (!PasswordHasher.Verify(model.Senha, user.Senha))
                throw new BadRequestException(nameof(model.Senha), "Senha inválida.");

            // authentication successful
            var token = _jwtGenerator.GenerateToken(user);
            return new LoginResponse(user, token);
        }
    }
}
