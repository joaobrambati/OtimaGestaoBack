using Application.Common;
using Infrastructure.Context;
using MediatR;

namespace Application.Clientes.Commands;

public class ClienteExcluirCommand : IRequest<Response<bool>>
{
    public int Id { get; set; }
}

public class ClienteExcluirCommandHandler : IRequestHandler<ClienteExcluirCommand, Response<bool>>
{
    private readonly DataContext _context;

    public ClienteExcluirCommandHandler(DataContext context)
    {
        _context = context;
    }

    public async Task<Response<bool>> Handle(ClienteExcluirCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var cliente = await _context.Clientes.FindAsync(request.Id);

            if (cliente is null) 
                return new Response<bool> { Data = false, Status = false, Message = "Cliente não encontrado" };

            _context.Clientes.Remove(cliente);
            var excluiu = await _context.SaveChangesAsync(cancellationToken);

            if (excluiu > 0)
                return new Response<bool> { Data = true, Status = true, Message = "Cliente excluído com sucesso" };

            return new Response<bool> { Data = false, Status = false, Message = "Erro ao excluir o cliente" };

        }
        catch (Exception ex)
        {
            return new Response<bool> { Status = false, Message = ex.Message }; 
        }
    }

}