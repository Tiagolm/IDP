using Castle.Core.Resource;
using Domain.Interfaces;
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
    public class BaseRepositoryTest : IClassFixture<DbTest>
    {
        private BaseRepository<Contact> _baseRepository;
        private ContactRepository _contactRepository;
        private readonly UnitOfWork _unitOfWork;

        public BaseRepositoryTest(DbTest dbTest)
        {
            var serviceProvider = dbTest.ServiceProvider;
            var context = serviceProvider.GetRequiredService<ApplicationContext>();

            _baseRepository = new BaseRepository<Contact>(context);
            _contactRepository = new ContactRepository(context);
            _unitOfWork = new UnitOfWork(context);
        }

        [Fact(DisplayName = "GetByIdAsync - Registra e valida se ao consultar por Id retorna o mesmo Contato registrado anteriormente", Skip = "Tests need database. Run only on localhost.")]
        public async Task GetByIdAsync_ContatoRegistrado_DeveRetornarMesmoContato()
        {
            //Arrange
            var contact = ContactFaker.ContactGenerate();
            var phones = ContactFaker.PhonesGenerate();

            contact.Phones = phones;

            var addResult = await _contactRepository.Add(contact);
            await _unitOfWork.Save();

            //Act
            var result = await _baseRepository.GetById(addResult.Id);

            //Assert
            Assert.NotNull(result);
            Assert.True(result.Id != 0);
            Assert.Equal(addResult.Id, result.Id);
            Assert.Equal(addResult.Name, result.Name);
        }

        [Fact(DisplayName = "GetByIdAsync - Valida se retorna nulo ao consultar Contato por ID inválido", Skip = "Tests need database. Run only on localhost.")]
        public async Task GetByIdAsync_IdInvalidoDeContato_DeveRetornarNulo()
        {
            var result = await _baseRepository.GetById(9999);

            Assert.Null(result);
        }

        [Fact(DisplayName = "GetOneAsync - Registra e valida se ao consultar por telefones retorna o mesmo Contato registrado anteriormente", Skip = "Tests need database. Run only on localhost.")]
        public async Task GetOneAsync_ContatoRegistrado_DeveRetornarMesmoContato()
        {
            //Arrange
            var contact = ContactFaker.ContactGenerate();
            var phones = ContactFaker.PhonesGenerate();

            contact.Phones = phones;

            var addResult = await _contactRepository.Add(contact);
            await _unitOfWork.Save();

            //Act
            _baseRepository.AddPreQuery(x => x.Where(c => c.Phones == addResult.Phones));
            var result = (await _contactRepository.List()).FirstOrDefault();

            //Assert
            Assert.NotNull(result);
            Assert.True(result.Id != 0);
            Assert.Equal(addResult.Id, result.Id);
            Assert.Equal(addResult.Phones, result.Phones);
        }

        [Fact(DisplayName = "GetOneAsync - Valida se retorna nulo ao consulta Contato por nome", Skip = "Tests need database. Run only on localhost.")]
        public async Task GetOneAsync_ContatoNaoRegistrado_DeveRetornarNull()
        {
            _baseRepository.AddPreQuery(x => x.Where(c => c.Name.Equals("teste")));
            var result = (await _baseRepository.List()).FirstOrDefault();

            Assert.Null(result);
        }

        [Fact(DisplayName = "GetByIdAsync - Registra e valida se ao consultar por nome retorna o mesmo Contato registrado anteriormente", Skip = "Tests need database. Run only on localhost.")]
        public async Task ExistsAsync_ContatoRegistrado_DeveRetornarTrue()
        {
            //Arrange
            var contact = ContactFaker.ContactGenerate();
            var phones = ContactFaker.PhonesGenerate();

            contact.Phones = phones;

            var addResult = await _contactRepository.Add(contact);
            await _unitOfWork.Save();

            //Act
            _baseRepository.AddPreQuery(x => x.Where(c => c.Name == addResult.Name));
            var result = (await _contactRepository.List()).FirstOrDefault();

            //Assert
            Assert.NotNull(result);
            Assert.True(result.Id != 0);
            Assert.Equal(addResult.Id, result.Id);
            Assert.Equal(addResult.Name, result.Name);
        }

        [Fact(DisplayName = "ExistsAsync - Testa não existência de Contato", Skip = "Tests need database. Run only on localhost.")]
        public async Task ExistsAsync_ContatoNaoRegistrado_DeveRetornarFalse()
        {
            _baseRepository.AddPreQuery(x => x.Where(c => c.Name.Equals("teste")));
            var result = (await _contactRepository.List()).Any();

            Assert.False(result);
        }
    }
}
