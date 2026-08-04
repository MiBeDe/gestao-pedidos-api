namespace GestaoPedidos.Api.Filters
{
    public class ExcecaoDeDominio : Exception
    {
        public List<string> MensagensDeErro { get; }

        public ExcecaoDeDominio(List<string> erros) : base("Erro de domínio")
        {
            MensagensDeErro = erros;
        }
    }
}
