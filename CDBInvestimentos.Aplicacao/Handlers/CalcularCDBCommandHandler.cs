using CDBInvestimentos.Aplicacao.Interfaces;
using CDBInvestimentos.Aplicacao.Responses;
using MediatR;

namespace CDBInvestimentos.Aplicacao.Handlers;

public class CalcularCDBCommandHandler : IRequestHandler<CalcularCDBCommand, CDBCalculoResultado>
{
    private readonly IServicoCalculadoraCDB _servicoCalculadoraCDB;

    public CalcularCDBCommandHandler(IServicoCalculadoraCDB servicoCalculadoraCDB)
    {
        _servicoCalculadoraCDB = servicoCalculadoraCDB;
    }

    public async Task<CDBCalculoResultado> Handle(CalcularCDBCommand request, CancellationToken cancellationToken)
    {
        if (request == null)
        {
            throw new ArgumentNullException(nameof(request), "O objeto investimento não pode ser nulo.");
        }

        if (request.ValorInicial <= 0)
            throw new ArgumentException("O valor inicial deve ser maior que zero.");

        if (request.PrazoMeses <= 1)
            throw new ArgumentException("O prazo em meses deve ser maior que 1.");

        if (request.PrazoMeses > 1200)
            throw new ArgumentException("O prazo em meses é muito alto. O máximo permitido é 1200 meses.");

        var resultado = await _servicoCalculadoraCDB.CalcularCDBAsync(request.ValorInicial, request.PrazoMeses);
        return resultado;
    }
}