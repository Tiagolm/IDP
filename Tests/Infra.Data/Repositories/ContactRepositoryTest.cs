using Castle.Core.Resource;
using Domain.Models;
using Infrastructure;
using Infrastructure.Data;
using Infrastructure.Repository;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tests.Fakers.Models;
using Xunit;

namespace Tests.Infra.Data.Repositories
{
    [Trait("TestType", "Unit")]
    public class ContatoRepositoryTest : IClassFixture<DbTest>
    {
        private ContactRepository _contactRepository;
        private readonly UnitOfWork _unitOfWork;

        public ContatoRepositoryTest(DbTest dbTest)
        {
            var serviceProvider = dbTest.ServiceProvider;
            var context = serviceProvider.GetRequiredService<ApplicationContext>();

            _contactRepository = new ContactRepository(context);
            _unitOfWork = new UnitOfWork(context);
        }

        [Fact(DisplayName = "AddContato", Skip = "Tests need database. Run only on localhost.")]
        public async Task AddAsync_ValidContato_Successful()
        {
            //Arrange
            var contact = ContactFaker.ContactGenerate();
            var phones = ContactFaker.PhonesGenerate();

            contact.Phones = phones;

            //Act
            var addResult = await _contactRepository.Add(contact);
            await _unitOfWork.Save();

            //Assert
            Assert.NotNull(addResult);
            Assert.True(addResult.Id != 0);
            Assert.True(addResult.Phones is not null);
            Assert.Equal(contact.Name, addResult.Name);
        }

        [Fact(DisplayName = "UpdateContato - Registra e atualiza Contato com sucesso", Skip = "Tests need database. Run only on localhost.")]
        public async Task UpdateContato_ValidContato_Successful()
        {
            //Arrange
            var contact = ContactFaker.ContactGenerate();
            var phones = ContactFaker.PhonesGenerate();

            contact.Phones = phones;

            contact = await _contactRepository.Add(contact);
            await _unitOfWork.Save();

            var newContato = ContactFaker.ContactGenerate();

            //Act
            contact.Name = newContato.Name;
            contact.Phones = newContato.Phones;
           
            await _contactRepository.Update(contact);
            await _unitOfWork.Save();

            //Assert
            Assert.NotNull(contact);
            Assert.True(contact.Id != 0);
            Assert.Equal(contact.Name, newContato.Name);
            Assert.Equal(contact.Phones, newContato.Phones);
        }

        [Fact(DisplayName = "RemoveContato - Valida remoção de Contato existente", Skip = "Tests need database. Run only on localhost.")]
        public async Task RemoveContato_ValidContato_Successful()
        {
            //Arrange
            var contact = ContactFaker.ContactGenerate();
            var phones = ContactFaker.PhonesGenerate();

            contact.Phones = phones;

            var addResult = await _contactRepository.Add(contact);
            await _unitOfWork.Save();

            //Act
            await _contactRepository.Delete(addResult.Id);
            await _unitOfWork.Save();

            var result = await _contactRepository.GetById(contact.Id);

            //Assert
            Assert.Null(result);
        }
    }
}
