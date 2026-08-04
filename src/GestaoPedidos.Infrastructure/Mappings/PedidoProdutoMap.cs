using GestaoPedidos.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GestaoPedidos.Infrastructure.Mappings
{
    public class PedidoProdutoMap : IEntityTypeConfiguration<PedidoProdutosEntity>
    {
        public void Configure(EntityTypeBuilder<PedidoProdutosEntity> builder)
        {
            builder.ToTable("PedidoProdutos");

            builder.HasKey(x => x.IdPedidoProduto);

            builder.Property(x => x.ValorUnitario)
                   .HasColumnName("ValorUnitario")
                   .HasColumnType("decimal(18,2)")
                   .IsRequired();

            builder.Property(x => x.Quantidade)
                   .HasColumnName("Quantidade")
                   .IsRequired();

            builder.Property(x => x.SubTotal)
                   .HasColumnName("SubTotal")
                   .HasColumnType("decimal(18,2)")
                   .IsRequired();

            builder.HasOne(x => x.Pedido)
                   .WithMany(x => x.PedidoProdutos)
                   .HasForeignKey(x => x.IdPedido)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.Produto)
                   .WithMany()
                   .HasForeignKey(x => x.IdProduto)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
