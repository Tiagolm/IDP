using Application.Interfaces;
using Application.Services;
using Application.Validators;
using Application.ViewModels.Contact;
using Application.ViewModels.PhoneContact;
using Castle.Core.Resource;
using Domain.Core;
using Domain.Interfaces;
using Domain.Models;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tests.Fakers.Models;
using Tests.Fakers.Requests.ContactRequests;
using Xunit;

namespace Tests.Application.Services
{
    [Trait("TestType", "Unit")]
    public class PhonebookServiceTest : ServiceBaseTest
    {
        private Mock<IContactRepository> _contactRepository;
        private Mock<IAuthService> _authService;
        private IRequestValidator<ContactRequest> _contactValidator;
        private IRequestValidator<PhoneContactRequest> _phoneContactValidator;

        public PhonebookServiceTest()
        {
            InstantiateMocks();
        }

        public void InstantiateMocks()
        {
            _contactRepository = new Mock<IContactRepository>();
            _authService = new Mock<IAuthService>();
            _contactValidator = new ContactRequestValidator(_contactRepository.Object);
            _phoneContactValidator = new PhoneContactRequestValidator(_contactRepository.Object);
        }

        [Fact(DisplayName = "GetByIdAsync - Consulta por ID com sucesso")]
        public async Task GetByIdAsync_ContatoRegistrado_DeveRetornarMesmoContato()
        {
            //Arrange
            InstantiateMocks();

            var contact = ContactFaker.ContactGenerate();
            var phones = ContactFaker.PhonesGenerate();
            contact.Phones = phones;

            _contactRepository.Setup(x => x.GetById(It.IsAny<int>())).Returns(Task.FromResult(contact));
            
            var phonebookService = new PhonebookService(_contactRepository.Object, _unitOfWork.Object, _mapper, _authService.Object, _contactValidator);

            //Act
            var result = await phonebookService.GetContact(10);

            //Assert
            _contactRepository.Verify(r => r.GetById(It.IsAny<int>()), Times.Once);

            result.Should().NotBeNull();
            result.Name.Should().Be(contact.Name);
            result.Phones.Select(c => c.Description).Should().Equal(contact.Phones.Select(c => c.Description));
            result.Phones.Select(c => c.Ddd).Should().Equal(contact.Phones.Select(c => c.Ddd));
            result.Phones.Select(c => c.Phone).Should().Equal(contact.Phones.Select(c => c.Phone));
            result.Phones.Select(c => c.FormattedPhone).Should().Equal(contact.Phones.Select(c => c.FormattedPhone));
        }

        [Fact(DisplayName = "GetByIdAsync - Consulta customer inexistente por ID com sucesso")]
        public async Task GetByIdAsync_ContactNaoRegistrado_DeveRetornarNull()
        {
            //Arrange
            InstantiateMocks();

            var phonebookService = new PhonebookService(_contactRepository.Object, _unitOfWork.Object, _mapper, _authService.Object, _contactValidator);

            //Act
            var result = await phonebookService.GetContact(10);

            //Assert
            _contactRepository.Verify(r => r.GetById(It.IsAny<int>()), Times.Once);

            result.Should().BeNull();
        }

        [Fact(DisplayName = "Register - Cadastra com sucesso")]
        public async Task RegisterAsync_RegistraContact_DeveRetornarSucesso()
        {
            //Arrange
            InstantiateMocks();

            var customerRegister = ContactRegisterRequestFaker.ContactRegisterRequestGenerate();

            var phonebookService = new PhonebookService(_contactRepository.Object, _unitOfWork.Object, _mapper, _authService.Object, _contactValidator);

            //Act
            var result = await phonebookService.AddContact(customerRegister);

            //Assert
            _contactRepository.Verify(r => r.Add(It.IsAny<Contact>()), Times.Once);
            _unitOfWork.Verify(r => r.Save(), Times.Once);

            result.Should().NotBeNull();
        }
    }


}
