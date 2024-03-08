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

    }
}