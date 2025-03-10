using Application.Common;
using Domain.Entities;
using Infrastructure.Context;
using MediatR;

namespace Application.Clientes.Commands;
public class ClienteCriarCommand : IRequest<Response<bool>>
{
    public string Nome { get; set; }
    public string Email { get; set; }
    public string Telefone { get; set; }
    public string Endereco { get; set; }
}

public class ClienteCriarCommandHandler : IRequestHandler<ClienteCriarCommand, Response<bool>>
{
    private readonly DataContext _context;

    public ClienteCriarCommandHandler(DataContext context)
    {
        _context = context;
    }

    public async Task<Response<bool>> Handle(ClienteCriarCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var cliente = new Cliente
            {
                Nome = request.Nome,
                Email = request.Email,
                Telefone = request.Telefone,
                Endereco = request.Endereco,
                DataCadastro = DateTime.Now
            };

            _context.Clientes.Add(cliente);
            var criou = await _context.SaveChangesAsync(cancellationToken);

            if (criou > 0)
                return new Response<bool> { Data = true, Status = true, Message = "Cliente criado com sucesso" };

            return new Response<bool> { Data = false, Status = false, Message = "Erro ao criar o cliente" };

        }
        catch(Exception ex)
        {
            return new Response<bool> { Status = false, Message = ex.Message };
        }
    }

}
