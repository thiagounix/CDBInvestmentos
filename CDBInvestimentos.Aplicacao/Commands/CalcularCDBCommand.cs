using CdbInvestimentos.Aplicacao.Responses;
using MediatR;

public class CalcularCdbCommand : IRequest<CdbCalculoResultado>
{
    public decimal ValorInicial { get; set; }
    public int PrazoMeses { get; set; }
}


