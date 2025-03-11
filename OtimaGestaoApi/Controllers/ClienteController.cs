using Application.Clientes.Commands;
using Application.Clientes.Queries;
using Application.Produtos.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace OtimaGestaoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ClienteController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("obterTodos")]
        public async Task<IActionResult> Obter()
        {
            return Ok(await _mediator.Send(new ClienteObterQuery { }));
        }

        [HttpPost("obterPorId")]
        public async Task<IActionResult> ObterPorId(ClienteObterPorIdQuery query)
        {
            return Ok(await _mediator.Send(query));
        }

        [HttpPost("excluir")]
        public async Task<IActionResult> Excluir(ClienteExcluirCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        [HttpPost("criar")]
        public async Task<IActionResult> Criar(ClienteCriarCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        [HttpPost("editar")]
        public async Task<IActionResult> Editar(ClienteEditarCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

    }
}
