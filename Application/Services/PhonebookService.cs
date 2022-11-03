using Application.Exceptions;
using Application.Interfaces;
using Application.SearchParams;
using Application.ViewModels.Contact;
using Application.ViewModels.PhoneContact;
using Application.ViewModels.PhoneContactType;
using AutoMapper;
using Domain.Core;
using Domain.Interfaces;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Application.Services
{
    public class PhonebookService : IPhonebookService
    {
        private readonly IContactRepository _contactRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IAuthService _authService;
        private readonly IRequestValidator<ContactRequest> _requestValidator;

        public PhonebookService(
            IContactRepository contactRepository,
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IAuthService authService,
            IRequestValidator<ContactRequest> requestValidator)
        {
            _contactRepository = contactRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _authService = authService;
            _requestValidator = requestValidator;

            _contactRepository.AddPreQuery(x => x.Where(c => c.UserId == _authService.LoggedUser.Id));
        }

        public async Task<ContactResponse> GetContact(int id)
        {
            var contact = await _contactRepository
                .Query
                .AsNoTracking()
                .Include(x => x.Phones)
                .ThenInclude(x => x.PhoneContactType)
                .FirstOrDefaultAsync(x => x.Id == id);

            await _unitOfWork.Save();

            return _mapper.Map<ContactResponse>(contact);
        }

        public async Task<ContactResponse> AddContact(ContactRequest contactViewModel)
        {
            var validate = await _requestValidator.ValidateAsync(contactViewModel);

            if (!validate.IsValid)
                throw new BadRequestException(validate);

            var contact = _mapper.Map<Contact>(contactViewModel);

            contact.UserId = _authService.LoggedUser.Id;

            await _contactRepository.Add(contact);
            await _unitOfWork.Save();

            return await GetContact(contact.Id);
        }

        public async Task<ContactResponse> UpdateContact(int id, ContactRequest contactViewModel)
        {
            var contact = await _contactRepository
                .Query
                .Include(x => x.Phones)
                .ThenInclude(x => x.PhoneContactType)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (contact == null)
                throw new BadRequestException(nameof(id), "Inválido.");

            if (contact.UserId != _authService.LoggedUser.Id)
                throw new NotAuthorizedException();

            _contactRepository.AddPreQuery(x => x.Where(x => x.Id != id));
            var validate = await _requestValidator.ValidateAsync(contactViewModel);

            if (!validate.IsValid)
                throw new BadRequestException(validate);

            _mapper.Map(contactViewModel, contact);

            await _contactRepository.Update(contact);
            await _unitOfWork.Save();

            return _mapper.Map<ContactResponse>(contact);
        }

        public async Task RemoveContact(int id)
        {
            var contact = _contactRepository.Query.FirstOrDefault(x => x.Id == id);

            if (contact == null)
                throw new BadRequestException(nameof(id), "Inválido");

            if (contact.UserId != _authService.LoggedUser.Id)
                throw new NotAuthorizedException();

            await _contactRepository.Delete(contact.Id);
            await _unitOfWork.Save();
        }

        public IEnumerable<PhoneContactTypeResponse> PhoneContactTypes()
        {
            return _mapper.Map<IEnumerable<PhoneContactTypeResponse>>(Enumeration.GetAll<PhoneContactType>());
        }

        public async Task<PaginationResult<ContactResponse>> SaerchContacts(ContactQueryParam viewModel)
        {
            var list = await _contactRepository.PaginateAndFilterAsync(viewModel);
            await _unitOfWork.Save();

            return _mapper.Map<PaginationResult<ContactResponse>>(list);
        }

        public async Task<IEnumerable<PhoneContactResponse>> SearchPhones(ContactQueryParam viewModel)
        {
            var list = await _contactRepository.Search(viewModel.ContactId, viewModel.ContactName, viewModel.Ddd, viewModel.Number);            
            await _unitOfWork.Save();

            return _mapper.Map<IEnumerable<PhoneContactResponse>>(list.SelectMany(x => x.Phones));
        }

    }
}