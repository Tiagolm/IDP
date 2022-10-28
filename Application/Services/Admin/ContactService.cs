using Application.Exceptions;
using Application.Interfaces;
using Application.SearchParams;
using Application.ViewModels.Contact;
using AutoMapper;
using Domain.Core;
using Domain.Interfaces;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Admin
{
    public class ContactService : IContactService
    {
        private readonly IContactRepository _contactRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IRequestValidator<AdminContactRequest> _requestValidator;

        public ContactService(
            IContactRepository contatoRepository,
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IRequestValidator<AdminContactRequest> requestValidator)
        {
            _contactRepository = contatoRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _requestValidator = requestValidator;
        }
        public async Task<AdminContactResponse> GetContact(int id)
        {
            var contact = await _contactRepository
                .Query
                .AsNoTracking()
                .Include(x => x.User)
                .Include(x => x.Phones)
                .ThenInclude(x => x.PhoneContactType)
                .FirstOrDefaultAsync(x => x.Id == id);

            return _mapper.Map<AdminContactResponse>(contact);
        }
        public async Task<AdminContactResponse> AddContact(AdminContactRequest contactViewModel)
        {
            var validate = await _requestValidator.ValidateAsync(contactViewModel);

            if (!validate.IsValid)
                throw new BadRequestException(validate);

            var contact = _mapper.Map<Contact>(contactViewModel.Contact);
            contact.UserId = contactViewModel.UserId;

            await _contactRepository.Add(contact);
            await _unitOfWork.Save();

            return _mapper.Map<AdminContactResponse>(contact);
        }

        public async Task<AdminContactResponse> UpdateContact(int id, AdminContactRequest contactViewModel)
        {
            var contact = await _contactRepository
                .Query
                .Include(x => x.Phones)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (contact == null)
                throw new BadRequestException(nameof(id), "Inválido.");

            _contactRepository
                .AddPreQuery(x => x.Where(x => x.UserId == contactViewModel.UserId && x.Id != id));
            var validate = await _requestValidator.ValidateAsync(contactViewModel);

            if (!validate.IsValid)
                throw new BadRequestException(validate);

            _mapper.Map(contactViewModel.Contact, contact);

            await _contactRepository.Update(contact);
            await _unitOfWork.Save();
            return _mapper.Map<AdminContactResponse>(contact);
        }

        public async Task DeleteContact(int id)
        {
            var contact = _contactRepository.Query.FirstOrDefault(x => x.Id == id);

            if (contact == null)
                throw new BadRequestException(nameof(id), "Inválido");

            await _contactRepository.Delete(contact.Id);
            await _unitOfWork.Save();
        }

        public async Task<IEnumerable<AdminContactResponse>> SearchContacts(AdminContactQueryParam viewModel)
        {
            var list = await _contactRepository
                .Query
                .AsNoTracking()
                .Include(x => x.Phones)
                .ToListAsync();

            return _mapper.Map<IEnumerable<AdminContactResponse>>(list);
        }

    }
}
