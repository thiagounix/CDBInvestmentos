using CDBInvestimentos.Aplicacao.Responses;
using MediatR;

public class CalcularCDBCommand : IRequest<CDBCalculoResultado>
{
    public decimal ValorInicial { get; set; }
    public int PrazoMeses { get; set; }
}


