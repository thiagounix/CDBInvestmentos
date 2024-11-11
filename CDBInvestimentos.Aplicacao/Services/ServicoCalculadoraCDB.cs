using CdbInvestimentos.Aplicacao.Interfaces;
using CdbInvestimentos.Aplicacao.Responses;
using CdbInvestimentos.Aplicacao.Services.Calculators;

public class ServicoCalculadoraCdb : IServicoCalculadoraCdb
{
    public async Task<CdbCalculoResultado> CalcularCdbAsync(decimal valorInicial, int prazoMeses)
    {
        await Task.Delay(1);

        decimal valorBruto = CdbCalculator.CalcularValorBruto(valorInicial, prazoMeses);

        decimal taxaImposto = prazoMeses switch
        {
            <= 6 => 22.5m,
            <= 12 => 20.0m,
            <= 24 => 17.5m,
            _ => 15.0m
        };

        decimal valorLiquido = CdbCalculator.CalcularValorLiquido(valorBruto, valorInicial, taxaImposto);

        return new CdbCalculoResultado
        {
            ValorBruto = valorBruto,
            ValorLiquido = valorLiquido
        };
    }
}