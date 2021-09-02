using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace AltalizaApi.Models
{
    public partial class ApiContext : DbContext
    {
        public ApiContext()
        {
        }

        public ApiContext(DbContextOptions<ApiContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Character> Character { get; set; }
        public virtual DbSet<Vehicle> Vehicle { get; set; }
        public virtual DbSet<Rental> Rental { get; set; }
        public virtual DbSet<Category> Category { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Character>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.ToTable("characters");

                entity.Property(e => e.Id)
                    .HasColumnName("Id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Carteira)
                    .IsRequired()
                    .HasColumnName("Carteira")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasColumnName("Nome")
                    .HasColumnType("varchar(80)");
            });

            modelBuilder.Entity<Vehicle>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.ToTable("vehicles");

                entity.Property(e => e.Id)
                    .HasColumnName("Id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.IdCategoria)
                    .IsRequired()
                    .HasColumnName("IdCategoria")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasColumnName("Nome")
                    .HasColumnType("varchar(80)");

                entity.Property(e => e.Estoque)
                    .IsRequired()
                    .HasColumnName("Estoque")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Imagem)
                    .IsRequired()
                    .HasColumnName("Imagem")
                    .HasColumnType("varchar(80)");

                entity.Property(e => e.Preco1Dia)
                    .IsRequired()
                    .HasColumnName("Preco1Dia")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Preco7Dia)
                    .IsRequired()
                    .HasColumnName("Preco7Dia")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Preco15Dia)
                    .IsRequired()
                    .HasColumnName("Preco15Dia")
                    .HasColumnType("int(11)");
            });

            modelBuilder.Entity<Rental>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.ToTable("character_vehicle");

                entity.Property(e => e.Id)
                    .HasColumnName("Id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.IdPersonagem)
                    .IsRequired()
                    .HasColumnName("IdPersonagem")
                    .HasColumnType("int(11)");

                entity.Property(e => e.IdVeiculo)
                    .IsRequired()
                    .HasColumnName("IdVeiculo")
                    .HasColumnType("int(11)");

                entity.Property(e => e.DataExpiracao)
                    .IsRequired()
                    .HasColumnName("DataExpiracao")
                    .HasColumnType("datetime");
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.ToTable("categories");

                entity.Property(e => e.Id)
                    .HasColumnName("Id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasColumnName("Nome")
                    .HasColumnType("varchar(20)");

                entity.Property(e => e.Descricao)
                    .IsRequired()
                    .HasColumnName("Descricao")
                    .HasColumnType("varchar(80)");
            });
        }
    }
}
