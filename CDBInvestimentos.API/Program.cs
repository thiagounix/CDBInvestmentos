using CDBInvestimentos.Aplicacao.Handlers;
using CDBInvestimentos.Aplicacao.Interfaces;
using CDBInvestimentos.Aplicacao.Responses;
using MediatR;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMediatR(typeof(CalcularCDBCommandHandler).Assembly);
builder.Services.AddScoped<IServicoCalculadoraCDB, ServicoCalculadoraCDB>();
builder.Services.AddScoped<IRequestHandler<CalcularCDBCommand, CDBCalculoResultado>, CalcularCDBCommandHandler>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseCors("AllowAllOrigins");
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
