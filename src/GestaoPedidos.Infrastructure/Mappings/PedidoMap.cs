using GestaoPedidos.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GestaoPedidos.Infrastructure.Mappings
{
    public class PedidoMap : IEntityTypeConfiguration<PedidoEntity>
    {
        public void Configure(EntityTypeBuilder<PedidoEntity> builder)
        {
            builder.ToTable("Pedidos");

            builder.HasKey(x => x.IdPedido);

            builder.Property(x => x.ValorTotalPedido)
                   .HasColumnName("ValorTotalPedido")
                   .HasColumnType("decimal(18,2)")
                   .IsRequired();

            builder.HasOne(x => x.Cliente)
                   .WithMany()
                   .HasForeignKey(x => x.IdCliente)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.StatusPedido)
                   .WithMany()
                   .HasForeignKey(x => x.IdStatus)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(x => x.PedidoProdutos)
                   .WithOne()
                   .HasForeignKey(x => x.IdPedido)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
