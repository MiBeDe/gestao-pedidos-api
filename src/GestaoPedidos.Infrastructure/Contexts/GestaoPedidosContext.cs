using GestaoPedidos.Domain.Common;
using GestaoPedidos.Domain.Entities;
using GestaoPedidos.Infrastructure.Mappings;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GestaoPedidos.Infrastructure.Contexts
{
    public class GestaoPedidosContext : DbContext
    {
        private readonly IPublisher _publisher;



        public DbSet<ClienteEntity> Clientes { get; set; }
        public DbSet<ProdutoEntity> Produtos { get; set; }
        public DbSet<PedidoEntity> Pedidos { get; set; }
        public DbSet<PedidoProdutosEntity> PedidoProdutos { get; set; }
        public DbSet<StatusPedidoEntity> StatusPedido { get; set; }




        public GestaoPedidosContext(DbContextOptions<GestaoPedidosContext> options, IPublisher publisher) 
            : base(options)
        {
            _publisher = publisher;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ClienteMap());
            modelBuilder.ApplyConfiguration(new ProdutoMap());
            modelBuilder.ApplyConfiguration(new StatusPedidoMap());
            modelBuilder.ApplyConfiguration(new PedidoMap());
            modelBuilder.ApplyConfiguration(new PedidoProdutoMap());

            base.OnModelCreating(modelBuilder);
        }

        public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            var entities = ChangeTracker
                .Entries<Entity>()
                .Where(e => e.Entity.DomainEvents.Any())
                .Select(e => e.Entity)
                .ToList();

            var domainEvents = entities
                .SelectMany(e => e.DomainEvents)
                .ToList();

            foreach (var entity in entities)
            {
                entity.ClearDomainEvents();
            }

            var result = await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);

            foreach (var domainEvent in domainEvents)
            {
                await _publisher.Publish(domainEvent, cancellationToken);
            }

            return result;
        }

    }
}
