using CDBInvestimentos.Aplicacao.Interfaces;
using CDBInvestimentos.Aplicacao.Responses;
using CDBInvestimentos.Aplicacao.Services.Calculators;

public class ServicoCalculadoraCDB : IServicoCalculadoraCDB
{
    public async Task<CDBCalculoResultado> CalcularCDBAsync(decimal valorInicial, int prazoMeses)
    {
        await Task.Delay(1);

        decimal valorBruto = CDBCalculator.CalcularValorBruto(valorInicial, prazoMeses);

        decimal taxaImposto = prazoMeses switch
        {
            <= 6 => 22.5m,
            <= 12 => 20.0m,
            <= 24 => 17.5m,
            _ => 15.0m
        };

        decimal valorLiquido = CDBCalculator.CalcularValorLiquido(valorBruto, valorInicial, taxaImposto);

        return new CDBCalculoResultado
        {
            ValorBruto = valorBruto,
            ValorLiquido = valorLiquido
        };
    }
}