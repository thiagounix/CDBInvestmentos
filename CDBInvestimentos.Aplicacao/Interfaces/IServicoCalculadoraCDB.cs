using CdbInvestimentos.Aplicacao.Responses;

namespace CdbInvestimentos.Aplicacao.Interfaces
{
    public interface IServicoCalculadoraCdb
    {
        Task<CdbCalculoResultado> CalcularCdbAsync(decimal valorInicial, int prazoMeses);
    }
}