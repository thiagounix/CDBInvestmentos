namespace CDBInvestimentos.Aplicacao.Commands
{
    public class CalcularCDBCommand
    {
        public decimal ValorInicial { get; set; }
        public int PrazoMeses { get; set; }

        public CalcularCDBCommand(decimal valorInicial, int prazoMeses)
        {
            ValorInicial = valorInicial;
            PrazoMeses = prazoMeses;
        }
    }
}