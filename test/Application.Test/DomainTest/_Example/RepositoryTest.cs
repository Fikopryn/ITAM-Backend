using Core.Interfaces;
using Core.Models.Entities.Tables;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Test.DomainTest._Example
{
    public class RepositoryTest : BaseTest
    {
        private IUnitOfWork _uow;

        private readonly TExPerson addData = new TExPerson()
        {
            Name = "TEST NAME",
            Surname = "TEST SURNAME",
            Email = "TEST EMAIL",
            Street = "TEST STREET",
            City = "TEST CITY"
        };

        [SetUp]
        public void Setup()
        {
            _uow = _svcProvider.GetService<IUnitOfWork>();
        }

        [Test, Order(0)]
        public async Task Insert()
        {
            await _uow.ExPersons.Add(addData);
            await _uow.CompleteAsync();

            var testData = _uow.ExPersons.Set().FirstOrDefault(m => m.Id == addData.Id);

            Assert.That(testData, Is.Not.Null);
        }

        [Test, Order(1)]
        public async Task Update()
        {
            var data = _uow.ExPersons.Set().FirstOrDefault(m => m.Email == addData.Email);

            data.Name = addData.Email + " UPDATE";

            _uow.ExPersons.Update(data);
            await _uow.CompleteAsync();

            var testData = _uow.ExPersons.Set().FirstOrDefault(m => m.Id == data.Id);

            Assert.That(testData.Name, Is.EqualTo(data.Name));
        }

        [Test, Order(2)]
        public async Task Delete()
        {
            var data = _uow.ExPersons.Set().FirstOrDefault(m => m.Email == addData.Email);

            _uow.ExPersons.Remove(data);
            await _uow.CompleteAsync();

            var testData = _uow.ExPersons.Set().FirstOrDefault(m => m.Id == data.Id);

            Assert.That(testData, Is.Null);
        }
    }
}
