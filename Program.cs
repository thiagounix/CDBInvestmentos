var builder = WebApplication.CreateBuilder(args);

// Adicionar servi�os ao cont�iner de inje��o de depend�ncia
builder.Services.AddScoped<ServicoCalculadoraCDB>(); // Registrando o servi�o de c�lculo do CDB

// Construir a aplica��o
var app = builder.Build();

// Habilitar redirecionamento de HTTP para HTTPS
app.UseHttpsRedirection();

// Defini��o do endpoint Minimal API para o c�lculo do CDB
app.MapPost("/api/cdb/calcular", async (CDBInvestimento investimento, ServicoCalculadoraCDB servico) =>
{
    if (investimento.PrazoMeses <= 1 || investimento.ValorInicial <= 0)
    {
        return Results.BadRequest("O prazo deve ser maior que 1 m�s e o valor inicial deve ser positivo.");
    }

    var valorBruto = await servico.CalcularValorBrutoAsync(investimento);
    var imposto = await servico.CalcularImpostoAsync(valorBruto, investimento.ValorInicial, investimento.PrazoMeses);
    var valorLiquido = valorBruto - imposto;

    return Results.Ok(new { ValorBruto = valorBruto, ValorLiquido = valorLiquido });
});

// Iniciar a aplica��o
app.Run();
