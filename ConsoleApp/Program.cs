using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Diagnostics.Tracing;
using System;
using System.Linq;
using SamuraiApp.Data;
using SamuraiApp.Domain;
using Microsoft.EntityFrameworkCore;

namespace ConsoleApp
{
    class Program
    {
        private static SamuraiContext _context = new SamuraiContext();

        static void Main(string[] args)
        {
            // AddSamurai("Michael");
            // GetSamurais("After add:");
            // InsertMultipleSamurais();            
            // QueryFilters();
            // RetrieveAndUpdateSamurai();
            // InsertBattle();
            Console.WriteLine("press any key");
            Console.Read();
        }

        private static void InsertMultipleSamurais()
        {
            var samurai = new Samurai { Name = "Oldat" };
            var samurai2 = new Samurai { Name = "Vasya" };
            var samurai3 = new Samurai { Name = "Wertoj" };
            var samurai4 = new Samurai { Name = "Gwedpk" };
            _context.Samurais.AddRange(samurai, samurai2, samurai3, samurai4);
            _context.SaveChanges();
        }

        private static void InsertVariousTypes()
        {
            var samurai = new Samurai { Name = "Nakamuro" };
            var clan = new Clan { ClanName = "Champlue's Clan" };
            _context.AddRange(samurai, clan);
            _context.SaveChanges();
        }

        private static void GetSamuraisSimpler()
        {
            var query = _context.Samurais;
            var samurais = query.ToList();
        }

        private static void AddSamurai(string name)
        {
            var samurai = new Samurai { Name = name };
            _context.Samurais.Add(samurai);
            _context.SaveChanges();
        }

        private static void GetSamurais(string text)
        {
            var samurais = _context.Samurais.ToList();
            Console.WriteLine($"{ text }: Samurai count is { samurais.Count }");
            samurais.ForEach(samurai => Console.WriteLine(samurai.Name));
        }

        private static void QueryFilters()
        {
            // var name = "Vasya";
            // var samurais = _context.Samurais.Where(s => s.Name == name).ToList();
            var samurais = _context.Samurais.Where(s => EF.Functions.Like(s.Name, "V%"))
                .FirstOrDefault();
        }

        private static void RetrieveAndUpdateSamurai()
        {
            var samurai = _context.Samurais.FirstOrDefault();
            samurai.Name += "San";
            _context.SaveChanges();
        }

        private static void RetrieveAndUpdateMultipleSamurai()
        {
            var samurais = _context.Samurais.Skip(1).Take(3).ToList();
            samurais.ForEach(s => s.Name += "San");
            _context.SaveChanges();
        }

        private static void InsertBattle()
        {
            _context.Battles.Add(new Battle
            {
                Name = "Battle of Okinawa",
                StartDate = new DateTime(1560, 05, 01),
                EndDate = new DateTime(1560, 06, 15),
            });
            _context.SaveChanges();
        }
    
        private static void QueryAndUpdateBattle_Disconnected()
        {
            var battle = _context.Battles.AsNoTracking().FirstOrDefault();
            battle.EndDate = new DateTime(1560, 06, 30);
            using (var newContextInstance = new SamuraiContext())
            {
                newContextInstance.Battles.Update(battle);
                newContextInstance.SaveChanges();
            }
        }
    }
}
