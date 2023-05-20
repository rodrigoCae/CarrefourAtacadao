using System;
using System.Collections.Generic;
using Carrefour_Atacadao_BackEnd.Entites;
using Microsoft.EntityFrameworkCore;

namespace Carrefour_Atacadao_BackEnd.Context;

public partial class CarrefourAtacadaoContext : DbContext
{
    public CarrefourAtacadaoContext()
    {
    }

    public CarrefourAtacadaoContext(DbContextOptions<CarrefourAtacadaoContext> options)
        : base(options)
    {
    }

    public virtual DbSet<TbCidade> TbCidades { get; set; }

    public virtual DbSet<TbCliente> TbClientes { get; set; }

    public virtual DbSet<TbClienteEndereco> TbClienteEnderecos { get; set; }

    public virtual DbSet<TbEndereco> TbEnderecos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
            var connectionString = Environment.GetEnvironmentVariable("SQLCONNSTR_DBTeste");
            optionsBuilder.UseSqlServer(connectionString);
            //optionsBuilder.UseSqlServer("Data Source=DESKTOP-LSEMRNN;Initial Catalog=Carrefour_Atacadao;Integrated Security=True;TrustServerCertificate=True;");    
    }



    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TbCidade>(entity =>
        {
            entity.ToTable("TB_CIDADE");

            entity.Property(e => e.Id)
                //.ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.Estado)
                .HasMaxLength(2)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("ESTADO");
            entity.Property(e => e.Nome)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("NOME");
        });

        modelBuilder.Entity<TbCliente>(entity =>
        {
            entity.ToTable("TB_CLIENTE");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CodEmpresa).HasColumnName("COD_EMPRESA");
            entity.Property(e => e.Cpf)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("CPF");
            entity.Property(e => e.DataNascimento)
                .HasColumnType("date")
                .HasColumnName("DATA_NASCIMENTO");
            entity.Property(e => e.Email)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("EMAIL");
            entity.Property(e => e.Nome)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("NOME");
            entity.Property(e => e.Rg)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("RG");
            entity.Property(e => e.Telefone)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("TELEFONE");
        });

        modelBuilder.Entity<TbClienteEndereco>(entity =>
        {
            entity.ToTable("TB_CLIENTE_ENDERECO");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.ClienteId).HasColumnName("CLIENTE_ID");
            entity.Property(e => e.EnderecoId).HasColumnName("ENDERECO_ID");

            entity.HasOne(d => d.Cliente).WithMany(p => p.TbClienteEnderecos)
                .HasForeignKey(d => d.ClienteId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CLIENTE");

            entity.HasOne(d => d.Endereco).WithMany(p => p.TbClienteEnderecos)
                .HasForeignKey(d => d.EnderecoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ENDERECO");
        });

        modelBuilder.Entity<TbEndereco>(entity =>
        {
            entity.ToTable("TB_ENDERECO");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Bairro)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("BAIRRO");
            entity.Property(e => e.Cep)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("CEP");
            entity.Property(e => e.CidadeId).HasColumnName("CIDADE_ID");
            entity.Property(e => e.Complemento)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("COMPLEMENTO");
            entity.Property(e => e.Numero)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("NUMERO");
            entity.Property(e => e.Rua)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("RUA");
            entity.Property(e => e.TipoEndereco).HasColumnName("TIPO_ENDERECO");

            entity.HasOne(d => d.Cidade).WithMany(p => p.TbEnderecos)
                .HasForeignKey(d => d.CidadeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CIDADE");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
