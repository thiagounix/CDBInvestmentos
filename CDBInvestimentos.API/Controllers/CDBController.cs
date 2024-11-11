using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CdbInvestimentos.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CdbController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpPost("calcular")]
    public async Task<IActionResult> CalcularCdb([FromBody] CalcularCdbCommand command)
    {
        var resultado = await _mediator.Send(command);
        return Ok(resultado);
    }
}