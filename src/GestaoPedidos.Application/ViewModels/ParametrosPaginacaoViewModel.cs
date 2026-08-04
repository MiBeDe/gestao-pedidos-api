namespace GestaoPedidos.Application.ViewModels
{
    public class ParametrosPaginacaoViewModel
    {
        private const int TamanhoMaximoPagina = 100;
        public int Pagina { get; set; } = 1;

        private int _tamanhoPagina = 10;

        public int tamanhoPagina
        {
            get => _tamanhoPagina;
            set => _tamanhoPagina = value > TamanhoMaximoPagina ? TamanhoMaximoPagina : value;
        }
    }
}
