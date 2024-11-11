namespace CdbInvestimentos.Aplicacao.Services.Calculators
{
    public static class CdbCalculator
    {
        private const decimal CDI = 0.009m;
        private const decimal TB = 1.08m;

        public static decimal CalcularValorBruto(decimal valorInicial, int prazoMeses)
        {
            decimal valorBruto = valorInicial;
            for (int i = 0; i < prazoMeses; i++)
            {
                valorBruto *= (1 + (CDI * TB));
            }
            return Math.Round(valorBruto, 2);
        }

        public static decimal CalcularValorLiquido(decimal valorBruto, decimal valorInicial, decimal taxaImposto)
        {
            decimal lucro = valorBruto - valorInicial;
            decimal imposto = Math.Round(lucro * taxaImposto / 100, 2);
            return Math.Round(valorBruto - imposto, 2);
        }
    }
}
