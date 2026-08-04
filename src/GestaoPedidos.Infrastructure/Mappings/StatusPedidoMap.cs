using GestaoPedidos.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GestaoPedidos.Infrastructure.Mappings
{
    public class StatusPedidoMap : IEntityTypeConfiguration<StatusPedidoEntity>
    {
        public void Configure(EntityTypeBuilder<StatusPedidoEntity> builder)
        {
            builder.ToTable("StatusPedido");

            builder.HasKey(x => x.IdStatus);

            builder.Property(x => x.Descricao)
                   .HasColumnName("Descricao")
                   .HasMaxLength(50)
                   .IsRequired();
        }
    }
}
