using MyPortfolio.Models;

namespace UnitTest1
{
    [TestClass]
    public class UnitTest1
    {
        PersonRepository personRepository;

        [TestInitialize]
        public void Init()
        {
            personRepository = new PersonRepository();
        }

        [TestMethod]
        public void TestAddingPersonToRepository()
        {
            // ARRANGE
            Person person1 = null;

            if (personRepository.FindPerson("test") == null)
            {
                person1 = new Person("test", "test123", "test@gmail.com", "tttttest");
                personRepository.CreateNewPerson(person1); // ACT
            }

            Assert.IsNotNull(personRepository.FindPerson("test")); // ASSERT
        }

        [TestMethod]
        public void TestDeletingFromRepository()
        {
            personRepository.DeletePerson(personRepository.FindPerson("test")); // ACT

            Assert.IsNull(personRepository.FindPerson("test")); // ASSERT
        }
    }
}