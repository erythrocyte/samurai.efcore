using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SamuraiApp.Data;
using SamuraiApp.Domain;

namespace SamuraiApp.Tests
{
    [TestClass]
    public class DatabaseTests
    {
        [TestMethod]
        public void CanInsertSamuraiIntoDatabase()
        {
            var contextOptions = new DbContextOptionsBuilder<SamuraiContext>()
                .UseSqlServer(@"Server=localhost;Database=SamuraiAppData;User=SA;Password=Samurai_01")
                .Options;

            using (var context = new SamuraiContext(contextOptions))
            {
                // arrange
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
                var samurai = new Samurai();

                // act
                context.Samurais.Add(samurai);
                Debug.WriteLine($"Before save : { samurai.Id }");

                context.SaveChanges();
                Debug.WriteLine($"After save : { samurai.Id }");

                // assert
                Assert.AreNotEqual(0, samurai.Id);
            }
        }
    }
}
