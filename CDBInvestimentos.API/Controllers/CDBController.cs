using Microsoft.AspNetCore.Mvc;
using MediatR;

namespace CDBInvestimentos.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CDBController : ControllerBase
{
    private readonly IMediator _mediator;

    public CDBController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("calcular")]
    public async Task<IActionResult> CalcularCDB([FromBody] CalcularCDBCommand command)
    {
        var resultado = await _mediator.Send(command);
        return Ok(resultado);
    }
}