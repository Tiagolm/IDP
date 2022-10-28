using Application.Exceptions;
using Application.Interfaces;
using Application.SearchParams;
using Application.ViewModels.User;
using Application.ViewModels.UserRole;
using AutoMapper;
using Domain.Core;
using Domain.Interfaces;
using Domain.Models.Auth;
using Infrastructure.Utils;
using Microsoft.EntityFrameworkCore;

namespace Application.Services.Admin
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        private readonly IRequestValidator<UserRequest> _requestValidator;

        public UserService(IMapper mapper, IUserRepository userRepository, IUnitOfWork unitOfWork, IRequestValidator<UserRequest> requestValidator)
        {
            _mapper = mapper;
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
            _requestValidator = requestValidator;
        }

        public async Task<IEnumerable<UserResponse>> Search(UserQueryParam queryParam)
        {
            var list = await _userRepository.Query
                .AsNoTracking()
                .Include(x => x.UserRole)
                .ToListAsync();

            return _mapper.Map<IEnumerable<UserResponse>>(list);
        }

        public async Task<UserResponse> GetById(int id)
        {
            var user = await _userRepository.Query
                .AsNoTracking()
                .Include(x => x.UserRole)
                .FirstOrDefaultAsync(x => x.Id == id);

            return _mapper.Map<UserResponse>(user);
        }

        public async Task<UserResponse> Add(UserRequest model)
        {
            var validate = await _requestValidator.ValidateAsync(model);

            if (!validate.IsValid)
                throw new BadRequestException(validate);

            var user = _mapper.Map<User>(model);
            user.Password = PasswordHasher.Hash(model.Password);

            await _userRepository.Add(user);
            await _unitOfWork.Save();
            return await GetById(user.Id);
        }

        public async Task<UserResponse> Update(int id, UserRequest model)
        {
            var exists = await _userRepository
                .Query
                .FirstOrDefaultAsync(x => x.Id == id);

            if (exists == null)
                throw new BadRequestException(nameof(id), "Inválido");

            _userRepository.AddPreQuery(x => x.Where(u => u.Id != id));
            var validate = await _requestValidator.ValidateAsync(model);

            if (!validate.IsValid)
                throw new BadRequestException(validate);

            _mapper.Map(model, exists);
            exists.Password = PasswordHasher.Hash(model.Password);

            await _userRepository.Update(exists);
            await _unitOfWork.Save();
            return _mapper.Map<UserResponse>(exists);
        }

        public async Task Delete(int id)
        {
            var user = await _userRepository.Query
                .AsNoTracking()
                .Include(x => x.UserRole)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (user == null)
                throw new BadRequestException(nameof(id), "Inválido");

            await _userRepository.Delete(user.Id);
            await _unitOfWork.Save();
        }

        public IEnumerable<UserRoleResponse> UserRoles()
        {
            return _mapper.Map<IEnumerable<UserRoleResponse>>(Enumeration.GetAll<UserRole>());
        }
    }
}