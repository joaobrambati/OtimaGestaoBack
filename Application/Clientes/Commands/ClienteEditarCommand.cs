using Application.Common;
using Infrastructure.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Clientes.Commands;

public class ClienteEditarCommand : IRequest<Response<bool>>
{
    public int Id { get; set; }
    public string? Nome { get; set; }
    public string? Email { get; set; }
    public string? Telefone { get; set; }
    public string? Endereco { get; set; }
}

public class ClienteEditarCommandHandler : IRequestHandler<ClienteEditarCommand, Response<bool>>
{
    private readonly DataContext _context;

    public ClienteEditarCommandHandler(DataContext context)
    {
        _context = context;
    }

    public async Task<Response<bool>> Handle(ClienteEditarCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var cliente = await _context.Clientes
                .FirstOrDefaultAsync(c => c.Id == request.Id);

            if (cliente is null) 
                return new Response<bool> { Data = false, Status = false, Message = "Cliente não encontrado" };

            if (!string.IsNullOrWhiteSpace(request.Nome))
                cliente.Nome = request.Nome;

            if (!string.IsNullOrWhiteSpace(request.Email))
                cliente.Email = request.Email;

            if (!string.IsNullOrWhiteSpace(request.Telefone))
                cliente.Telefone = request.Telefone;

            if (!string.IsNullOrWhiteSpace(request.Endereco))
                cliente.Endereco = request.Endereco;

            _context.Clientes.Update(cliente);
            var editou = await _context.SaveChangesAsync(cancellationToken);

            if (editou > 0)
                return new Response<bool> { Data = true, Status = true, Message = "Cliente atualizado com sucesso" };

            return new Response<bool> { Data = false, Status = false, Message = "Erro ao atualizar o cliente" };

        }
        catch (Exception ex)
        {
            return new Response<bool> { Status = false, Message = ex.Message };
        }
    }

}

