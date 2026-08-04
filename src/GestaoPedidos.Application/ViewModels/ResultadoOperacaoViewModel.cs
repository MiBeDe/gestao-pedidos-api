namespace GestaoPedidos.Application.ViewModels
{
    public class ResultadoOperacaoViewModel
    {
        public bool Sucesso { get; set; }
        public string Mensagem { get; set; }
        public List<dynamic>? Dados { get; set; }
    }
}
