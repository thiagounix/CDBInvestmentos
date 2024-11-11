using CdbInvestimentos.Aplicacao.Responses;
using MediatR;
namespace CdbInvestimentos.Aplicacao.Commands;
public class CalcularCdbCommand : IRequest<CdbCalculoResultado>
{
    public decimal ValorInicial { get; set; }
    public int PrazoMeses { get; set; }
}


