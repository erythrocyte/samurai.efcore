using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SamuraiApp.Data;
using SamuraiApp.Domain;

namespace SamuraiApp.Tests
{
    [TestClass]
    public class InMemoryTests
    {
        [TestMethod]
        public void CanInsertSamuraiIntoDatabase()
        {
            var contextOptions = new DbContextOptionsBuilder<SamuraiContext>()
                .UseInMemoryDatabase("CanInsertSamurai")
                .Options;

            using (var context = new SamuraiContext(contextOptions))
            {
                // arrange
                var samurai = new Samurai();

                // act
                context.Samurais.Add(samurai);

                // assert
                Assert.AreEqual(EntityState.Added, context.Entry(samurai).State);
            }
        }
    }
}