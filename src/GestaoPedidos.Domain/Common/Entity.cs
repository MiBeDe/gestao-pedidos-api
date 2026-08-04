using System.ComponentModel.DataAnnotations.Schema;

namespace GestaoPedidos.Domain.Common
{
    public abstract class Entity
    {
        private readonly List<DomainEvent> _domainEvents = new();

        [NotMapped]
        public ICollection<DomainEvent> DomainEvents => _domainEvents;

        protected void Raise(DomainEvent domainEvent)
        {
            _domainEvents.Add(domainEvent);
        }

        public void ClearDomainEvents()
        {
            _domainEvents.Clear();
        }
    }
}
