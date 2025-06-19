using DevXpert.Store.Core.Business.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace DevXpert.Store.Core.Data.Mappings
{
    public class VendedorMapping : IEntityTypeConfiguration<Vendedor>
    {
        public void Configure(EntityTypeBuilder<Vendedor> builder)
        {
            builder.HasKey(v => v.Id);

            builder.Property(v => v.Nome)
                   .IsRequired()
                   .HasColumnType("VARCHAR(100)");

            builder.Property(v => v.Email)
                   .IsRequired()
                   .HasColumnType("VARCHAR(100)");

            builder.Property(v => v.Senha)
                   .IsRequired()
                   .HasColumnType("VARCHAR(256)");

            builder.Property(v => v.Ativo)
                   .IsRequired()
                   .HasColumnType("BIT");

            builder.HasIndex(v => v.Email)
                   .HasDatabaseName("UQ_VENDEDOR_EMAIL")
                   .HasFillFactor(80)
                   .IsUnique();

            builder.HasIndex(v => v.Nome)
                   .HasDatabaseName("IX_VENDEDOR_NOME")
                   .HasFillFactor(80)
                   .IsUnique(false);

            builder.ToTable("VENDEDORES");

            builder.Ignore(v => v.ValidationResult);
            builder.Ignore(v => v.ProdutoId);
        }
    }
}

