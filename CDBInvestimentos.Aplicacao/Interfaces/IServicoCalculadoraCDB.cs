using CDBInvestimentos.Aplicacao.Responses;

namespace CDBInvestimentos.Aplicacao.Interfaces
{
    public interface IServicoCalculadoraCDB
    {
        Task<CDBCalculoResultado> CalcularCDBAsync(decimal valorInicial, int prazoMeses);
    }
}