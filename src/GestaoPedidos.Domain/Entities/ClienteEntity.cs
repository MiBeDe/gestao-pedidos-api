using System.ComponentModel.DataAnnotations;

namespace GestaoPedidos.Domain.Entities
{
    public class ClienteEntity
    {
        [Key]
        public int IdCliente { get; private set; }
        public string NomeCompleto { get; private set; }
        public string Cpf { get; private set; }

        private ClienteEntity() { }

        public ClienteEntity(string nomeCompleto, string cpf)
        {
            NomeCompleto = nomeCompleto;
            Cpf = cpf;
        }
    }
}
