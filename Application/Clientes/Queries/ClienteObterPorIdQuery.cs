using Application.Clientes.DTO;
using Application.Common;
using Infrastructure.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Application.Clientes.Queries;

public class ClienteObterPorIdQuery : IRequest<Response<ClienteDto>>
{
    public int Id { get; set; }
}

public class ClienteObterPorIdQueryHandler : IRequestHandler<ClienteObterPorIdQuery, Response<ClienteDto>>
{
    private readonly DataContext _context;

    public ClienteObterPorIdQueryHandler(DataContext dataContext)
    {
        _context = dataContext;
    }

    public async Task<Response<ClienteDto>> Handle(ClienteObterPorIdQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var cliente = await _context.Clientes
                .Where(c => c.Id == request.Id)
                .Select(c => new ClienteDto
                {
                    Id = c.Id,
                    Nome = c.Nome,
                    Email = c.Email,
                    Telefone = c.Telefone,
                    Endereco = c.Endereco,
                    DataCadastro = c.DataCadastro
                })
                .FirstOrDefaultAsync(cancellationToken);

            if (cliente is null)
                return new Response<ClienteDto> { Status = false, Message = "Cliente não encontrado" };

            return new Response<ClienteDto> { Data = cliente, Status = true, Message = "Cliente exibido com sucesso" };
        }
        catch (Exception ex)
        {
            return new Response<ClienteDto> { Status = false, Message = ex.Message };
        }
    }

}
