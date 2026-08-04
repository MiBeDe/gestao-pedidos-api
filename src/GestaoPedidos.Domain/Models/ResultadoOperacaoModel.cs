namespace GestaoPedidos.Domain.Models
{
    public class ResultadoOperacaoModel
    {
        public bool Sucesso { get; set; }
        public string Mensagem { get; set; }
        public int? ScopedIdentity { get; set; }
        public List<dynamic>? Dados { get; set; } = new();
    }
}
