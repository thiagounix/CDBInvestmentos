var builder = WebApplication.CreateBuilder(args);

// Adicionar serviços ao contêiner de injeção de dependência
builder.Services.AddScoped<ServicoCalculadoraCDB>(); // Registrando o serviço de cálculo do CDB

// Construir a aplicação
var app = builder.Build();

// Habilitar redirecionamento de HTTP para HTTPS
app.UseHttpsRedirection();

// Definição do endpoint Minimal API para o cálculo do CDB
app.MapPost("/api/cdb/calcular", async (CDBInvestimento investimento, ServicoCalculadoraCDB servico) =>
{
    if (investimento.PrazoMeses <= 1 || investimento.ValorInicial <= 0)
    {
        return Results.BadRequest("O prazo deve ser maior que 1 mês e o valor inicial deve ser positivo.");
    }

    var valorBruto = await servico.CalcularValorBrutoAsync(investimento);
    var imposto = await servico.CalcularImpostoAsync(valorBruto, investimento.ValorInicial, investimento.PrazoMeses);
    var valorLiquido = valorBruto - imposto;

    return Results.Ok(new { ValorBruto = valorBruto, ValorLiquido = valorLiquido });
});

// Iniciar a aplicação
app.Run();
