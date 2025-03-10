using Application.Clientes.DTO;
using Application.Common;
using Infrastructure.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Clientes.Queries;
public class ClienteObterQuery : IRequest<Response<List<ClienteDto>>>
{
}

public class ClienteObterQueryHandler : IRequestHandler<ClienteObterQuery, Response<List<ClienteDto>>>
{
    private readonly DataContext _context;

    public ClienteObterQueryHandler(DataContext context)
    {
        _context = context;
    }

    public async Task<Response<List<ClienteDto>>> Handle(ClienteObterQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var clientes = await _context.Clientes
                .Select(c => new ClienteDto
                {
                    Id = c.Id,
                    Nome = c.Nome,
                    Email = c.Email,
                    Telefone = c.Telefone,
                    Endereco = c.Endereco,
                    DataCadastro = c.DataCadastro
                })
                .ToListAsync();

            if (!clientes.Any()) 
                return new Response<List<ClienteDto>> { Status = false, Message = "Lista de Clientes vazia"};

            return new Response<List<ClienteDto>> { Data = clientes, Status = true, Message = "Clientes listados com sucesso" };
        }
        catch (Exception ex)
        {
            return new Response<List<ClienteDto>> { Status = false, Message = ex.Message };
        }
    }

}
