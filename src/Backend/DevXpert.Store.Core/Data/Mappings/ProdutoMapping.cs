using DevXpert.Store.Core.Business.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace DevXpert.Store.Core.Data.Mappings
{
    public class ProdutoMapping : IEntityTypeConfiguration<Produto>
    {
        public void Configure(EntityTypeBuilder<Produto> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Nome)
                   .IsRequired()
                   .HasColumnType("VARCHAR(100)");

            builder.Property(p => p.Descricao)
                   .IsRequired()
                   .HasColumnType("VARCHAR(500)");

            builder.Property(p => p.Ativo)
                   .IsRequired()
                   .HasColumnType("BIT");

            builder.Property(p => p.Imagem)
                   .IsRequired()
                   .HasColumnType("VARCHAR(256)");

            builder.Property(p => p.Estoque)
                   .IsRequired()
                   .HasColumnType("INT");

            builder.Property(p => p.Preco)
                   .IsRequired()
                   .HasColumnType("DECIMAL(18,2)");

            builder.HasOne(p => p.Categoria)
                   .WithMany(p => p.Produto)
                   .HasForeignKey(p => p.CategoriaId)
                   .HasConstraintName("FK_PRODUTO_CATEGORIAID");

            builder.HasOne(p => p.Vendedor)
                   .WithMany(p => p.Produto)
                   .HasForeignKey(p => p.VendedorId)
                   .HasConstraintName("FK_PRODUTO_VENDEDORID");

            builder.Property(p => p.Imagem)
                   .IsRequired(false)
                   .HasColumnType("VARCHAR(500)");

            builder.HasIndex(p => p.Nome)
                   .HasDatabaseName("IX_PRODUTO_NOME")
                   .HasFillFactor(80)
                   .IsUnique(false);

            builder.HasIndex(p => new { p.Nome, p.CategoriaId, p.VendedorId })
                   .HasDatabaseName("UQ_PRODUTO_NOME_CATEGORIAID_VENDEDORID")
                   .HasFillFactor(80)
                   .IsUnique(true);

            builder.HasIndex(p => new { p.Nome, p.VendedorId })
                   .HasDatabaseName("UQ_PRODUTO_NOME_VENDEDORID")
                   .HasFillFactor(80)
                   .IsUnique(true);


            builder.ToTable("PRODUTOS");

            builder.Ignore(p => p.ValidationResult);
            builder.Ignore(p => p.FileUpload);
        }
    }
}

