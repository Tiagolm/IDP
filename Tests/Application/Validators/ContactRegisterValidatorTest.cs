using Application.Validators;
using Domain.Interfaces;
using Domain.Models;
using FluentValidation.TestHelper;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tests.Fakers.Requests.ContactRequests;
using Xunit;

namespace Tests.Application.Validators
{
    public class ContactRegisterValidatorTest
    {
        private Mock<IContactRepository> _contactRepository;

        public ContactRegisterValidatorTest()
        {
            InstantiateMocks();
        }

        public void InstantiateMocks()
        {
            _contactRepository = new Mock<IContactRepository>();
        }

        [Fact(DisplayName = "Validate - Valida um model de ContactRegisterRequest com sucesso")]
        public async Task Validate_ValidCustomerRegister_MustValidateSuccessful()
        {
            //Arrange
            var validator = new ContactRequestValidator(_contactRepository.Object);

            var contactRegister = ContactRegisterRequestFaker.ContactRegisterRequestGenerate();

            //Act
            var result = await validator.ValidateAsync(contactRegister);

            //Assert
            Assert.NotNull(result);
            Assert.True(result.IsValid);
            Assert.False(result.Errors.Any());
        }

        [Theory(DisplayName = "Validate - Valida um model de ContactRegisterRequest com campos inválidos retornando erros")]
        [InlineData("")]
        [InlineData("(9823) 12331-1233")]
        [InlineData("+55 (984) 42331-1233")]
        [InlineData("    ")]
        [InlineData("aaaa")]
        public async Task ValidateCustomerRegister_InvalidFields_MustValidateWithErrors(string field)
        {
            var validator = new ContactRequestValidator(_contactRepository.Object);

            var customerRegister = ContactRegisterRequestFaker.ContactRegisterRequestGenerate();
            foreach (var phone in customerRegister.Phones)
            {
                phone.FormattedPhone = field;
            }

            var result = await validator.TestValidateAsync(customerRegister);

            Assert.NotNull(result);
            Assert.False(result.IsValid);
            Assert.True(result.Errors.Any());
            result.ShouldHaveValidationErrorFor(x => x.Phones);
        }
    }
}
