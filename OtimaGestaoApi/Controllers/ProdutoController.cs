

using Application.Produtos.Commands;
using Application.Produtos.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace OtimaGestaoApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProdutoController : ControllerBase
{
    private readonly IMediator _mediator;
    public ProdutoController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("obterTodos")]
    public async Task<IActionResult> Obter()
    {
        return Ok(await _mediator.Send(new ProdutoObterQuery { }));
    }

    [HttpPost("excluir")]
    public async Task<IActionResult> Excluir(ProdutoExcluirCommand command)
    {
        return Ok(await _mediator.Send(command));
    }

    [HttpPost("criar")]
    public async Task<IActionResult> Criar(ProdutoCriarCommand command)
    {
        return Ok(await _mediator.Send(command));
    }

}

