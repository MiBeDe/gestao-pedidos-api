namespace GestaoPedidos.Api.Filters
{
    public class ExcecaoDeValidacao : Exception
    {
        public List<string> Erros { get; }

        public ExcecaoDeValidacao(List<string> erros) : base("Erro de validação")
        {
            Erros = erros;
        }
    }
}
