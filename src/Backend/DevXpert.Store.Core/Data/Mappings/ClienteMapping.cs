using DevXpert.Store.Core.Business.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace DevXpert.Store.Core.Data.Mappings
{
    public class ClienteMapping : IEntityTypeConfiguration<Cliente>
    {
        public void Configure(EntityTypeBuilder<Cliente> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Nome)
                   .IsRequired()
                   .HasColumnType("VARCHAR(100)");

            builder.Property(c => c.Email)
                   .IsRequired()
                   .HasColumnType("VARCHAR(100)");

            builder.Property(c => c.Senha)
                   .IsRequired()
                   .HasColumnType("VARCHAR(256)");

            builder.Property(c => c.Ativo)
                   .IsRequired()
                   .HasColumnType("BIT");

            builder.HasIndex(c => c.Email)
                   .HasDatabaseName("UQ_CLIENTE_EMAIL")
                   .HasFillFactor(80)
                   .IsUnique();

            builder.HasIndex(c => c.Nome)
                   .HasDatabaseName("IX_CLIENTE_NOME")
                   .HasFillFactor(80)
                   .IsUnique(false);           

            builder.ToTable("CLIENTES");

            builder.Ignore(c => c.ValidationResult);
        }
    }
}

