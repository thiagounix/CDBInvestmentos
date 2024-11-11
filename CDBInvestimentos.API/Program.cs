using CdbInvestimentos.API.Middlewares;
using CdbInvestimentos.Aplicacao.Handlers;
using CdbInvestimentos.Aplicacao.Interfaces;
using CdbInvestimentos.Aplicacao.Responses;
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
builder.Services.AddMediatR(typeof(CalcularCdbCommandHandler).Assembly);
builder.Services.AddScoped<IServicoCalculadoraCdb, ServicoCalculadoraCdb>();
builder.Services.AddScoped<IRequestHandler<CalcularCdbCommand, CdbCalculoResultado>, CalcularCdbCommandHandler>();

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
