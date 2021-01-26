using System;
using System.Linq;
using SamuraiApp.Data;
using SamuraiApp.Domain;

namespace ConsoleApp
{
    class Program
    {
        private static SamuraiContext context = new SamuraiContext();

        static void Main(string[] args)
        {
            context.Database.EnsureCreated();
            GetSamurais("Before add:");
            AddSamurai("Michael");
            GetSamurais("After add:");
            Console.WriteLine("press any key");
            Console.Read();
        }

        private static void AddSamurai(string name)
        {
            var samurai = new Samurai { Name = name };
            context.Samurais.Add(samurai);
            context.SaveChanges();
        }

        private static void GetSamurais(string text)
        {
            var samurais = context.Samurais.ToList();
            Console.WriteLine($"{ text }: Samurai count is { samurais.Count }");
            samurais.ForEach(samurai => Console.WriteLine(samurai.Name));
        }
    }
}
