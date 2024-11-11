using CdbInvestimentos.Aplicacao.Interfaces;
using CdbInvestimentos.Aplicacao.Responses;
using MediatR;

namespace CdbInvestimentos.Aplicacao.Handlers;

public class CalcularCdbCommandHandler : IRequestHandler<CalcularCdbCommand, CdbCalculoResultado>
{
    private readonly IServicoCalculadoraCdb _servicoCalculadoraCdb;

    public CalcularCdbCommandHandler(IServicoCalculadoraCdb servicoCalculadoraCdb)
    {
        _servicoCalculadoraCdb = servicoCalculadoraCdb;
    }

    public async Task<CdbCalculoResultado> Handle(CalcularCdbCommand request, CancellationToken cancellationToken)
    {
        if (request == null)
        {
            throw new ArgumentNullException(nameof(request), "O objeto investimento não pode ser nulo.");
        }

        if (request.ValorInicial <= 0)
            throw new ArgumentException("O valor inicial deve ser maior que zero.");

        if (request.PrazoMeses <= 0)
            throw new ArgumentException("O prazo em meses deve ser maior que 0.");

        if (request.PrazoMeses > 1200)
            throw new ArgumentException("O prazo em meses é muito alto. O máximo permitido é 1200 meses.");

        var resultado = await _servicoCalculadoraCdb.CalcularCdbAsync(request.ValorInicial, request.PrazoMeses);
        return resultado;
    }
}