using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace SamuraiApp.Data
{
    public partial class SamuraiAppDataContext : DbContext
    {
        public SamuraiAppDataContext()
        {
        }

        public SamuraiAppDataContext(DbContextOptions<SamuraiAppDataContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Clan> Clans { get; set; }
        public virtual DbSet<Quote> Quotes { get; set; }
        public virtual DbSet<Samurai> Samurais { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=localhost;Database=SamuraiAppData;User=SA;Password=Samurai_01");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Quote>(entity =>
            {
                entity.HasIndex(e => e.SamuraiId, "IX_Quotes_SamuraiId");

                entity.HasOne(d => d.Samurai)
                    .WithMany(p => p.Quotes)
                    .HasForeignKey(d => d.SamuraiId);
            });

            modelBuilder.Entity<Samurai>(entity =>
            {
                entity.HasIndex(e => e.ClanId, "IX_Samurais_ClanId");

                entity.HasOne(d => d.Clan)
                    .WithMany(p => p.Samurais)
                    .HasForeignKey(d => d.ClanId);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
