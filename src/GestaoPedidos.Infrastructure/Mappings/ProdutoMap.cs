using GestaoPedidos.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GestaoPedidos.Infrastructure.Mappings
{
    public class ProdutoMap : IEntityTypeConfiguration<ProdutoEntity>
    {
        public void Configure(EntityTypeBuilder<ProdutoEntity> builder)
        {
            builder.ToTable("Produtos");

            builder.HasKey(x => x.IdProduto);

            builder.Property(x => x.NomeProduto)
                   .HasColumnName("NomeProduto")
                   .HasMaxLength(1000)
                   .IsRequired();

            builder.Property(x => x.Descricao)
                   .HasColumnName("Descricao")
                   .IsRequired();

            builder.Property(x => x.Preco)
                   .HasColumnName("Preco")
                   .HasColumnType("decimal(18,2)")
                   .IsRequired();

            builder.Property(x => x.Quantidade)
                   .HasColumnName("Quantidade")
                   .IsRequired();
        }
    }
}
